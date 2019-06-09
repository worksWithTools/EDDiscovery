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

namespace EDDiscovery.DLL
{
    internal class EDDManagedCaller : IEDDDLLCaller
    {
        public EDDManagedCaller(string path)
        {

        }
        public bool Loaded => false;

        public string Version { get; private set; }
        public string Name { get; private set; }

        public string ActionCommand(string cmd, string[] paras)
        {
            return "Not Implemented";
        }

        public bool ActionJournalEntry(EDDDLLIF.JournalEntry je)
        {
            return false;
        }

        public bool Init(string ourversion, string dllfolder, EDDDLLIF.EDDCallBacks callbacks)
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
            return false;
        }
    }
}