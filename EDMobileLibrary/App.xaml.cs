﻿using EDDMobile.Comms;
using EDDMobileImpl.Views;
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

            MainPage = new MainPage();

        }

        protected async override void OnStart()
        {
            EDDiscovery.Icons.IconSet.ResetIcons();     // start with a clean slate loaded up from default icons
            WebSocket = new WebSocketWrapper();
            //TODO: Configure...
            await WebSocket.Connect("ws://192.168.0.32/eddmobile");
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
