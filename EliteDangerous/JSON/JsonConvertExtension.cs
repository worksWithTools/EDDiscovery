using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace EliteDangerous.JSON
{
    public static class JsonConvertExtension
    {

        public static T Deserialize<T>(this string message) where T : class
        {
            var contractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            contractResolver.DefaultMembersSearchFlags |= BindingFlags.NonPublic;
            var settings = new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = contractResolver,
#if DEBUG
                TraceWriter = new MemoryTraceWriter()
#endif
            };
            try
            {
                return JsonConvert.DeserializeObject<T>(message, settings);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine(settings.TraceWriter);
            }
            return null;
        }
    }
}
