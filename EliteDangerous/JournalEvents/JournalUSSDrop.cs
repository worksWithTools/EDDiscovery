﻿/*
 * Copyright © 2016-2018 EDDiscovery development team
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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

namespace EliteDangerousCore.JournalEvents
{
    [JournalEntryType(JournalTypeEnum.USSDrop)]
    public class JournalUSSDrop : JournalEntry
	{
		[JsonConstructor]
		private JournalUSSDrop(){}
        public JournalUSSDrop(JObject evt ) : base(evt, JournalTypeEnum.USSDrop)
        {
            USSType = evt["USSType"].Str();
            USSTypeLocalised = JournalFieldNaming.CheckLocalisation(evt["USSType_Localised"].Str(),USSType);
            USSThreat = evt["USSThreat"].Int();
        }

        public string USSType { get; set; }
        public int USSThreat { get; set; }
        public string USSTypeLocalised { get; set; }

        public override void FillInformation(out string info, out string detailed) 
        {
            info = BaseUtils.FieldBuilder.Build("Type:".Txb(this), USSTypeLocalised, "Threat:".Txb(this), USSThreat);
            detailed = "";
        }
    }
}
