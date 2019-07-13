using EliteDangerousCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EDPlugin
{
    public class ManagedCallbacks
    {
        public delegate bool EDDRequestRefresh(/*int lastjid*/);
        public delegate List<HistoryEntry> EDDGetHistory(int entryCount);
        public delegate HistoryEntry EDDGetLastHistory();
        public delegate HistoryEntry EDDGetHistoryEvent(long eventid);
        public delegate HistoryEntry EDDGetLastHistoryEntry(Predicate<HistoryEntry> where, HistoryEntry frominclusive);

        public EDDGetHistory GetHistory;
        public EDDGetLastHistory GetLastHistory;
        public EDDGetHistoryEvent GetHistoryEvent;
        public EDDGetLastHistoryEntry GetLastHistoryEntry;
    }

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
