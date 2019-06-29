
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using EDDMobileImpl;
using ImageCircle.Forms.Plugin.Droid;
using UXDivers.Gorilla.Droid;

namespace EDDMobile.Droid
{
    [Activity(Label = "EDDMobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();
#if GORILLA
            // this normally happens in App.OnSTart()
            EDDiscovery.Icons.IconSet.ResetIcons();     // start with a clean slate loaded up from default icons

            LoadApplication(Player.CreateApplication(
                this,
                new UXDivers.Gorilla.Config("worksWithToolsLaptop")
                .RegisterAssemblyFromType<SkiaSharp.SKImage>()
                .RegisterAssemblyFromType<EDMobileLibrary.ViewModels.JournalEntryViewModel>()
                .RegisterAssemblyFromType<ImageCircleRenderer>()
                )
            );

#else
            LoadApplication(new App());
#endif
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}