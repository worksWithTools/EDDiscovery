using EDDMobileImpl.ViewModels;
using EDPlugin;
using EliteDangerous.JSON;
using EliteDangerousCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public ObservableCollection<JournalEntry> Items { get => items;  private set => items = value; }

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
            WebSocket.TryGetMessage(out string msg);
            Debug.WriteLine($"INFO: msg received: {msg.Length}");
            JournalEntry entry = JsonConvert.DeserializeObject<JournalEntry>(msg, new JsonConverter[] { new JournalEntryConverter() });
            items.Add(entry);

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
                await WebSocket.Send(WebSocketMessage.GET_JOURNAL);

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
