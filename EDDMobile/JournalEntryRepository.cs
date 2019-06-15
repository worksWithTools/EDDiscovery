using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EliteDangerousCore;

namespace EDDMobile
{
    public class JournalEntryRepository
    {
        private ObservableCollection<JournalEntry> journalinfo;
        public ObservableCollection<JournalEntry> journalCollection
        {
            get { return journalinfo; }
            set { this.journalinfo = value; }
        }

        public JournalEntryRepository()
        {
            journalinfo = new ObservableCollection<JournalEntry>();
        }

        public void AddEntry(string entry)
        {
           journalinfo.Add(JournalEntry.CreateJournalEntry(entry));
        }
    }
}