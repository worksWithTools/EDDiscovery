﻿/*
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

namespace EDPlugin
{
    public interface IEDDDLLCaller
    {
        bool Loaded { get; }
        string Version { get; }
        string Name { get; }

        string ActionCommand(string cmd, string[] paras);
        bool ActionJournalEntry(EDDDLLIF.JournalEntry je);
        bool Init(string ourversion, string dllfolder, EDDDLLIF.EDDCallBacks callbacks);
        bool NewJournalEntry(EDDDLLIF.JournalEntry nje);
        bool Refresh(string cmdr, EDDDLLIF.JournalEntry je);
        bool UnLoad();

    }
}