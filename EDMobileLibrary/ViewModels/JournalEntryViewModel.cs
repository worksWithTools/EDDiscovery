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
using System.Linq;

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
                if (response == null)
                    return;
                Debug.WriteLine($"INFO: msg received: {response.RequestType}");
                if (response.RequestType == WebSocketMessage.BROADCAST)
                {
                    //TODO: we'll need to push the broadcast to the dbase now
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
                Items.Clear();
                //TODO: add commander filter
                var yesterday = DateTime.Now.Subtract(TimeSpan.FromHours(12));
                var newItems = await JournalEntry.GetAllAsync(after:yesterday, order:"DESC");

                do
                {
                    await Task.Run(() =>
                    {
                        var chunkSize = Math.Min(10, newItems.Count);
                        var nextChunk = newItems.Take(chunkSize);
                        foreach (var i in nextChunk)
                            Items.Add(i);
                        newItems.RemoveRange(0, chunkSize);
                    });
                } while (newItems.Count > 0);


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
