using EDDMobileImpl;
using EDPlugin;
using EliteDangerous.DB;
using EliteDangerous.JSON;
using EliteDangerousCore;
using EliteDangerousCore.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EDMobileLibrary.Services
{
    public class UserDataCache
    {
        public delegate void HistoryLoadedEvent();
        public static event HistoryLoadedEvent OnHistoryLoaded = ()=>{};
        public static HistoryList History { get; private set; } = null;
        

        public async static Task Initialise()
        {
            Debug.WriteLine("MOBILE:: Initializing database");

            if (!File.Exists(App.Options.UserDatabasePath))
            {
                Debug.WriteLine("MOBILE::UserDataCache !!not found!!");
                await App.WebSocket.Connect();
               
                Debug.WriteLine("MOBILE::UserDataCache requesting new DB");
                await App.WebSocket.Send(WebSocketMessage.INIT_DB);
                var result = await App.WebSocket.ListenForData();

                Debug.WriteLine($"MOBILE::UserDataCache persisting new DB ({result.Length} bytes)");
                File.WriteAllBytes(App.Options.UserDatabasePath, result);
            }

            await InitialiseUserDB();

            await InitialiseSystemDB();

            await LoadFSDHistory();
        }

        internal static void StoreMessage(MobileWebResponse response)
        {
            Debug.WriteLine($"INFO: msg received: {response.RequestType}");
            if (response.RequestType == WebSocketMessage.BROADCAST)
            {
                JournalEntryClass entry = JsonConvert.DeserializeObject<JournalEntryClass>(response.Responses[0]);
                Task.Run(async () => await entry.AddAsync());
            }
        }

        private static async Task InitialiseUserDB()
        {
            await Task.Run(() => SQLiteConnectionUser.Initialize());

            await App.WebSocket.Connect();
            var localLastEntry = JournalEntry.GetLastEvent(EDCommander.CurrentCmdrID).Id;
            await App.WebSocket.Send($"{WebSocketMessage.SYNCLASTEVENT}:{localLastEntry}");

            var result = await App.WebSocket.ListenForMessage();
            MobileWebResponse response = result.Deserialize<MobileWebResponse>();
            if (response.RequestType == WebSocketMessage.DONE)
                return;

            List<JournalEntryClass> newRecords = new List<JournalEntryClass>();
            if (response.RequestType == WebSocketMessage.SYNCLASTEVENT)
            {
                foreach(var part in response.Responses)
                {
                    var record = part.Deserialize<JournalEntryClass>();
                    newRecords.Add(record);
                }
            }
            JournalEntryClass.AddEntries(newRecords);
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
    }
}
