using BaseUtils.Misc;
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
    public static class WebSocketHttpServer
    {
        private const int PREVIEW_LENGTH = 50;

        private static HttpListener Listener;
        private static CancellationTokenSource TokenSource;
        private static CancellationToken Token;
        private static EDDDLLIF.EDDCallBacks _callbacks;
        private static int SocketCounter = 0;
        
        // The dictionary key corresponds to active socket IDs, and the BlockingCollection wraps
        // the default ConcurrentQueue to store broadcast messages for each active socket.
        private static ConcurrentDictionary<int, WebSocketServer> WebSocketConnections = new ConcurrentDictionary<int, WebSocketServer>();

        public static void Start(string uriPrefix, EDDDLLIF.EDDCallBacks callbacks)
        {
            _callbacks = callbacks;
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
                Task.Run(() => ListenForWebSocketConnectRequests().ConfigureAwait(false));
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
            foreach (var kvp in WebSocketConnections)
            {
                SendToQueue(kvp.Key, message);
            }
        }

        public static void SendToQueue(int socketId, string message)
        {
            WebSocketConnections[socketId].QueueMessage(message);
        }

        private static async Task ListenForWebSocketConnectRequests()
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
            try
            {
                Debug.WriteLine($"Creating new WebSocketServer for socket {socketId}");
                WebSocketServer server = new WebSocketServer(socketId, context, Token);
                WebSocketConnections.TryAdd(socketId, server);

                await server.ReceiveMessagesAsync();
            }
            finally
            {
                WebSocketConnections.TryRemove(socketId, out _);
            }
        }

        
    }
}