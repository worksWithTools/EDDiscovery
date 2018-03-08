using EliteDangerousCore;
using EliteDangerousCore.Inara;
using EliteDangerousCore.JournalEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EliteDangerous.Inara
{
    public class InaraSync
    {
        static HistoryEntry lastrank;

        public static void InitalSync(EDCommander cmdr, HistoryList history)
        {
            if (!cmdr.SyncToInara)
                return;

            HistoryEntry he = history.GetLastLoadGame;

            if (he == null)
                return;

            JournalLoadGame lg = he.journalEntry as JournalLoadGame;

            InaraClass inara = new InaraClass(cmdr);

            List<InaraEvent> ievents = new List<InaraEvent>();

            InaraEvent ie = InaraEvent.setCommanderCredits(lg.EventTimeUTC, lg.Credits, 0, lg.Loan);

            ievents.Add(ie);

            inara.SendEvents(ievents);
        }





        static public bool SyncHistoryEntry(EDCommander cmdr, HistoryEntry he)
        {
            List<InaraEvent> events = new List<InaraEvent>();

            switch (he.journalEntry.EventTypeID)
            {
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
            }


            InaraClass inara = new InaraClass(cmdr);
            if (events.Count>0)
                inara.SendEvents(events);

            return true;
        }



    }
}
