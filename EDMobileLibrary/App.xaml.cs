using EDDMobile.Comms;
using EDDMobileImpl.Views;
using EDMobileLibrary.Services;
using EliteDangerousCore;
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

        protected async override void OnStart()
        {
            // TODO: allow it to timeout connecting...
            await WebSocket.Connect();

            await UserDataCache.Initialise();

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
