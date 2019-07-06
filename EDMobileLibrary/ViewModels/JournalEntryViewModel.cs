using EDDMobileImpl;
using EDDMobileImpl.ViewModels;
using EDPlugin;
using EliteDangerous.JSON;
using EliteDangerousCore;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EDMobileLibrary.ViewModels
{
    public class JournalEntryViewModel : BaseViewModel
    {
        ObservableCollection<JournalEntry> items;
        public Command LoadItemsCommand { get; private set; }
        public ObservableCollection<JournalEntry> Items { get => items;  private set => SetProperty(ref items, value); }

        public JournalEntryViewModel() : base()
        {
            Title = "Log";
            Items = new ObservableCollection<JournalEntry>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadHistoryCommand());
        }
        protected override void WebSocket_OnMessage()
        {
            try
            {
                App.WebSocket.TryGetMessage(out string msg);
                MobileWebResponse response = msg.Deserialize<MobileWebResponse>();
                Debug.WriteLine($"INFO: msg received: {response.RequestType}");
                if (response.RequestType == WebSocketMessage.GET_JOURNAL)
                {
                    JournalEntry entry = JsonConvert.DeserializeObject<JournalEntry>(response.Responses[0], new JsonConverter[] { new JournalEntryConverter() });
                    items.Add(entry);
                }
                else if (response.RequestType == WebSocketMessage.BROADCAST)
                {
                    JournalEntry entry = JsonConvert.DeserializeObject<JournalEntry>(response.Responses[0], new JsonConverter[] { new JournalEntryConverter() });
                    items.Insert(0, entry);
                }

            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private async Task ExecuteLoadHistoryCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                items.Clear();
                await App.WebSocket.Send(WebSocketMessage.GET_JOURNAL);
                Items = new ObservableCollection<JournalEntry>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
