using Android.App;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using EDDMobile.Comms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDDMobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private JournalEntryRepository journalEntries = new JournalEntryRepository();
        private WebSocketWrapper webSocket = new WebSocketWrapper();
        private EditText edduri;
        private TextView jsonview;
        private Button connect;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            //// Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            EDDiscovery.Icons.IconSet.ResetIcons();     // start with a clean slate loaded up from default icons
            
            //var listView = FindViewById<ListView>(Resource.Id.journalListView);
            
            //listView.Adapter = new JournalViewAdapter(this, journalEntries.journalCollection);
    
            connect = FindViewById<Button>(Resource.Id.connectBtn);
            connect.Click += Connect_Click;
            edduri = FindViewById<EditText>(Resource.Id.eddUri);
            jsonview = FindViewById<TextView>(Resource.Id.jsonview);
        }

        private async void Connect_Click(object sender, EventArgs e)
        {
            await webSocket.Connect(edduri.Text);

            //TODO: we'll probably want a better handshake than this!
            //await Send("refresh");
            webSocket.OnMessage += WebSocket_OnMessage;
            await webSocket.Listen();
        }

        private void WebSocket_OnMessage()
        {
            // of course, this can be extended to be while....
            if (webSocket.TryGetMessage(out string msg))
                jsonview.Text = msg;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

  

   

       
    }


}