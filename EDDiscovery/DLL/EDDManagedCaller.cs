/*
 * Copyright © 2015 - 2018 EDDiscovery development team
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
 * file except in compliance with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software distributed under
 * the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
 * ANY KIND, either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 * 
 * EDDiscovery is not affiliated with Frontier Developments plc.
 */

using EDPlugin;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EDDiscovery.DLL
{
    internal class EDDManagedCaller : IEDDDLLCaller
    {
        Assembly assembly;
        string _path;
        IManagedDll caller;
        public EDDManagedCaller(string path)
        {
            try
            {
                _path = path;
                Name = Path.GetFileNameWithoutExtension(_path);
                var pluginPath = Path.GetDirectoryName(_path);
                AppDomain.CurrentDomain.AppendPrivatePath(pluginPath);
                assembly = Assembly.LoadFrom(_path);
                Type t = assembly.GetTypes()
                    .Where(x => IsManagedPlugin(x))
                    .FirstOrDefault();
                if (t != null)
                {
                    caller = Activator.CreateInstance(t) as IManagedDll;

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private static bool IsManagedPlugin(Type x)
        {
            return typeof(IManagedDll).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract;
        }

        public bool Loaded => caller != null;

        public string Version { get; private set; }
        public string Name { get; private set; }

        public bool Init(string ourversion, string dllfolder, EDDDLLIF.EDDCallBacks callbacks)
        {
            try
            {
                Version = caller.EDDInitialise(ourversion, callbacks);

                return !String.IsNullOrEmpty(Version) && Version[0] != '!';
            }
            catch(Exception e)
            {
                // TODO: logging or summat
            }
            return false;
        }
        public string ActionCommand(string cmd, string[] paras)
        {
            return caller.EDDActionCommand(cmd, paras);
        }

        public bool ActionJournalEntry(EDDDLLIF.JournalEntry je)
        {
            caller.EDDActionJournalEntry(je);
            return true;
        }

        

        public bool NewJournalEntry(EDDDLLIF.JournalEntry nje)
        {
            caller.EDDNewJournalEntry(nje);
            return true;
        }

        public bool Refresh(string cmdr, EDDDLLIF.JournalEntry je)
        {
            caller.EDDRefresh(cmdr, je);
            return true;
        }

        public bool UnLoad()
        {
            caller.EDDTerminate();
            return true;
        }
    }
}