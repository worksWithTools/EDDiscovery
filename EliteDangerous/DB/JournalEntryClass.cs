using EliteDangerousCore.DB;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace EliteDangerous.DB
{
    public class JournalEntryClass
    {
        public long Id { get; private set; }
        public long TravelLogId { get; private set; }
        public long CommanderId { get; private set; }
        public long EventTypeId { get; private set; }
        public string EventType { get; private set; }
        public DateTime EventTime { get; private set; }
        public string EventData { get; private set; }
        public long EdsmId { get; private set; }
        public long Synced { get; private set; }

        public JournalEntryClass()
        {
        }

        public static JournalEntryClass GetJournalEntry(long id)
        {
            using (SQLiteConnectionUser cn = new SQLiteConnectionUser(mode: SQLLiteExtensions.SQLExtConnection.AccessMode.Reader))
            {
                using (DbCommand cmd = cn.CreateCommand($"select * from JournalEntries where Id={id}"))
                {
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                           return MakeJournalEntryClass(rdr);
                        }
                    }
                }
            }
            return null;
        }

        public static List<JournalEntryClass> GetJournalEntries(long fromid)
        {
            List<JournalEntryClass> results = new List<JournalEntryClass>();
            using (SQLiteConnectionUser cn = new SQLiteConnectionUser(mode: SQLLiteExtensions.SQLExtConnection.AccessMode.Reader))
            {
                using (DbCommand cmd = cn.CreateCommand($"select * from JournalEntries where Id>{fromid}"))
                {
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            results.Add(MakeJournalEntryClass(rdr));
                        }
                    }
                }
            }
            return results;
        }

        private static JournalEntryClass MakeJournalEntryClass(DbDataReader rdr)
        {
            return new JournalEntryClass
            {
                Id = (long)rdr["Id"],
                TravelLogId = (long)rdr["TravelLogId"],
                CommanderId = (long)rdr["CommanderId"],
                EventTypeId = (long)rdr["EventTypeId"],
                EventType = (string)rdr["EventType"],
                EventTime = (DateTime)rdr["EventTime"],
                EventData = (string)rdr["EventData"],
                EdsmId = (long)rdr["EdsmId"],
                Synced = (long)rdr["Synced"]
            };
        }

        public async Task<bool> AddAsync()
        {
            using (SQLiteConnectionUser cn = new SQLiteConnectionUser())      // open connection..
            {
                return await AddAsync(cn);
            }
        }

        public static void AddEntries(List<JournalEntryClass> newRecords)
        {
            using (SQLiteConnectionUser cn = new SQLiteConnectionUser())
            {
                newRecords.ForEach(async (r) => await r.AddAsync());
            }
        }

        private async Task<bool> AddAsync(SQLiteConnectionUser cn)
        {
            try
            {
                using (DbCommand cmd = cn.CreateCommand("Insert into JournalEntries (Id, TravelLogId, CommanderId, EventTypeId, EventType, EventTime, EventData, EdsmId, Synced) " +
                    "values (@id,@tlid,@cmdid,@etid,@et,@etime,@data,@edsmid,@sync)"))
                {
                    cmd.AddParameterWithValue("@id", Id);
                    cmd.AddParameterWithValue("@tlid", TravelLogId);
                    cmd.AddParameterWithValue("@cmdid", CommanderId);
                    cmd.AddParameterWithValue("@etid", EventTypeId);
                    cmd.AddParameterWithValue("@et", EventType);
                    cmd.AddParameterWithValue("@etime", EventTime);
                    cmd.AddParameterWithValue("@data", EventData);
                    cmd.AddParameterWithValue("@edsmid", EdsmId);
                    cmd.AddParameterWithValue("@sync", Synced);
                    await cmd.ExecuteNonQueryAsync();

                    return true;
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return false;
        }

    }
}
