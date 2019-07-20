using EDDMobileImpl;
using EDPlugin;
using EliteDangerous.DB;
using EliteDangerous.JSON;
using EliteDangerousCore;
using EliteDangerousCore.DB;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ToastMessage;
using Xamarin.Forms;

namespace EDMobileLibrary.Services
{
    public class UserDataCache
    {
        public delegate void HistoryLoadedEvent();
        public static event HistoryLoadedEvent OnHistoryLoaded = ()=>{};
        public delegate void CacheUpdatedEvent(JournalEntry[] id);
        public static event CacheUpdatedEvent OnCacheUpdated = (JournalEntry[] newEntries) => {};
        public static HistoryList History { get; private set; } = null;
        
        public async static Task Initialise()
        {
            Debug.WriteLine("MOBILE:: Initializing database");

            if (!File.Exists(App.Options.UserDatabasePath))
            {
                try
                {
                    Debug.WriteLine("MOBILE::UserDataCache !!not found!!");
                    if (await App.WebSocket.Connect())
                    {
                        Debug.WriteLine("MOBILE::UserDataCache requesting new DB");
                        await App.WebSocket.Send(WebSocketMessage.INIT_DB);
                        var result = await App.WebSocket.ListenForData();
                        Debug.WriteLine($"MOBILE::UserDataCache persisting new DB ({result.Length} bytes)");
                        File.WriteAllBytes(App.Options.UserDatabasePath, result);
                    }
                    else
                    {
                        //TODO: make this more prominent
                        DependencyService.Get<Toast>().Show($"Failed to connect to EDD backend.");
                        return;
                    }
                }
                catch (SQLiteException e)
                {
                    Debug.WriteLine(e);
                }
            }

            await InitialiseUserDB();

            await InitialiseSystemDB();

            await LoadFSDHistory();

            await UpdateUserDB();
        }

        internal static async void StoreMessage(MobileWebResponse response)
        {
            Debug.WriteLine($"INFO: msg received: {response.RequestType}");
            if (response.RequestType == WebSocketMessage.BROADCAST)
            {
                JournalEntryClass entry = response.Responses[0].Deserialize<JournalEntryClass>();
                Debug.WriteLine($"MOBILE::received journal entry type: {entry.EventType}; Id={entry.Id}; [{entry.ToString()}]");
                if (await entry.AddAsync())
                    OnCacheUpdated(new[] { JournalEntry.CreateJournalEntry(entry) });
            }
        }


        private static async Task InitialiseUserDB()
        {
            await Task.Run(() => SQLiteConnectionUser.Initialize());
            using (var conn = new SQLiteConnectionUser())
            {
                await conn.MobileInit();
            }
        }

        private static async Task InitialiseSystemDB()
        {
            // note: for now won'actually populate this... it's just for backward compatibility...
            await Task.Run(() => SQLiteConnectionSystem.Initialize());
        }

        private static async Task LoadFSDHistory()
        {
            
            try
            {
                Debug.WriteLine("MOBILE:: Load recent history..");

                await Task.Run(() =>
                {
                    // do we need to rewrite this to be more friendly to the app?
                    History = HistoryList.LoadHistory(null, () => false,
                        (p, s) => Debug.WriteLine("Processing history entry {0}:{1}", p, s),
                        CurrentCommander: EDCommander.CurrentCmdrID,
                        essentialitems: JournalEssentialEvents.StatusEssentialEvents,
                        startFromLast: JournalTypeEnum.FSDJump) ; 
                });

                Trace.WriteLine(BaseUtils.AppTicks.TickCountLap() + " Load history complete with " + History.Count + " records");

                OnHistoryLoaded();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("History Refresh Error: " + ex);
            }
        }

        private static async Task UpdateUserDB()
        {
            try
            {
                Debug.WriteLine("MOBILE:: Check for updates in EDD dbase..");
                if (await App.WebSocket.Connect())
                {
                    Debug.WriteLine("MOBILE:: Connected to EDD dbase..");
                    var localLastEntry = JournalEntry.GetLastEvent(EDCommander.CurrentCmdrID).Id;
                    await App.WebSocket.Send($"{WebSocketMessage.SYNCLASTEVENT}:{localLastEntry}");

                    var result = await App.WebSocket.ListenForMessage();
                    MobileWebResponse response = result.Deserialize<MobileWebResponse>();
                    if (response.RequestType == WebSocketMessage.DONE)
                        return;

                    List<JournalEntryClass> newRecords = new List<JournalEntryClass>();
                    if (response.RequestType == WebSocketMessage.SYNCLASTEVENT)
                    {
                        foreach (var part in response.Responses)
                        {
                            var record = part.Deserialize<JournalEntryClass>();
                            newRecords.Add(record);
                        }
                    }
                    JournalEntryClass.AddEntries(newRecords);
                    OnCacheUpdated((from r in newRecords
                                   select JournalEntry.CreateJournalEntry(r)).ToArray());
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine($"MOBILE:: Error updating user DB {e}");
            }
        }
    }
}
