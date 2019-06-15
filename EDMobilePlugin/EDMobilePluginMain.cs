using System;
using System.Reflection;
using EDDiscovery.DLL;

namespace EDMobilePlugin
{
    public class EDMobilePluginMain : EDDiscovery.DLL.IManagedDll
    {
        public string EDDActionCommand(string cmdname, string[] paras)
        {
            throw new NotImplementedException();
        }

        public void EDDActionJournalEntry(EDDDLLIF.JournalEntry lastje)
        {
            throw new NotImplementedException();
        }

        public string EDDInitialise(string vstr, string dllfolder, EDDDLLIF.EDDCallBacks callbacks)
        {
            // we'll probs want to be able to stop this task somehow?

            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public void EDDNewJournalEntry(EDDDLLIF.JournalEntry nje)
        {
            throw new NotImplementedException();
        }

        public void EDDRefresh(string cmdname, EDDDLLIF.JournalEntry lastje)
        {
            throw new NotImplementedException();
        }

        public void EDDTerminate()
        {
       }
    }
}
