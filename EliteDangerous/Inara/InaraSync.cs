using EliteDangerousCore;
using EliteDangerousCore.Inara;
using EliteDangerousCore.JournalEvents;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EliteDangerous.Inara
{
    public class InaraSync
    {
        static EDCommander CurrentCmdr;
        static HistoryEntry lastrank;
        static ConcurrentQueue<InaraEvent> eventQueue = new ConcurrentQueue<InaraEvent>();
        static ConcurrentQueue<int> journalIDQueue = new ConcurrentQueue<int>();
        private bool flushEventue;
        private static Thread ThreadEGOSync;
        private static int _running = 0;
        private static bool Exit = false;

        private static void QueueEvent(EDCommander cmdr, InaraEvent ie)
        {
            // TODO Test if new comander
            CurrentCmdr = cmdr;
            eventQueue.Enqueue(ie);
        }



        public void Start()
        {
            if (Interlocked.CompareExchange(ref _running, 1, 0) == 0)
            {
                Exit = false;
                ThreadEGOSync = new System.Threading.Thread(new System.Threading.ThreadStart(SyncThread));
                ThreadEGOSync.Name = "Inara Sync";
                ThreadEGOSync.IsBackground = true;
                ThreadEGOSync.Start();
            }
        }


        public static void StopSync()
        {
            Exit = true;

        }


        private void SyncThread()
        {
            List<InaraEvent> inaraevents = new List<InaraEvent>();

            try
            {
                _running = 1;
                //mainForm.LogLine("Starting EGO sync thread");



                while (!Exit)
                {

                    while (eventQueue.TryDequeue(out InaraEvent ie))
                    {
                        inaraevents.Add(ie);
                    }


                    // Time to flush

                    if (flushEventue)
                    {
                        flushEventue = false;
                        InaraClass inara = new InaraClass(CurrentCmdr);
                        List<int> jidList = inara.SendEvents(inaraevents);
                        inaraevents.Clear();

                        if (jidList != null)
                            foreach (int jid in jidList)
                                journalIDQueue.Enqueue(jid);
                    }


                  
                    Thread.Sleep(100);   // Throttling 

                }

                //mainForm.LogLine("Inara sync thread exiting");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Exception ex:" + ex.Message);
                //logger?.Invoke("Inara sync Exception " + ex.Message);
            }
            finally
            {
                _running = 0;
            }
        }






        public static void InitalSync(EDCommander cmdr, HistoryList history)
        {
            if (!cmdr.SyncToInara)
                return;

            HistoryEntry he = history.GetLastLoadGame;

            if (he == null)
                return;

            JournalLoadGame lg = he.journalEntry as JournalLoadGame;


          
            if (!lg.SyncedInara)
            {
                InaraEvent ie = InaraEvent.setCommanderCredits(lg);
                QueueEvent(cmdr, ie); 
            }
        }


        static public bool SyncHistoryEntry(EDCommander cmdr, HistoryEntry he)
        {
            List<InaraEvent> events = new List<InaraEvent>();

            switch (he.journalEntry.EventTypeID)
            {
                case JournalTypeEnum.LoadGame:
                    JournalLoadGame lg = he.journalEntry as JournalLoadGame;
                    events.Add(InaraEvent.setCommanderCredits(lg));
                    break;

                case JournalTypeEnum.Rank:
                    lastrank = he;
                    break;


                case JournalTypeEnum.Progress:
                    if (lastrank!=null)
                    {
                        JournalRank rank = lastrank.journalEntry as JournalRank;
                        JournalProgress progress = he.journalEntry as JournalProgress;

                        events.Add(InaraEvent.setCommanderRankPilot(progress.EventTimeUTC, "Combat", (int)rank.Combat, progress.Combat / 100.0));
                        events.Add(InaraEvent.setCommanderRankPilot(progress.EventTimeUTC, "Trade", (int)rank.Trade, progress.Trade / 100.0));
                        events.Add(InaraEvent.setCommanderRankPilot(progress.EventTimeUTC, "Explore", (int)rank.Explore, progress.Explore / 100.0));
                        events.Add(InaraEvent.setCommanderRankPilot(progress.EventTimeUTC, "Empire", (int)rank.Empire, progress.Empire / 100.0));
                        events.Add(InaraEvent.setCommanderRankPilot(progress.EventTimeUTC, "Federation", (int)rank.Federation, progress.Federation / 100.0));
                        events.Add(InaraEvent.setCommanderRankPilot(progress.EventTimeUTC, "CQC", (int)rank.CQC, progress.CQC / 100.0));


                        lastrank = null;
                    }
                    break;

                case JournalTypeEnum.Statistics:
                    JournalStatistics stats = he.journalEntry as JournalStatistics;


                    events.Add(InaraEvent.setCommanderGameStatistics(stats.EventTimeUTC, stats));
                    break;


                case JournalTypeEnum.EngineerProgress:
                    JournalEngineerProgress engprogress = he.journalEntry as JournalEngineerProgress;

                    break;
            }


            InaraClass inara = new InaraClass(cmdr);
            if (events.Count > 0)
                foreach (InaraEvent ie in events)
                    QueueEvent(cmdr, ie);

            return true;
        }
    }
}
