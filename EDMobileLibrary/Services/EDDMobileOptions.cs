using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace EDMobileLibrary.Services
{
    internal class EDDMobileOptions : EliteDangerousCore.IEliteOptions
    {
        private static EDDMobileOptions instance;

        private EDDMobileOptions()
        {
        }

        public static EDDMobileOptions Instance            // Singleton pattern
        {
            get
            {
                if (instance == null)
                {
                    instance = new EDDMobileOptions();
                    EliteDangerousCore.EliteConfigInstance.InstanceOptions = instance;
                }
                return instance;
            }
        }
        public string AppDataDirectory => FileSystem.AppDataDirectory;

        public string SystemDatabasePath => Path.Combine(AppDataDirectory, "EDDSystem.sqlite");

        public string UserDatabasePath => Path.Combine(AppDataDirectory, "EDDUser.sqlite");

        public bool ForceBetaOnCommander => false;

        public bool DisableMerge => true;

        public bool DisableBetaCommanderCheck => true;
    }
}
