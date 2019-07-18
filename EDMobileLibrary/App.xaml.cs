using EDDMobile.Comms;
using EDDMobileImpl.Views;
using EDMobileLibrary.Services;
using EDPlugin;
using EliteDangerous.DB;
using EliteDangerous.JSON;
using EliteDangerousCore;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EDDMobileImpl
{
    public partial class App : Application
    {
        public static WebSocketWrapper WebSocket { get; private set; } = new WebSocketWrapper();
        public static IEliteOptions Options { get; private set; }
        public static IEliteConfig Config { get; private set; }
        public App()
        {
            InitializeComponent();
            Config = EDDMobileConfig.Instance; 
            Options = EDDMobileOptions.Instance;
            MainPage = new MainPage();

            EDDiscovery.Icons.IconSet.ResetIcons();     // start with a clean slate loaded up from default icons
        }

        private void WebSocket_OnMessage()
        {
            try
            {
                WebSocket.TryGetMessage(out string msg);
                MobileWebResponse response = msg.Deserialize<MobileWebResponse>();
                if (response == null)
                    return;
                Debug.WriteLine($"MOBILE:: Received message {response.RequestType} containing {response.Responses.Count} entries.");
                UserDataCache.StoreMessage(response);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        protected async override void OnStart()
        {
            // TODO: we'll need a dialog for this...
            await UserDataCache.Initialise();
            WebSocket.OnMessage += WebSocket_OnMessage;

            if (await WebSocket.Connect())
            {
                Debug.WriteLine($"MOBILE: Starting listening for broadcasts on {WebSocket.Uri}");
                await WebSocket.Listen();
            }
            //TODO: implement a reconnect mechanism
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
