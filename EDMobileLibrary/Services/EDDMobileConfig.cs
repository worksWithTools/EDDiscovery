using System;
using System.Collections.Generic;
using System.Text;

namespace EDMobileLibrary.Services
{
    public class EDDMobileConfig : EliteDangerousCore.IEliteConfig
    {
        private static EDDMobileConfig instance;

        private EDDMobileConfig()
        {
        }

        public static EDDMobileConfig Instance            // Singleton pattern
        {
            get
            {
                if (instance == null)
                {
                    instance = new EDDMobileConfig();
                    EliteDangerousCore.EliteConfigInstance.InstanceConfig = instance;        // hook up so classes can see this which use this IF
                }
                return instance;
            }
        }

        //TODO: full implementation
        public int DefaultMapColour { get => System.Drawing.Color.Red.ToArgb(); }

        public bool DisplayUTC => DateTime.UtcNow == DateTime.Now;

        public string EDSMFullSystemsURL => throw new NotImplementedException();

        public string EDDBSystemsURL => throw new NotImplementedException();
    }
}
