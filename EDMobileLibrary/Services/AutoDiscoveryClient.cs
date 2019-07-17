using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        
        public delegate void EndPointDiscoveredEventHandler(object sender, EndPointDiscoveredEventArgs e);
        public static event EndPointDiscoveredEventHandler EndPointDiscovered;

        public delegate void EndPointDiscoveryTimeoutHandler();
        public static event EndPointDiscoveryTimeoutHandler EndPointTimeout;

        public static string ServerAddress { get; private set; } = String.Empty;
        public async static Task StartAutodiscovery()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(30000);
            
            try
            {
                await Task.Run(() =>
                    {
                        while (ServerAddress == String.Empty && !cts.IsCancellationRequested)
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

                        }
                    }, cts.Token);
                if (cts.IsCancellationRequested)
                    OnEndPointTimeout();
            }
            catch (OperationCanceledException ce)
            {
                Debug.WriteLine($"Discovery timeout: {ce}");
                OnEndPointTimeout();
            }
        }

        private static void OnEndpointDiscovered(IPAddress endpoint)
        {
            ServerAddress = endpoint.ToString();
            var epdisc = EndPointDiscovered;
            if (epdisc == null)
                return;

            epdisc.Invoke(null, new EndPointDiscoveredEventArgs(endpoint));
        }

        private static void OnEndPointTimeout()
        {
            var ept = EndPointTimeout;
            if (ept == null)
                return;

            ept.Invoke();
        }
    }


}
