using EliteDangerousCore;
using EliteDangerousCore.Inara;
using EliteDangerousCore.JournalEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteDangerous.Inara
{
    public class InaraSync
    {
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

    }
}
