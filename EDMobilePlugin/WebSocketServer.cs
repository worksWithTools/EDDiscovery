using BaseUtils.Misc;
using EDPlugin;
using EliteDangerous.DB;
using EliteDangerousCore;
using EliteDangerousCore.JournalEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
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

        public WebSocketServer(int socketId, HttpListenerWebSocketContext context, CancellationToken token)
        {
            this.socketId = socketId;
            socket = context.WebSocket;
            Token = token;
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
                        if (message == WebSocketMessage.INIT_DB)
                        {
                            await SendInitDataToMobile();
                        }
                        else if (message.StartsWith(WebSocketMessage.SYNCLASTEVENT))
                        {
                            await PushLastEventToMobile(message);
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

        private async Task PushLastEventToMobile(string message)
        {
            //todo: get last event from message
            var lastIdStr = message.Split(':')[1];
            long lastId = Int64.Parse(lastIdStr);
            var lastevent = JournalEntry.GetLastEvent(EDCommander.CurrentCmdrID);
            if (lastevent.Id > lastId)
            {
                var entriesToSend = JournalEntryClass.GetJournalEntries(lastId);
                int msgToSend = entriesToSend.Count;
                MobileWebResponse response = new MobileWebResponse(WebSocketMessage.SYNCLASTEVENT);
                response.Responses.AddRange(from e in entriesToSend
                                            select JsonConvert.SerializeObject(e));
                
                await SendMessageToSocketAsync(socketId, socket, response, Token);
            }
            else
            {
                Debug.WriteLine($"Socket {socketId}: Last Entry Id was: {lastevent.Id}");
                MobileWebResponse response = new MobileWebResponse(WebSocketMessage.DONE);
                await SendMessageToSocketAsync(socketId, socket, response, Token);
            }
        }

        private async Task SendInitDataToMobile()
        {
            var backupPath = $"{EliteDangerousCore.EliteConfigInstance.InstanceOptions.UserDatabasePath}.bak";
            try
            {
                using (var source = new SQLiteConnection($"Data Source={EliteDangerousCore.EliteConfigInstance.InstanceOptions.UserDatabasePath};"))
                using (var destination = new SQLiteConnection($"Data Source={backupPath};"))
                {
                    source.Open();
                    destination.Open();
                    source.BackupDatabase(destination, "main", "main", -1, null, 0);
                }
                using (var destination = new SQLiteConnection($"Data Source={backupPath};"))
                {
                    destination.Open();
                    //TODO: add configurable range for trimming
                    await TrimJournalEntries(destination);
                }

                var dbcontent = File.ReadAllBytes(backupPath);
                Debug.WriteLine($"Socket {socketId}: Sending trimmed UserDb.");
                var msgbuf = new ArraySegment<byte>(dbcontent);
                await socket.SendAsync(msgbuf, WebSocketMessageType.Binary, endOfMessage: true, Token);

            }
            finally
            {
                File.Delete(backupPath);
            }
        }
        //TODO: this really doesn't belong here!
        private async Task TrimJournalEntries(SQLiteConnection destination)
        {
            try
            {
                var firstEntry = JournalEntry.GetLast<JournalFSDJump>(EDCommander.CurrentCmdrID, DateTime.UtcNow);
                string[] commands = new[]
                {
                    $"DELETE FROM JournalEntries WHERE Id < {firstEntry.Id}",
                    "vacuum"
                };
                foreach (var command in commands)
                {
                    using (DbCommand cmd = destination.CreateCommand())
                    {
                        cmd.CommandText = command;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e);
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

            var msgbuf = new ArraySegment<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(response)));
            await socket.SendAsync(msgbuf, WebSocketMessageType.Text, endOfMessage: true, socketToken);
        }


    }
}
