using EDDMobileImpl;
using EDPlugin;
using EliteDangerousCore;
using EliteDangerousCore.DB;
using System;
using System.Collections.Generic;
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

        private static async Task InitialiseUserDB()
        {
             await Task.Run(() => SQLiteConnectionUser.Initialize());
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
                        new TimeSpan(0, 30, 0), // the last 30 mins of play ought to do
                        CurrentCommander: EDCommander.CurrentCmdrID,
                        essentialitems: JournalEssentialEvents.StatusEssentialEvents) ; 
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
