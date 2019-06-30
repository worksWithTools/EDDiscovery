using EDDMobileImpl.Services;
using EDPlugin;
using EliteDangerousCore;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EDDMobileImpl.ViewModels
{
    public class StatusViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; private set; }
        public StatusViewModel()
        {
            Title = "Status";
            LoadItemsCommand = new Command(async () => await ExecuteLoadJournalEntriesCommand());
        }

        private HistoryEntry lastEntry;

        protected override void WebSocket_OnMessage()
        {
            App.WebSocket.TryGetMessage(out string msg);
            RefreshStatus(msg);
        }

       
        async Task ExecuteLoadJournalEntriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await App.WebSocket.Send(WebSocketMessage.REFRESH_STATUS);
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

        private void RefreshStatus(string msg)
        {
            //TODO: improve the deserialization.
            
            var lastEntry = JsonConvert.DeserializeObject<HistoryEntry>(msg);
            if (lastEntry != null)
            {
                this.lastEntry = lastEntry;
                PropertyNotifier<HistoryEntry, StatusViewModel>.NotifyAllChanges(this.lastEntry, this);
            }

        }

        public long Credits {
            get => lastEntry?.Credits ?? 0;
        }

    }
}