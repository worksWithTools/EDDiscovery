using EDMobileLibrary.Services;
using EDPlugin;
using EliteDangerous.JSON;
using EliteDangerousCore;
using EliteDangerousCore.DB;
using EliteDangerousCore.JournalEvents;
using Newtonsoft.Json;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
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
            LoadItemsCommand = new Command(ExecuteLoadJournalEntriesCommand);
            UserDataCache.OnHistoryLoaded += ExecuteLoadJournalEntriesCommand;
        }

        void ExecuteLoadJournalEntriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                shipLoadout = UserDataCache.History?.CurrentShipLoadout;

                lastSystem = UserDataCache.History?.GetLastFSD;

                OnPropertyChanged(null); // should result in all props being refreshed.
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
        public string Visits { get {
                if (lastFSD == null) return "Unknown";
                var result = Task.Run(async () => await JournalEntry.GetAllAsync(EDCommander.CurrentCmdrID, ids: new JournalTypeEnum[] { JournalTypeEnum.FSDJump })).Result;
                return result?.Count((s) => (s as JournalFSDJump).BodyID == lastFSD.BodyID).ToString() ?? "Unknown";
            }
        }

        public String WhereAmI {
            get => UserDataCache.History?.GetLast?.WhereAmI ?? "Unknown";
        }
        public String Note
        {
            get => lastSystem?.snc?.Note ?? "";
        }
        public ISystem System { get => shipLoadout?.System; }
        public ShipInformation ShipInformation { get => shipLoadout?.ShipInformation; }

        public String Fuel { get => String.Format($"{ShipInformation?.FuelLevel} / {ShipInformation?.FuelCapacity ?? 1}"); }
        public int MaterialsCount { get => shipLoadout?.MaterialCommodity?.MaterialsCount ?? 0; }
        public int CargoCount { get => shipLoadout?.MaterialCommodity?.CargoCount ?? 0; }
        
        public int DataCount { get => shipLoadout?.MaterialCommodity?.DataCount ?? 0; }

        public string Allegiance { get => lastFSD?.Allegiance; }
        public string PrimaryEconomy { get => lastFSD?.Economy_Localised; }
        public string Government { get => lastFSD?.Government_Localised; }
        public string State { get => lastFSD?.FactionState.SplitCapsWord(); }
        public string Security { get => lastFSD?.Security_Localised; }

        public string Credits { get => shipLoadout?.Credits.ToString("N0") ?? "Unknown"; }

        private JournalFSDJump lastFSD => lastSystem?.journalEntry as JournalFSDJump;
        private HistoryEntry shipLoadout;
        private HistoryEntry lastSystem;
    }
}