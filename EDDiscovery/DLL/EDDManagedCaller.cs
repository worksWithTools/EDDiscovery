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

using System;
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
                assembly = Assembly.LoadFile(_path);
                //TODO: can we loosen this?
                Type t = assembly.GetTypes()
                    .Where(x => typeof(IManagedDll).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .FirstOrDefault();
                if (t != null)
                {
                    caller = Activator.CreateInstance(t) as IManagedDll;

                    Name = System.IO.Path.GetFileNameWithoutExtension(_path);
                }
            }
            catch (Exception)
            {
                //TODO: do stuff
            }
        }

        public bool Loaded => caller != null;

        public string Version { get; private set; }
        public string Name { get; private set; }

        public bool Init(string ourversion, string dllfolder, EDDDLLIF.EDDCallBacks callbacks)
        {
            try
            {
                Version = caller.EDDInitialise(ourversion, dllfolder, callbacks);
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
            
            return "Not Implemented";
        }

        public bool ActionJournalEntry(EDDDLLIF.JournalEntry je)
        {
            return false;
        }

        

        public bool NewJournalEntry(EDDDLLIF.JournalEntry nje)
        {
            return false;
        }

        public bool Refresh(string cmdr, EDDDLLIF.JournalEntry je)
        {
            return false;
        }

        public bool UnLoad()
        {
            //TODO: shutdown the webserver
            return false;
        }
    }
}