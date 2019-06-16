using System;
using System.Collections.Generic;
using System.Text;
using static EDPlugin.EDDDLLIF;

namespace EDPlugin
{
    public interface IManagedDll
    {
        String EDDInitialise(string vstr,
                             EDDCallBacks callbacks,
                             ManagedCallbacks managedCallbacks);

        void EDDRefresh(string cmdname, JournalEntry lastje);

        void EDDNewJournalEntry(JournalEntry nje);

        void EDDTerminate();

        String EDDActionCommand(string cmdname, string[] paras);

        void EDDActionJournalEntry(JournalEntry lastje);
    }

    public class ManagedCallbacks
    {
        public delegate bool EDDRequestRefresh(/*int lastjid*/);

        public EDDRequestRefresh RequestRefresh; // placeholder... might be more...
    }
}
