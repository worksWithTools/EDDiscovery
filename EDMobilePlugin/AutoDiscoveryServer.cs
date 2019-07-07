using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EDMobilePlugin
{
    public static class AutoDiscoveryServer
    {
        public const string EDMOBILECQ = "EDDMOBCQ";
        private static bool running = false;
        public  static void Start()
        {
            Task.Run(() =>
            {
                Debug.WriteLine("AUTODISCSVR: listening for connections...");
                running = true;
                do
                {
                    var RemoteEp = new IPEndPoint(IPAddress.Any, 0);
                    var LocalEp = new IPEndPoint(IPAddress.Any, 8888);
                    var Server = new UdpClient( LocalEp );
                    var ClientRequestData = Server.Receive(ref RemoteEp);
                    var ClientRequest = Encoding.ASCII.GetString(ClientRequestData);

                    Debug.WriteLine("AUTODISCSVR: Received {0} from {1}", ClientRequest, RemoteEp.Address.ToString());
                    if (ClientRequest == EDMOBILECQ)
                    {
                        Debug.WriteLine($"AUTODISCSVR: Sending ACK response to {RemoteEp.Address.ToString()}" );
                        var ResponseData = Encoding.ASCII.GetBytes("ACK");
                        try
                        {
                            Server.Send(ResponseData, ResponseData.Length, RemoteEp);
                        }
                        catch (SocketException e)
                        {
                            Debug.WriteLine("AUTODISCSVR: error: " + e);
                        }
                    }

                } while (running);
            });
        }

        public static void Stop()
        {
            running = false;
            Debug.WriteLine("Autodiscover stopping...");
        }

    }
}
