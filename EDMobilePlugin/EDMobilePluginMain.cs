using System;
using System.Diagnostics;
using System.Reflection;
using EDPlugin;
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

            //var he = _managedcallbacks.GetHistoryEvent(nje.indexno);
            //var json = JsonConvert.SerializeObject(he.journalEntry);
            //WebSocketHttpServer.Broadcast(json);
            
        }

        public void EDDRefresh(string cmdname, EDDDLLIF.JournalEntry lastje)
        {
            Debug.WriteLine($"EDDRefresh: {cmdname}, Journal Entry {lastje.indexno}");
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
