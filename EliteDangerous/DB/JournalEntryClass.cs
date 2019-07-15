using EliteDangerousCore.DB;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

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
                            return new JournalEntryClass {
                                Id = id,
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
                    }
                }
            }
            return null;
        }

        public bool Add()
        {
            using (SQLiteConnectionUser cn = new SQLiteConnectionUser())      // open connection..
            {
                return Add(cn);
            }
        }

        private bool Add(SQLiteConnectionUser cn)
        {
            using (DbCommand cmd = cn.CreateCommand("Insert into JournalEntries (TravelLogId, CommanderId, EventTypeId, EventType, EventTime, EventData, EdsmId, Synced) " +
                "values (@tlid,@cmdid,@etid,@et,@etime,@data,@edsmid,@sync)"))
            {
                cmd.AddParameterWithValue("@tlid", TravelLogId);
                cmd.AddParameterWithValue("@cmdid", CommanderId);
                cmd.AddParameterWithValue("@etid", EventTypeId);
                cmd.AddParameterWithValue("@et", EventType);
                cmd.AddParameterWithValue("@etime", EventTime);
                cmd.AddParameterWithValue("@data", EventData);
                cmd.AddParameterWithValue("@edsmid", EdsmId);
                cmd.AddParameterWithValue("@sync", Synced);
                cmd.ExecuteNonQuery();

                using (DbCommand cmd2 = cn.CreateCommand("Select Max(id) as id from JournalEntries"))
                {
                    Id = (long)cmd2.ExecuteScalar();
                }

                return true;
            }
        }

    }
}
