
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDDMobile.Comms
{
    public class WebSocketWrapper
    {
        private ClientWebSocket webSocket;
        private ConcurrentQueue<string> messages = new ConcurrentQueue<string>();

        public delegate void OnMessageHandler();

        //Note: we're not passing the actual message out of the delegate
        // because its possible the queue could get swamped.
        public event OnMessageHandler OnMessage;

        public async Task Connect(string uri)
        {
            webSocket = null;

            try
            {
                webSocket = new ClientWebSocket();

                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }

        }

        public async Task Listen()
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await ReceiveFullMessage(CancellationToken.None);
                if (result.Item1.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    string message = Encoding.ASCII.GetString(result.Item2.ToArray());
                    messages.Enqueue(message);
                    OnMessage?.Invoke();

                    //LogStatus(true, buffer, result.Count);
                    //journalEntries.AddEntry(message);
                }
            }
        }

        public bool TryGetMessage(out string msg)
        {
            return messages.TryDequeue(out msg);
        }

        async Task<(WebSocketReceiveResult, IEnumerable<byte>)> ReceiveFullMessage(CancellationToken cancelToken)
        {
            WebSocketReceiveResult response;
            var message = new List<byte>();
            var buffer = new byte[4096];
            do
            {
                response = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancelToken);
                message.AddRange(new ArraySegment<byte>(buffer, 0, response.Count));
            } while (!response.EndOfMessage);
            return (response, message);
        }

        public async Task Send(string message)
        {
            var random = new Random();
            var bytes = Encoding.ASCII.GetBytes(message);

            var arraySegment = new ArraySegment<byte>(bytes);

            await webSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
        }


    }

}