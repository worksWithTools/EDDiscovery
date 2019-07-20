using System;
using System.Diagnostics;
using System.Reflection;
using EDPlugin;
using EliteDangerous.DB;
using EliteDangerousCore.DB;
using Newtonsoft.Json;

namespace EDMobilePlugin
{
    public class EDMobilePluginMain : IManagedDll
    {
        public string EDDActionCommand(string cmdname, string[] paras)
        {
            Debug.WriteLine($"EDDActionCommand: {cmdname}, {paras}");
            return "Done";
        }

        public void EDDActionJournalEntry(EDDDLLIF.JournalEntry lastje)
        {
            Debug.WriteLine($"EDDActionJournalEntry: {lastje.ToJson()}");
        }

        public string EDDInitialise(string vstr, EDDDLLIF.EDDCallBacks callbacks)
        {
            AutoDiscoveryServer.Start();
            WebSocketHttpServer.Start("http://+:80/eddmobile/", callbacks);
 
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public void EDDNewJournalEntry(EDDDLLIF.JournalEntry nje)
        {
            Debug.WriteLine($"EDDNewJournalEntry: {nje.ToJson()}");

            JournalEntryClass je = JournalEntryClass.GetJournalEntry(nje.jid); 
            var json = JsonConvert.SerializeObject(je);
            WebSocketHttpServer.Broadcast(json);
        }

        public void EDDRefresh(string cmdname, EDDDLLIF.JournalEntry lastje)
        {
            Debug.WriteLine($"EDDRefresh: {cmdname}, Journal Entry {lastje.jid}");
            //
            //var he = _managedcallbacks.GetHistoryEvent(lastje.indexno);

            //var json = JsonConvert.SerializeObject(he);
            //WebSocketHttpServer.Broadcast(json);
        }

        public void EDDTerminate()
        {
            WebSocketHttpServer.Stop();
        }
    }
}
