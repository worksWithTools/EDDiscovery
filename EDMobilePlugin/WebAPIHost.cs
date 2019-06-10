using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDMobilePlugin
{
 
    class WebAPIHost
    {
        const string webHookSenderBaseAddress = "http://localhost:9002";

        private HttpClient client;
        private SemaphoreSlim signal = new SemaphoreSlim(0, 1);
        public async Task StartListeningAsync()
        {
            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = true
            };

            // Start OWIN host 
            using (WebApp.Start<Startup>(webHookSenderBaseAddress))
            using (client = new HttpClient(handler))
            {
                Debug.WriteLine("Webhook sender up and running. Waiting for webhook receiver to register");

                await signal.WaitAsync();
            }
            Debug.WriteLine("Webhook sender is no longer listening.");
        }

        public void StopListening()
        {
            Debug.WriteLine("Asking Webhook sender to stop listening.");
            signal?.Release();
        }

        public async Task PostEventAsync()
        {
            // trigger a webhook call
            await client.PostAsJsonAsync($"{webHookSenderBaseAddress}/api/messages", new Message { Sender = "WebApiHost", Body = "Hello From Sender" });
        }

    }
}
