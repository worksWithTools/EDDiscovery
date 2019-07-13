﻿using EDDMobileImpl;
using EDPlugin;
using EliteDangerousCore;
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
                // TODO: we'll need a dialog for this...
                var result = await App.WebSocket.ListenForData();

                Debug.WriteLine($"MOBILE::UserDataCache persisting new DB ({result.Length} bytes)");
                File.WriteAllBytes(App.Options.UserDatabasePath, result);
            }
            await LoadHistory();
        }

        private static async Task LoadHistory()
        {
            
            try
            {
                Debug.WriteLine("MOBILE:: Load history..");

                await Task.Run(() =>
                {
                    //TODO: implement a cancel handler
                    
                    // do we need to rewrite this to be more friendly to the app?
                    History = HistoryList.LoadHistory(null, () => false,
                        (p, s) => Debug.WriteLine("Processing history entry {0}:{1}", p, s), 
                        CurrentCommander: EDCommander.CurrentCmdrID,
                        essentialitems: nameof(JournalEssentialEvents.FullStatsEssentialEvents),
                        fullhistoryloaddaylimit: 1);
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
