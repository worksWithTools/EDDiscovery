using EliteDangerousCore.DB;
using SQLLiteExtensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace EDMobileLibrary.Services
{
    public static class SQLiteConnectionUserExt
    {
        static readonly int currentDbVer = 1;
        public static async Task MobileInit(this SQLiteConnectionUser conn)
        {
            SQLExtRegister reg = new SQLExtRegister(conn);
            var dbver = reg.GetSettingInt("MobileDBVer", 0);

            if (dbver < 1)
            {
                await RemoveAutoIncrementFromID(conn);
            }

            reg.PutSettingInt("MobileDBVer", currentDbVer);
        }

        private static async Task RemoveAutoIncrementFromID(SQLiteConnectionUser conn)
        {
            string[] queries = new[]
           {
                //TODO: should this maybe be in one Execute, with a TRANS/COMMIT?
                "DROP TABLE IF EXISTS JournalEntriesBackup",
                "ALTER TABLE JournalEntries RENAME TO JournalEntriesBackup",
                "CREATE TABLE JournalEntries ( Id INTEGER NOT NULL PRIMARY KEY," +
                    "TravelLogId INTEGER NOT NULL REFERENCES TravelLogUnit(Id), " +
                    "CommanderId INTEGER NOT NULL DEFAULT 0," +
                    "EventTypeId INTEGER NOT NULL, " +
                    "EventType TEXT, " +
                    "EventTime DATETIME NOT NULL, " +
                    "EventData TEXT, " +
                    "EdsmId INTEGER, " +
                    "Synced INTEGER )",
                "INSERT INTO JournalEntries " +
                    "(Id, TravelLogId, CommanderId, EventTypeId, EventType, EventTime, EventData, EdsmId, Synced )" +
                    " SELECT Id, TravelLogId, CommanderId, EventTypeId, EventType, EventTime, EventData, EdsmId, Synced" +
                    " FROM JournalEntriesBackup",
                "DROP TABLE IF EXISTS JournalEntriesBackup",
            };

            foreach (string query in queries)
            {
                using (DbCommand cmd = conn.CreateCommand(query))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
