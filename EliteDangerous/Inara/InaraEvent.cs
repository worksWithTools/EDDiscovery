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
        int eventCustomID;
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


        public static InaraEvent setCommanderCredits(DateTime dt, long commanderCredits, long commanderAssets, long commanderLoan)
        {
            InaraEvent ie = new InaraEvent(dt);
            ie.eventName = "setCommanderCredits";

            ie.eventData["commanderCredits"] = commanderCredits;
            if (commanderAssets != 0)
                ie.eventData["commanderAssets"] = commanderAssets;

            if (commanderLoan != 0)
                ie.eventData["commanderLoan"] = commanderLoan;


            return ie;
        }


        public static InaraEvent setCommanderRankPilot(DateTime dt, string rankName, int rankvalue, double rankProgress)
        {
            InaraEvent ie = new InaraEvent(dt);
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
