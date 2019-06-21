
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDDMobile.Comms
{
    //TODO: extract an interface and make this work in the DependencyService
    public class WebSocketWrapper
    {
        public WebSocketWrapper(){
            webSocket = new ClientWebSocket();

        }
        private ClientWebSocket webSocket;
        private ConcurrentQueue<string> messages = new ConcurrentQueue<string>();

        public delegate void OnMessageHandler();
        //Note: we're not passing the actual message out of the delegate
        // because its possible the queue could get swamped.
        public event OnMessageHandler OnMessage;

        public async Task Connect(string uri)
        {
           try
            {
                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }

        }

        public async Task Disconnect()
        {
            try
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing socket", CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }
        }

       
        public bool TryGetMessage(out string msg)
        {
            return messages.TryDequeue(out msg);
        }

        public async Task Send(string message)
        {
            var bytes = Encoding.ASCII.GetBytes(message);

            var arraySegment = new ArraySegment<byte>(bytes);

            await webSocket.SendAsync(arraySegment, WebSocketMessageType.Binary, true, CancellationToken.None);
        }

        async Task<(WebSocketReceiveResult, IEnumerable<byte>)> ReceiveFullMessage(CancellationToken cancelToken)
        {
            WebSocketReceiveResult response = null;
            var message = new List<byte>();
            try
            {
                do
                {
                    var buffer = new byte[4096];
                    response = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancelToken);
                    message.AddRange(new ArraySegment<byte>(buffer, 0, response.Count));
                } while (!response.EndOfMessage);
            
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                if (e.InnerException != null) Debug.WriteLine(e.InnerException.Message);
            }
            return (response, message);
        }


        public async Task Listen()
        {
            // TODO: put a CancelToken in here...
            while (webSocket.State == WebSocketState.Open)
            {
                var msg = await ListenForMessage();
                messages.Enqueue(msg);
                OnMessage?.Invoke();

            }
        }

        public async Task<string> ListenForMessage()
        {
            if (webSocket.State == WebSocketState.Open)
            {
                var result = await ReceiveFullMessage(CancellationToken.None);
                if (result.Item1.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    string message = Encoding.ASCII.GetString(result.Item2.ToArray());
                    return message;
                }
            }
            return "";
        }
    }

}