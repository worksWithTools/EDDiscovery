using EDDMobile.Comms;
using EDDMobileImpl.Views;
using EDMobileLibrary.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EDDMobileImpl
{
    public partial class App : Application
    {
        public static WebSocketWrapper WebSocket { get; private set; }


        public App()
        {
            InitializeComponent();
            _ = EDDMobileConfig.Instance; // just to initialise - for now
            MainPage = new MainPage();

        }

        protected async override void OnStart()
        {
            EDDiscovery.Icons.IconSet.ResetIcons();     // start with a clean slate loaded up from default icons
            WebSocket = new WebSocketWrapper();
            await WebSocket.Connect();
            Debug.WriteLine($"MOBILE: Starting listening for broadcasts on {WebSocket.Uri}");
            await WebSocket.Listen();
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
