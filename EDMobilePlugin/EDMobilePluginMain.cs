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
            Debug.WriteLine($"EDDActionJournalEntry: {lastje.ToNullSafeString()}");
        }

        public string EDDInitialise(string vstr, EDDDLLIF.EDDCallBacks callbacks, ManagedCallbacks managedCallbacks)
        {
            WebSocketServer.Start("http://+:80/eddmobile/", callbacks, managedCallbacks);
 
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public void EDDNewJournalEntry(EDDDLLIF.JournalEntry nje)
        {
            Debug.WriteLine($"EDDNewJournalEntry: {nje.ToNullSafeString()}");
            
        }

        public void EDDRefresh(string cmdname, EDDDLLIF.JournalEntry lastje)
        {
            Debug.WriteLine($"EDDRefresh: {cmdname}, {lastje.ToNullSafeString()}");
        }

        public void EDDTerminate()
        {
            WebSocketServer.Stop();
        }
    }
}
