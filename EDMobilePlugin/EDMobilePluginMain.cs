using System;
using System.Diagnostics;
using System.Reflection;
using EDPlugin;


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

        public string EDDInitialise(string vstr, EDDDLLIF.EDDCallBacks callbacks, ManagedCallbacks managedCallbacks)
        {
            WebSocketServer.Start("http://+:80/eddmobile/", callbacks, managedCallbacks);
 
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public void EDDNewJournalEntry(EDDDLLIF.JournalEntry nje)
        {
            Debug.WriteLine($"EDDNewJournalEntry: {nje.ToJson()}");
            
        }

        public void EDDRefresh(string cmdname, EDDDLLIF.JournalEntry lastje)
        {
            string json = lastje.ToJson();
            Debug.WriteLine($"EDDRefresh: {cmdname}, {json}");
            //Broadcast now, or 
            WebSocketServer.Broadcast(json);
        }

        public void EDDTerminate()
        {
            WebSocketServer.Stop();
        }
    }
}
