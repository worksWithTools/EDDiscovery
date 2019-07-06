﻿/*
 * Copyright © 2017 EDDiscovery development team
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace EliteDangerousCore.JournalEvents
{
    [JournalEntryType(JournalTypeEnum.Scanned)]
    public class JournalScanned : JournalEntry
	{
		[JsonConstructor]
		private JournalScanned(){}
        public JournalScanned(JObject evt) : base(evt, JournalTypeEnum.Scanned)
        {
            ScanType = evt["ScanType"].Str().SplitCapsWordFull();
        }

        public string ScanType { get; set; }

        public override void FillInformation(out string info, out string detailed) 
        {
            info = ScanType;
            detailed = "";
        }
    }

    [JournalEntryType(JournalTypeEnum.ShipTargeted)]
    public class JournalShipTargeted : JournalEntry
	{
		[JsonConstructor]
		private JournalShipTargeted(){}
        public JournalShipTargeted(JObject evt) : base(evt, JournalTypeEnum.ShipTargeted)
        {
            TargetLocked = evt["TargetLocked"].Bool();

            ShipFD = evt["Ship"].StrNull();
            if (ShipFD != null)
            {
                ShipFD = JournalFieldNaming.NormaliseFDShipName(ShipFD);
                Ship = JournalFieldNaming.GetBetterShipName(ShipFD);
            }
            Ship_Localised = JournalFieldNaming.CheckLocalisation(evt["Ship_Localised"].Str(), Ship);

            ScanStage = evt["ScanStage"].IntNull();
            PilotName = evt["PilotName"].StrNull();
            PilotName_Localised = JournalFieldNaming.CheckLocalisation(evt["PilotName_Localised"].Str(), PilotName);

            PilotRank = evt["PilotRank"].StrNull();
            ShieldHealth = evt["ShieldHealth"].DoubleNull();
            HullHealth = evt["HullHealth"].DoubleNull();
            Faction = evt["Faction"].StrNull();
            LegalStatus = evt["LegalStatus"].StrNull();
            Bounty = evt["Bounty"].IntNull();
            SubSystem = evt["SubSystem"].StrNull();
            SubSystemHealth = evt["SubSystemHealth"].DoubleNull();
        }

        public bool TargetLocked { get; set; }          // if false, no info below
        public int? ScanStage { get; set; }             // targetlocked= true, 0/1/2/3

        public string Ship { get; set; }                // 0 null
        public string ShipFD { get; set; }                // 0 null
        public string Ship_Localised { get; set; }      // 0 will be empty
        public string PilotName { get; set; }           // 1 null
        public string PilotName_Localised { get; set; } // 1 will be empty 
        public string PilotRank { get; set; }           // 1 null
        public double? ShieldHealth { get; set; }       // 2 null
        public double? HullHealth { get; set; }         // 2 null
        public string Faction { get; set; }             // 3 null
        public string LegalStatus { get; set; }         // 3 null
        public int? Bounty { get; set; }                // 3 null 
        public string SubSystem { get; set; }           // 3 null
        public double? SubSystemHealth { get; set; }    // 3 null

        public List<JournalShipTargeted> MergedEntries { get; set; }    // if verbose.. doing it this way does not break action packs as the variables are maintained
                                                                    // This is second, third merge etc.  First one is in above variables

        public void Add(JournalShipTargeted next)
        {
            if (MergedEntries == null)
                MergedEntries = new List<JournalShipTargeted>();
            MergedEntries.Add(next);
        }

        public override void FillInformation(out string info, out string detailed)
        {
            detailed = "";
            if (MergedEntries == null)
                info = ToString();
            else
            {
                info = (MergedEntries.Count() + 1).ToString() + " Target Events".Tx(this,"MC");
                for (int i = MergedEntries.Count - 1; i >= 0; i--)
                    detailed = detailed.AppendPrePad(MergedEntries[i].ToString(), System.Environment.NewLine);
                detailed = detailed.AppendPrePad(ToString(), System.Environment.NewLine);   // ours is the last one
            }
        }

        public override string ToString()
        {
            string info;
            if (TargetLocked)
            {
                if (ScanStage == null)
                {
                    info = "Missing Scan Stage - report to EDD team";
                }
                else if (ScanStage.Value == 0)
                {
                    info = BaseUtils.FieldBuilder.Build("", Ship_Localised);
                }
                else if (ScanStage.Value == 1)
                {
                    info = BaseUtils.FieldBuilder.Build("", PilotName_Localised, "Rank:".Txb(this), PilotRank, "< in ".Tx(this), Ship_Localised);
                }
                else if (ScanStage.Value == 2)
                {
                    info = BaseUtils.FieldBuilder.Build(
                        "", PilotName_Localised, "Rank:".Txb(this), PilotRank, "< in ".Tx(this), Ship_Localised,
                        "Shield ;;N1".Txb(this), ShieldHealth, "Hull ;;N1".Tx(this), HullHealth);
                        

                }
                else if (ScanStage.Value == 3)
                {
                    info = BaseUtils.FieldBuilder.Build(
                                    "", PilotName_Localised, "<(;)", LegalStatus, "Rank:".Txb(this), PilotRank, "< in ".Tx(this), Ship_Localised,
                                    "Shield ;;N1".Txb(this), ShieldHealth, "Hull ;;N1".Tx(this), HullHealth,
                                    "Bounty:; cr;N0".Txb(this), Bounty, 
                                    "", SubSystem, "< at ;;N1".Tx(this), SubSystemHealth
                                    );
                }
                else
                    info = "Unknown Scan Stage type - report to EDD team";
            }
            else
                info = "Lost Target".Txb(this);

            return info;
        }

    }


    [JournalEntryType(JournalTypeEnum.UnderAttack)]
    public class JournalUnderAttack : JournalEntry
	{
		[JsonConstructor]
		private JournalUnderAttack(){}
        public JournalUnderAttack(JObject evt) : base(evt, JournalTypeEnum.UnderAttack)
        {
            Target = evt["Target"].Str();
        }

        public string Target { get; set; }                  // always first one if merged list.
        public List<string> MergedEntries { get; set; }     // if verbose.. doing it this way does not break action packs as the variables are maintained
                                                            // This is second, third merge etc.  First one is in above variables

        public override void FillInformation(out string info, out string detailed)
        {
            detailed = "";
            if (MergedEntries != null)
            {
                info = (MergedEntries.Count+1).ToString("N0") + " " + "times".Tx(this, "ACOUNT");
                for (int i = MergedEntries.Count - 1; i >= 0; i--)
                    detailed = detailed.AppendPrePad(MergedEntries[i], System.Environment.NewLine);
                detailed = detailed.AppendPrePad(Target, System.Environment.NewLine);   // ours is the last one
            }
            else
            {
                info = BaseUtils.FieldBuilder.Build("", Target);
            }
        }

        public void Add( string target )
        {
            if (MergedEntries == null)
                MergedEntries = new List<string>();
            MergedEntries.Add(target);    // first is second oldest, etc
        }

    }

    [JournalEntryType(JournalTypeEnum.ShieldState)]
    public class JournalShieldState : JournalEntry
	{
		[JsonConstructor]
		private JournalShieldState(){}
        public JournalShieldState(JObject evt) : base(evt, JournalTypeEnum.ShieldState)
        {
            ShieldsUp = evt["ShieldsUp"].Bool();
        }

        public bool ShieldsUp { get; set; }

        protected override JournalTypeEnum IconEventType { get { return ShieldsUp ? JournalTypeEnum.ShieldState_ShieldsUp : JournalTypeEnum.ShieldState_ShieldsDown; } }

        public override void FillInformation(out string info, out string detailed)
        {
            info = BaseUtils.FieldBuilder.Build("Shields Down;Shields Up".Txb(this), ShieldsUp);
            detailed = "";
        }
    }


}
