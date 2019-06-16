using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDPlugin
{
    public static class JournalEntryExtensions
    {
        public static string ToString(this EDPlugin.EDDDLLIF.JournalEntry je)
        {
            return JsonConvert.SerializeObject(je);
        }
    }
}
