using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteDangerous.Inara
{
    public class InaraEvent
    {
        string eventName;
        DateTime eventTimestamp;
        int eventCustomID;
        JObject eventData;


        public InaraEvent()
        {
            eventTimestamp = DateTime.Now;
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
            InaraEvent ie = new InaraEvent();

            ie.eventTimestamp = dt;
            ie.eventName = "setCommanderCredits";

            JObject eventdata = new JObject();

            eventdata["commanderCredits"] = commanderCredits;
            if (commanderAssets != 0)
                eventdata["commanderAssets"] = commanderAssets;

            if (commanderLoan != 0)
                eventdata["commanderLoan"] = commanderLoan;

            ie.eventData = eventdata;

            return ie;
        }

    }
}
