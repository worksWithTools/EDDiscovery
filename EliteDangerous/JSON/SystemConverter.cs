using EliteDangerousCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EliteDangerous.JSON
{
    class SystemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ISystem).IsAssignableFrom(objectType);

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            try
            {
                var jobject = JObject.Load(reader);

                var system = JsonConvert.DeserializeObject<SystemClass>(jobject.ToString());
                return system;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject.FromObject(value).WriteTo(writer);
        }
    }
}
