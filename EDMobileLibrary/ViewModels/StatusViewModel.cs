using EDPlugin;
using EliteDangerous.JSON;
using EliteDangerousCore;
using EliteDangerousCore.JournalEvents;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using static BaseUtils.CSVRead;

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


        protected override void WebSocket_OnMessage()
        {
            App.WebSocket.TryGetMessage(out string msg);
            MobileWebResponse response = msg.Deserialize<MobileWebResponse>();
            if (response.RequestType == WebSocketMessage.REFRESH_STATUS || response.RequestType == WebSocketMessage.BROADCAST)
                RefreshStatus(response);
            else // pass up to base handler
                base.WebSocket_OnMessage();
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

        private void RefreshStatus(MobileWebResponse msg)
        {
            var lastEntry = HistoryEntry.FromJSON(msg.Responses[0]);

            if (msg.RequestType == WebSocketMessage.REFRESH_STATUS)
            {
                var lastSystem = msg.Responses[1].Deserialize<JournalFSDJump>();

                Allegiance = lastSystem?.Allegiance;
                PrimaryEconomy = lastSystem?.Economy_Localised;
                Government = lastSystem?.Government_Localised;
                State = lastSystem?.FactionState.SplitCapsWord();
                Security = lastSystem?.Security_Localised;
            }
            if (lastEntry != null)
            {
                this.lastEntry = lastEntry;
            }

            OnPropertyChanged(null); // should result in all props being refreshed.

        }

        public String WhereAmI {
            get => lastEntry?.WhereAmI ?? "Unknown";
        }
        public String Note
        {
            get => lastEntry?.snc?.Note ?? "";
        }
        public ISystem System { get => lastEntry?.System; }
        public ShipInformation ShipInformation { get => lastEntry?.ShipInformation; }

        public String Fuel { get => String.Format($"{ShipInformation?.FuelLevel} / {ShipInformation?.FuelCapacity ?? 1}"); }
        public int MaterialsCount { get => lastEntry?.MaterialCommodity.MaterialsCount ?? 0; }
        public int CargoCount { get => lastEntry?.MaterialCommodity.CargoCount ?? 0; }
        
        public int DataCount { get => lastEntry?.MaterialCommodity.DataCount ?? 0; }
        public string Allegiance { get; private set; }
        public string PrimaryEconomy { get; private set; }
        public string Government { get; private set; }
        public string State { get; private set; }
        public string Security { get; private set; }

        private HistoryEntry lastEntry;
    }
}