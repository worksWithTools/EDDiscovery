using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using EliteDangerousCore;

namespace EDDMobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            string entry = @"{ 'timestamp':'2019 - 05 - 26T18: 00:17Z', 'event':'Progress', 'Combat':97, 'Trade':26, 'Explore':58, 'Empire':8, 'Federation':48, 'CQC':46 }";
            JournalEntry je = JournalEntry.CreateJournalEntry(entry);

            string info, details;
            je.FillInformation(out info, out details);

            var journal = FindViewById<TextView>(Resource.Id.journalEntry);
            journal.Text = info + "\n" + details;

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}