using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ToastMessage;
using Xamarin.Forms;

namespace EDMobileLibrary.Services
{
    public class EndPointDiscoveredEventArgs : EventArgs
    {
  
        public EndPointDiscoveredEventArgs(IPAddress endpointIP)
        {
            EndpointAddress = endpointIP;
        }

     
        public virtual IPAddress EndpointAddress { get; private set; }
    }
    public static class AutoDiscoveryClient
    {
        public static readonly string EDMOBILECQ = "EDDMOBCQ";
        public static readonly TimeSpan DiscoveryTimeout = new TimeSpan(0, 0, 30);

        public delegate void EndPointDiscoveredEventHandler(object sender, EndPointDiscoveredEventArgs e);
        public static event EndPointDiscoveredEventHandler EndPointDiscovered;
        public static string ServerAddress { get; private set; } = String.Empty;
        public async static Task StartAutodiscovery()
        {
            await Task.Run(async () =>
            {
                //TODO: Timeout
                while (ServerAddress == String.Empty)
                {
                    using (var udp = new UdpClient())
                    {
                        Debug.WriteLine($"MOBILE: looking for EDD Server...");
                        DependencyService.Get<Toast>().Show("Looking for EDD Server");

                        var RequestData = Encoding.ASCII.GetBytes(EDMOBILECQ);
                        var ServerEp = new IPEndPoint(IPAddress.Any, 0);
                        try
                        {
                            udp.EnableBroadcast = true;
                            udp.Client.ReceiveTimeout = 5000;
                            udp.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, 8888));

                            var result = udp.Receive(ref ServerEp);

                            var ServerResponse = Encoding.ASCII.GetString(result);

                            Debug.WriteLine("MOBILE: Received {0} from {1}", ServerResponse, ServerEp.Address.ToString());
                            if (ServerResponse.StartsWith("ACK"))
                                OnEndpointDiscovered(ServerEp.Address);
                        }
                        catch (SocketException e)
                        {
                            Debug.WriteLine("MOBILE: Autodiscovery timeout. Retrying..." + e);
                        }
                    }

                    await Task.Delay(3000);
                }
            });
        }

        private static void OnEndpointDiscovered(IPAddress endpoint)
        {
            ServerAddress = endpoint.ToString();
            var epdisc = EndPointDiscovered;
            if (epdisc == null)
                return;

            epdisc.Invoke(null, new EndPointDiscoveredEventArgs(endpoint));
        }
    }


}
