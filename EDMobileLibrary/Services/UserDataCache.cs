using EDDMobileImpl;
using EDPlugin;
using EliteDangerousCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EDMobileLibrary.Services
{
    public class UserDataCache
    {
        public async static Task Initialise()
        {
            Debug.WriteLine("MOBILE:: Initializing database");

            if (!File.Exists(App.Options.UserDatabasePath))
            {
                Debug.WriteLine("MOBILE::UserDataCache !!not found!!");

                Debug.WriteLine("MOBILE::UserDataCache requesting new DB");
                await App.WebSocket.Send(WebSocketMessage.INIT_DB);
                // TODO: we'll need a dialog for this...
                var result = await App.WebSocket.ListenForData();

                Debug.WriteLine($"MOBILE::UserDataCache persisting new DB ({result.Length} bytes)");
                File.WriteAllBytes(App.Options.UserDatabasePath, result);
            }
            // TODO: get database version locally.. do we need to upgrade?
        }
    }
}
