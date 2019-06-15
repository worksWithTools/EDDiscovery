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
        private static HttpListener Listener;
        private static CancellationTokenSource TokenSource;
        private static CancellationToken Token;

        private static int SocketCounter = 0;

        // The dictionary key corresponds to active socket IDs, and the BlockingCollection wraps
        // the default ConcurrentQueue to store broadcast messages for each active socket.
        private static ConcurrentDictionary<int, BlockingCollection<string>> BroadcastQueues = new ConcurrentDictionary<int, BlockingCollection<string>>();

        public static void Start(string uriPrefix)
        {
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
            Debug.WriteLine($"Broadcast: {message}");
            foreach (var kvp in BroadcastQueues)
                kvp.Value.Add(message);
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
            var broadcastTokenSource = new CancellationTokenSource();
            _ = Task.Run(() => WatchForBroadcasts(socketId, socket, broadcastTokenSource.Token));

            try
            {
                byte[] buffer = new byte[4096];
                while (socket.State == WebSocketState.Open && !Token.IsCancellationRequested)
                {
                    var receiveResult = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), Token);
                    Debug.WriteLine($"Socket {socketId}: Received {receiveResult.MessageType} frame ({receiveResult.Count} bytes).");
                    if (receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        Debug.WriteLine($"Socket {socketId}: Closing websocket.");
                        broadcastTokenSource.Cancel();
                        BroadcastQueues.TryRemove(socketId, out _);
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", Token);
                    }
                    else
                    {
                        Debug.WriteLine($"Socket {socketId}: Echoing data to queue.");
                        string message = Encoding.ASCII.GetString(buffer, 0, receiveResult.Count);
                        BroadcastQueues[socketId].Add(message);
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
            }
        }

        private const int BROADCAST_WAKEUP_INTERVAL = 250; // milliseconds

        private static async Task WatchForBroadcasts(int socketId, WebSocket socket, CancellationToken socketToken)
        {
            while (!socketToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(BROADCAST_WAKEUP_INTERVAL, socketToken);
                    if (!socketToken.IsCancellationRequested && BroadcastQueues[socketId].TryTake(out var message))
                    {
                        Debug.WriteLine($"Socket {socketId}: Sending from queue.");
                        var msgbuf = new ArraySegment<byte>(Encoding.ASCII.GetBytes(message));
                        await socket.SendAsync(msgbuf, WebSocketMessageType.Text, endOfMessage: true, socketToken);
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
    }
}