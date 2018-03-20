using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliteDangerousCore.JournalEvents;

namespace EliteDangerous.Inara
{
    public class InaraEvent
    {
        string eventName;
        DateTime eventTimestamp;
        long eventCustomID;
        JObject eventData = null;
 

        public InaraEvent()
        {
            eventTimestamp = DateTime.Now;
            eventData = new JObject();
        }

        public InaraEvent(DateTime dt)
        {
            eventTimestamp = dt;
            eventData = new JObject();
        }

        public InaraEvent(DateTime dt, long id)
        {
            eventTimestamp = dt;
            eventData = new JObject();
            eventCustomID = id;
        }

        public JObject ToJObject()
        {
            JObject jo = new JObject();

            jo["eventName"] = eventName;
            jo["eventTimestamp"] = eventTimestamp.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture);
            if (eventCustomID != 0)
                jo["eventCustomID"] = eventCustomID;
            jo["eventData"] = eventData;

            return jo;
        }


        public static InaraEvent setCommanderCredits(JournalLoadGame lg) //   DateTime dt, long commanderCredits, long commanderAssets, long commanderLoan)
        {
            InaraEvent ie = new InaraEvent(lg.EventTimeUTC, lg.Id);
            ie.eventName = "setCommanderCredits";

            
            ie.eventData["commanderCredits"] = lg.Credits;

            //if (commanderAssets != 0)
            //    ie.eventData["commanderAssets"] = commanderAssets;

            if (lg.Loan != 0)
                ie.eventData["commanderLoan"] = lg.Loan;


            return ie;
        }


        public static InaraEvent setCommanderRankEngineer(JournalEngineerProgress ev)
        {
            InaraEvent ie = new InaraEvent(ev.EventTimeUTC, ev.Id);
            ie.eventName = "setCommanderRankEngineer";

            ie.eventData["engineerName"] = ev.Engineer;
            ie.eventData["rankStage"] = ev.Progress;
            ie.eventData["rankValue"] = ev.Rank;

            return ie;
        }


        public static InaraEvent setCommanderRankPilot(long id, DateTime dt, string rankName, int rankvalue, double rankProgress)
        {
            InaraEvent ie = new InaraEvent(dt, id);
            ie.eventName = "setCommanderRankPilot";

          

            ie.eventData["rankName"] = rankName;
            ie.eventData["rankValue"] = rankvalue;
            ie.eventData["rankProgress"] = rankProgress;

            return ie;
        }

        public static InaraEvent setCommanderGameStatistics(DateTime eventTimeUTC, JournalStatistics stats)
        {
            InaraEvent ie = new InaraEvent(eventTimeUTC);
            ie.eventName = "setCommanderGameStatistics";

            JObject json = stats.GetJson();

            json.Remove("timestamp");
            json.Remove("event");

            ie.eventData = json;

            return ie;

        }



    }
}
