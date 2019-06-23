﻿using BaseUtils.Misc;
using EDPlugin;
using EliteDangerousCore;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDMobilePlugin
{
    public static class WebSocketServer
    {
        private const int PREVIEW_LENGTH = 50;
        private static HttpListener Listener;
        private static CancellationTokenSource TokenSource;
        private static CancellationToken Token;
        private static EDDDLLIF.EDDCallBacks _callbacks;
        private static ManagedCallbacks _managedCallbacks;
        private static int SocketCounter = 0;
        private static ConcurrentDictionary<int, AutoResetEvent> broadcastAvailable = new ConcurrentDictionary<int, AutoResetEvent>();

        // The dictionary key corresponds to active socket IDs, and the BlockingCollection wraps
        // the default ConcurrentQueue to store broadcast messages for each active socket.
        private static ConcurrentDictionary<int, BlockingCollection<string>> BroadcastQueues = new ConcurrentDictionary<int, BlockingCollection<string>>();

        public static void Start(string uriPrefix, EDDDLLIF.EDDCallBacks callbacks, ManagedCallbacks managedCallbacks)
        {
            _callbacks = callbacks;
            _managedCallbacks = managedCallbacks;
            TokenSource = new CancellationTokenSource();
            Token = TokenSource.Token;
            Listener = new HttpListener();
            Listener.Prefixes.Add(uriPrefix);
            Listener.Start();
            if (Listener.IsListening)
            {
                Debug.WriteLine("Connect browser for a basic echo-back web page.");
                Debug.WriteLine($"Server listening: {uriPrefix}");
                // listen on a separate thread so that Listener.Stop can interrupt GetContextAsync
                Task.Run(() => Listen().ConfigureAwait(false));
            }
            else
            {
                Debug.WriteLine("Server failed to start.");
            }
        }

        public static void Stop()
        {
            if (Listener?.IsListening ?? false)
            {
                TokenSource.Cancel();
                Debug.WriteLine("\nServer is stopping.");
                Listener.Stop();
                Listener.Close();
                TokenSource.Dispose();
            }
        }

        public static void Broadcast(string message)
        {
            Debug.WriteLine($"Broadcast request received: {message.Left(PREVIEW_LENGTH)}...");
            foreach (var kvp in BroadcastQueues)
            {
                SendToQueue(kvp.Key, message);
            }
        }

        public static void SendToQueue(int socketId, string message)
        {
            BroadcastQueues[socketId].Add(message);
            broadcastAvailable[socketId].Set();
        }

        private static async Task Listen()
        {
            while (!Token.IsCancellationRequested)
            {
                HttpListenerContext context = await Listener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    // HTTP is only the initial connection; upgrade to a client-specific websocket
                    HttpListenerWebSocketContext wsContext = null;
                    try
                    {
                        wsContext = await context.AcceptWebSocketAsync(subProtocol: null);
                        int socketId = Interlocked.Increment(ref SocketCounter);
                        Debug.WriteLine($"Socket {socketId}: New connection.");
                        _ = Task.Run(() => ProcessWebSocket(wsContext, socketId).ConfigureAwait(false));
                    }
                    catch (Exception)
                    {
                        // server error if upgrade from HTTP to WebSocket fails
                        context.Response.StatusCode = 500;
                        context.Response.Close();
                        return;
                    }
                }
                else
                {
                    {
                        context.Response.StatusCode = 404;
                    }
                    context.Response.Close();
                }
            }
        }

        private static async Task ProcessWebSocket(HttpListenerWebSocketContext context, int socketId)
        {
            var socket = context.WebSocket;

            BroadcastQueues.TryAdd(socketId, new BlockingCollection<string>());
            broadcastAvailable.TryAdd(socketId, new AutoResetEvent(false));
            var broadcastTokenSource = new CancellationTokenSource();
            _ = Task.Run(() => WatchForBroadcasts(socketId, socket, broadcastTokenSource.Token));

            try
            {
                byte[] buffer = new byte[4096];
                while (socket.State == WebSocketState.Open && !Token.IsCancellationRequested)
                {
                    var receiveResult = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), Token);
                    if (receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        Debug.WriteLine($"Socket {socketId}: Closing websocket.");
                        broadcastTokenSource.Cancel();
                        BroadcastQueues.TryRemove(socketId, out _);
                        broadcastAvailable.TryRemove(socketId, out _);
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", Token);
                    }
                    else
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, receiveResult.Count);
                        Debug.WriteLine($"Socket {socketId}: Received [{message.Left(PREVIEW_LENGTH)}]).");

                        // TODO: Apply a command pattern?
                        // if message is "ready" then send back some data..
                        if (message == WebSocketMessage.REFRESH_STATUS)
                            //TODO: replace this with the last HISTORY 
                            _managedCallbacks?.RequestRefresh();
                        else if (message.StartsWith(WebSocketMessage.GET_JOURNAL))
                        {
                            //TODO: add number to get...
                            var history = _managedCallbacks?.GetHistory(10);
                            foreach (var entry in history)
                            {
                                Debug.WriteLine($"Socket {socketId} Queueing : {entry.journalEntry.ToString()}");
                                var msg = JsonConvert.SerializeObject(entry.journalEntry);
                                await SendMessageToSocketAsync(socketId, socket, msg, Token);
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
                BroadcastQueues?.TryRemove(socketId, out _);
                broadcastAvailable?.TryRemove(socketId, out _);
            }
        }

        private static async Task WatchForBroadcasts(int socketId, WebSocket socket, CancellationToken socketToken)
        {
            var waitHandles = new[] { broadcastAvailable[socketId], socketToken.WaitHandle };
            while (!socketToken.IsCancellationRequested)
            {
                try
                {
                    // use async waiting
                    var waitEvent = broadcastAvailable[socketId];
                    if (await waitEvent.WaitOneAsync(socketToken))
                    {
                        Debug.WriteLine($"Broadcast received by socket {socketId}");
                        if (BroadcastQueues[socketId].TryTake(out var message))
                        {
                            await SendMessageToSocketAsync(socketId, socket, message, socketToken);
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

        private static async Task SendMessageToSocketAsync(int socketId, WebSocket socket, string message, CancellationToken socketToken)
        {
            Debug.WriteLine($"Socket {socketId}: Sending next broadcast from queue: [{message.Left(PREVIEW_LENGTH)}...]");
            var msgbuf = new ArraySegment<byte>(Encoding.ASCII.GetBytes(message));
            await socket.SendAsync(msgbuf, WebSocketMessageType.Text, endOfMessage: true, socketToken);
        }
    }
}