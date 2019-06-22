using EDDMobileImpl.Views;
using Xamarin.Forms;

namespace EDDMobileImpl
{
    public partial class App : Application
    {
    
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            EDDiscovery.Icons.IconSet.ResetIcons();     // start with a clean slate loaded up from default icons
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
