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
                lastEntry = UserDataCache.History?.GetLast;
                
                lastSystem = UserDataCache.History?.GetLastFSD?.journalEntry as JournalFSDJump;

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
        public string Visits { get => UserDataCache.History?.GetVisitsCount(lastSystem.Body).ToString() ?? "Unknown"; }

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
        public int MaterialsCount { get => lastEntry?.MaterialCommodity?.MaterialsCount ?? 0; }
        public int CargoCount { get => lastEntry?.MaterialCommodity?.CargoCount ?? 0; }
        
        public int DataCount { get => lastEntry?.MaterialCommodity?.DataCount ?? 0; }

        public string Allegiance { get => lastSystem?.Allegiance; }
        public string PrimaryEconomy { get => lastSystem?.Economy_Localised; }
        public string Government { get => lastSystem?.Government_Localised; }
        public string State { get => lastSystem?.FactionState.SplitCapsWord(); }
        public string Security { get => lastSystem?.Security_Localised; }

        public string Credits { get => lastEntry?.Credits.ToString("N0") ?? "Unknown"; }

        private HistoryEntry lastEntry;
        private JournalFSDJump lastSystem;
    }
}