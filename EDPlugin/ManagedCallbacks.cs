using EliteDangerousCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EDPlugin
{
    public static class WebSocketMessage
    {
        public const string BROADCAST = "broadcast";
        public const string INIT_DB = "initdb";
    }

    // Used to bundle multiple messages into one response
    // e.g. a HistoryEvent, along with VisitCount and System details
    public class MobileWebResponse
    {
        [JsonConstructor]
        private MobileWebResponse()
        { }
        public MobileWebResponse(string requestType) { RequestType = requestType; }
        
        public string RequestType { get; private set; }
        public List<string> Responses { get; private set; } = new List<string>();

        public void Add(Object obj) {
            Responses.Add(JsonConvert.SerializeObject(obj)); }
    }
}
