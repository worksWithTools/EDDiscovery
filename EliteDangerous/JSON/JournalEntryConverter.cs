using EliteDangerousCore;
using EliteDangerousCore.JournalEvents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EliteDangerous.JSON
{
    public class JournalEntryConverter : JsonConverter
    {
        public JournalEntryConverter():base()
        {
        }
        public override bool CanWrite => false;
        public override bool CanConvert(Type objectType)
        {
            return typeof(JournalEntry).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var jobject = JObject.Load(reader);

                JournalEntry je = JournalEntry.CreateJournalEntry(jobject);
                return je;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }
            return new JournalUnknown(new JObject());
        }


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
