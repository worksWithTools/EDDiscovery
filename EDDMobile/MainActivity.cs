using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using EliteDangerousCore;
using System;
using System.Collections.ObjectModel;
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
        private ClientWebSocket webSocket;
        private EditText edduri;
        private TextView comms;
        private Button connect;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            //// Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            EDDiscovery.Icons.IconSet.ResetIcons();     // start with a clean slate loaded up from default icons
            
            var listView = FindViewById<ListView>(Resource.Id.journalListView);
            listView.Adapter = new JournalViewAdapter(this, journalEntries);

            connect = FindViewById<Button>(Resource.Id.connectBtn);
            connect.Click += Connect_Click;
            edduri = FindViewById<EditText>(Resource.Id.eddUri);
            comms = FindViewById<TextView>(Resource.Id.comms);
        }

        private async void Connect_Click(object sender, EventArgs e)
        {
            await Connect(edduri.Text);
            if (webSocket.State == WebSocketState.Open)
                comms.Text = "Connected";

            //TODO: we'll probably want a better handshake than this!
            await Send(".ready");

            await Listen();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public async Task Connect(string uri)
        {
            webSocket = null;

            try
            {
                webSocket = new ClientWebSocket();

                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }

        }

        private async Task Send(string message)
        {
            var random = new Random();
            var bytes = Encoding.ASCII.GetBytes(message);

            var arraySegment = new ArraySegment<byte>(bytes);

            await webSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        private async Task Listen()
        {
            const int buffer_1mb = 16 * 1024;
            byte[] buffer = new byte[buffer_1mb];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    edduri.Text = "Done";
                }
                else
                {
                    string message = Encoding.ASCII.GetString(buffer);
                    comms.Text += $"\n{message}";
                    //LogStatus(true, buffer, result.Count);
                    journalEntries.AddEntry(message);
                }
            }
        }
    }
}