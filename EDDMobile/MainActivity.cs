using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using EliteDangerousCore;
using System.Linq;

namespace EDDMobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            //// Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            EDDiscovery.Icons.IconSet.ResetIcons();     // start with a clean slate loaded up from default icons
            var journalEntries = new JournalEntryRepository().journalCollection;
            var listView = FindViewById<ListView>(Resource.Id.journalListView);
            listView.Adapter = new JournalViewAdapter(this, journalEntries.ToList());
                //new ArrayAdapter<JournalEntry>(this, Android.Resource.Layout.SimpleListItem1, journalEntries.ToArray());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}