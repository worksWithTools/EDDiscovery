﻿using BaseUtils.Misc;
using EDPlugin;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDMobilePlugin
{
    internal class WebSocketServer
    {
        private const int PREVIEW_LENGTH = 50;

        private int socketId;
        private WebSocket socket;
        private readonly CancellationToken Token;
        private AutoResetEvent broadcastAvailable = new AutoResetEvent(false);
        private ConcurrentQueue<string> broadCastMessages = new ConcurrentQueue<string>();
        private readonly ManagedCallbacks managedCallbacks;

        public WebSocketServer(int socketId, HttpListenerWebSocketContext context, ManagedCallbacks managedCallbacks, CancellationToken token)
        {
            this.socketId = socketId;
            socket = context.WebSocket;
            Token = token;
            this.managedCallbacks = managedCallbacks;
        }

        internal void QueueMessage(string message)
        {
            broadCastMessages.Enqueue(message);
            broadcastAvailable.Set();
        }
        internal async Task ReceiveMessagesAsync()
        {
            var broadcastTokenSource = new CancellationTokenSource();
            _ = Task.Run(() => WatchForBroadcasts(broadcastTokenSource.Token));

            try
            {
                Debug.WriteLine($"Socket {socketId} starting to listen for incoming messages...");
                byte[] buffer = new byte[4096];
                while (socket.State == WebSocketState.Open && !Token.IsCancellationRequested)
                {
                    var receiveResult = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), Token);
                    if (receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        Debug.WriteLine($"Socket {socketId}: Closing websocket.");
                        broadcastTokenSource.Cancel();
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", Token);
                    }
                    else
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, receiveResult.Count);
                        Debug.WriteLine($"Socket {socketId}: Received [{message.Left(PREVIEW_LENGTH)}]).");

                        // TODO: Apply a command pattern?
                        // if message is "ready" then send back some data..
                        if (message == WebSocketMessage.REFRESH_STATUS)
                        {
                            var lasthistory = managedCallbacks?.GetLastHistory();
                            Debug.WriteLine($"TRACE: Socket {socketId} Queueing : {lasthistory.ToString()}");
                            var lastSystem = managedCallbacks?.GetLastHistoryEntry(x => x.journalEntry is EliteDangerousCore.JournalEvents.JournalFSDJump, lasthistory)?.journalEntry as EliteDangerousCore.JournalEvents.JournalFSDJump;

                            var response = new MobileWebResponse(message);
                            response.Add(lasthistory);
                            response.Add(lastSystem);
                            
                            await SendMessageToSocketAsync(socketId, socket, response, Token);
                        }
                        else if (message.StartsWith(WebSocketMessage.GET_JOURNAL))
                        {
                            //TODO: add number to get...
                            var history = managedCallbacks?.GetHistory(50);
                            foreach (var entry in history)
                            {
                                Debug.WriteLine($"Socket {socketId} Queueing : {entry.journalEntry.ToString()}");
                                MobileWebResponse response = new MobileWebResponse(message);
                                response.Add(entry.journalEntry);
                                await SendMessageToSocketAsync(socketId, socket, response, Token);
                            }
                        }

                    }
                }
            }
            catch (OperationCanceledException)
            {
                // normal upon task/token cancellation, disregard
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\nSocket {socketId}:\n  Exception {ex.GetType().Name}: {ex.Message}");
                if (ex.InnerException != null) Debug.WriteLine($"  Inner Exception {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
            }
            finally
            {
                socket?.Dispose();
                broadcastTokenSource?.Cancel();
                broadcastTokenSource?.Dispose();
            }
        }
        private async Task WatchForBroadcasts(CancellationToken token)
        {
            Debug.WriteLine($"Socket {socketId} starting to listen for broadcasts...");

            while (!token.IsCancellationRequested)
            {
                try
                {
                    // use async waiting
                    if (await broadcastAvailable.WaitOneAsync(token))
                    {
                        Debug.WriteLine($"Broadcast received by socket {socketId}");
                        if (broadCastMessages.TryDequeue(out string message))
                        {
                            MobileWebResponse response = new MobileWebResponse(WebSocketMessage.BROADCAST);
                            response.Responses.Add(message);
                            await SendMessageToSocketAsync(socketId, socket, response, token);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // normal upon task/token cancellation, disregard
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"\nSocket {socketId} broadcast task:\n  Exception {ex.GetType().Name}: {ex.Message}");
                    if (ex.InnerException != null) Debug.WriteLine($"  Inner Exception {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
                }
            }
        }

        private static async Task SendMessageToSocketAsync(int socketId, WebSocket socket, MobileWebResponse response, CancellationToken socketToken)
        {
            Debug.WriteLine($"Socket {socketId}: Sending next message: [{response.RequestType}]");
            //System.IO.File.WriteAllText(@"D:\Source\worksWithTools\EDDiscovery\EDDiscoveryTests\historyentry.json", message);
            var msgbuf = new ArraySegment<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(response)));
            await socket.SendAsync(msgbuf, WebSocketMessageType.Text, endOfMessage: true, socketToken);
        }

        
    }
}
