using EDDMobileImpl.Services;
using EDPlugin;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using JournalEntry = EDPlugin.EDDDLLIF.JournalEntry;

namespace EDDMobileImpl.ViewModels
{
    public class StatusViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; private set; }
        
        public string WhereAmI { get => lastJournalEntry.whereami; set => SetProperty(ref lastJournalEntry.whereami, value); }
        public string SystemName { get => lastJournalEntry.systemname; set => SetProperty(ref lastJournalEntry.systemname, value); }
        public string ShipType { get => lastJournalEntry.shiptype; set => SetProperty(ref lastJournalEntry.shiptype, value); }
        public long Credits { get => lastJournalEntry.credits; set => SetProperty(ref lastJournalEntry.credits, value); }

        public StatusViewModel()
        {
            Title = "Status";
            LoadItemsCommand = new Command(async () => await ExecuteLoadJournalEntriesCommand());
        }

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
                var msg = await App.WebSocket.ListenForMessage();

                RefreshStatus(msg);

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
            var lastEntry = JsonConvert.DeserializeObject<JournalEntry>(msg);
            PropertyCopier<JournalEntry, StatusViewModel>.Copy(lastEntry, this);
        }

        JournalEntry lastJournalEntry;
    }
}