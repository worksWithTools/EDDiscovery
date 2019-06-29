using EliteDangerousCore;
using System.Collections.Generic;

namespace EDPlugin
{
    public class ManagedCallbacks
    {
        public delegate bool EDDRequestRefresh(/*int lastjid*/);
        public delegate List<HistoryEntry> EDDGetHistory(int entryCount);
        public delegate HistoryEntry EDDGetLastHistory();

        public EDDGetHistory GetHistory;
        public EDDGetLastHistory GetLastHistory;
    }

    public static class WebSocketMessage
    {
        public const string REFRESH_STATUS = "refresh:status";
        public const string GET_JOURNAL = "refresh:journal:";
    }
}
