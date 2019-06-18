using EliteDangerousCore;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EDDMobileImpl.ViewModels
{
    public class JournalEntryViewModel : BaseViewModel
    {
        string lastJournalEntry;
        public Command LoadItemsCommand { get; set; }
        public string LastJournalEntry { get => lastJournalEntry; set => SetProperty(ref lastJournalEntry, value); }

        public JournalEntryViewModel()
        {
            Title = "Browse";
            LastJournalEntry = null;
            LoadItemsCommand = new Command(async () => await ExecuteLoadJournalEntriesCommand());
        }

        async Task ExecuteLoadJournalEntriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await WebSocket.Send("refresh");
                var msg = await WebSocket.ListenForMessage();
                LastJournalEntry = msg; // JournalEntry.CreateJournalEntry(msg);
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