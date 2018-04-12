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
        static ConcurrentQueue<long> journalIDQueue = new ConcurrentQueue<long>();
        static private bool flushEventQueue;
        private static Thread ThreadEGOSync;
        private static int _running = 0;
        private static bool Exit = false;

        private static void QueueEvent(EDCommander cmdr, InaraEvent ie)
        {
            lock (eventQueue)
            {
                if (CurrentCmdr == null)
                    CurrentCmdr = cmdr;

                if (CurrentCmdr.Nr != cmdr.Nr)
                {
                    FlushQueue(true);
                    CurrentCmdr = cmdr;
                }

            }
            eventQueue.Enqueue(ie);
            
        }

        /// <summary>
        /// Flush eventqueue
        /// </summary>
        /// <param name="wait">if true wait intill flushed.</param>
        public static void FlushQueue(bool wait)
        {
            flushEventQueue = true;

            if (wait)
            {
                Start();

                while (flushEventQueue)
                {
                    Thread.Sleep(10);
                }

            }

        }



        public static void Start()
        {
            if (Interlocked.CompareExchange(ref _running, 1, 0) == 0)
            {
                Exit = false;
                ThreadEGOSync = new System.Threading.Thread(new System.Threading.ThreadStart(InaraSync.SyncThread));
                ThreadEGOSync.Name = "Inara Sync";
                ThreadEGOSync.IsBackground = true;
                ThreadEGOSync.Start();
            }
        }


        public static void StopSync()
        {
            Exit = true;

        }


        private static void SyncThread()
        {
            List<InaraEvent> inaraevents = new List<InaraEvent>();

            try
            {
                DateTime firstqueuetime=default(DateTime);
                _running = 1;
                //mainForm.LogLine("Starting EGO sync thread");

                while (!Exit)
                {

                    while (eventQueue.TryDequeue(out InaraEvent ie))
                    {
                        if (inaraevents.Count == 0)  // First in queue set time
                            firstqueuetime = DateTime.UtcNow;
                        inaraevents.Add(ie);
                    }


                    // Time to flush?
                    if (inaraevents.Count > 0 && DateTime.UtcNow.Subtract(firstqueuetime).TotalSeconds > 10)
                        flushEventQueue = true;

                    if (inaraevents.Count > 50)
                        flushEventQueue = true;


                    if (flushEventQueue)
                    {
                        flushEventQueue = false;
                        InaraClass inara = new InaraClass(CurrentCmdr);
                        List<long> jidList = inara.SendEvents(inaraevents);
                        inaraevents.Clear();


                        if (jidList != null)
                            foreach (int jid in jidList)
                                journalIDQueue.Enqueue(jid);
                    }


                    // Uppdate journalids
                    while (journalIDQueue.TryDequeue(out long JournalID))
                    {
                        try
                        {
                            JournalEntry.UpdateSyncFlagBit(JournalID, SyncFlags.Inara, true);

                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Trace.WriteLine("Inara.UpdateSyncFlagBit exception ex:" + ex.Message);
                            //logger?.Invoke("Inara sync Exception " + ex.Message);
                        }
                    }


                    Thread.Sleep(20);   // Throttling 

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

                        events.Add(InaraEvent.setCommanderRankPilot(progress.Id, progress.EventTimeUTC, "Combat", (int)rank.Combat, progress.Combat / 100.0));
                        events.Add(InaraEvent.setCommanderRankPilot(progress.Id, progress.EventTimeUTC, "Trade", (int)rank.Trade, progress.Trade / 100.0));
                        events.Add(InaraEvent.setCommanderRankPilot(progress.Id, progress.EventTimeUTC, "Explore", (int)rank.Explore, progress.Explore / 100.0));
                        events.Add(InaraEvent.setCommanderRankPilot(progress.Id, progress.EventTimeUTC, "Empire", (int)rank.Empire, progress.Empire / 100.0));
                        events.Add(InaraEvent.setCommanderRankPilot(progress.Id, progress.EventTimeUTC, "Federation", (int)rank.Federation, progress.Federation / 100.0));
                        events.Add(InaraEvent.setCommanderRankPilot(progress.Id, progress.EventTimeUTC, "CQC", (int)rank.CQC, progress.CQC / 100.0));

                        lastrank = null;
                    }
                    break;


                case JournalTypeEnum.Statistics:
                    JournalStatistics stats = he.journalEntry as JournalStatistics;


                    events.Add(InaraEvent.setCommanderGameStatistics(stats.EventTimeUTC, stats));
                    break;


                case JournalTypeEnum.EngineerProgress:
                    JournalEngineerProgress engprogress = he.journalEntry as JournalEngineerProgress;

                    events.Add(InaraEvent.setCommanderRankEngineer(engprogress));
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
