﻿/*
 * Copyright © 2017-2019 EDDiscovery development team
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
using System.Collections.Generic;
using BaseUtils;
using ActionLanguage;
using EliteDangerousCore;
using EliteDangerousCore.DB;

namespace EDDiscovery.Actions
{
    public class ActionTarget : ActionBase
    {
        public override bool AllowDirectEditingOfUserData { get { return true; } }

        public override bool Configure(ActionCoreController cp, List<BaseUtils.TypeHelpers.PropertyNameInfo> eventvars, ActionConfigFuncs configFuncs)
        {
            string promptValue = configFuncs.PromptSingleLine("Command string", UserData, "Configure Target Command" , cp.Icon);
            if (promptValue != null)
            {
                userdata = promptValue;
            }

            return (promptValue != null);
        }

        public override bool ExecuteAction(ActionProgramRun ap)
        {
            string res;
            if (ap.functions.ExpandString(UserData, out res) != BaseUtils.Functions.ExpandResult.Failed)
            {
                StringParser sp = new StringParser(res);

                string prefix = "T_";
                string cmdname = sp.NextWord();

                if (cmdname != null && cmdname.Equals("PREFIX", StringComparison.InvariantCultureIgnoreCase))
                {
                    prefix = sp.NextWord();

                    if (prefix == null)
                    {
                        ap.ReportError("Missing name after Prefix in Target");                        
                        return true;
                    }

                    cmdname = sp.NextWord();
                }

                EDDiscoveryForm discoveryform = (ap.actioncontroller as ActionController).DiscoveryForm;

                if (cmdname != null )
                {
                    if (cmdname.Equals("GET", StringComparison.InvariantCultureIgnoreCase))
                    {
                        bool tset = EliteDangerousCore.DB.TargetClass.IsTargetSet();
                        ap[prefix + "TargetSet"] = tset.ToStringIntValue();
                        if (tset)
                        {
                            EliteDangerousCore.DB.TargetClass.GetTargetPosition(out string name, out double x, out double y, out double z);
                            ap[prefix + "TargetType"] =EliteDangerousCore.DB.TargetClass.GetTargetType().ToString();
                            ap[prefix + "TargetPositionFullName"] = name;
                            ap[prefix + "TargetPositionName"] = EliteDangerousCore.DB.TargetClass.GetNameWithoutPrefix(name);

                            if (!double.IsNaN(x) && !double.IsNaN(y) && !double.IsNaN(z))
                            {
                                ap[prefix + "TargetPositionX"] = x.ToStringInvariant("0.##");
                                ap[prefix + "TargetPositionY"] = y.ToStringInvariant("0.##");
                                ap[prefix + "TargetPositionZ"] = z.ToStringInvariant("0.##");
                            }
                        }
                    }
                    else if (cmdname.Equals("CLEAR", StringComparison.InvariantCultureIgnoreCase))
                    {
                        bool tset = EliteDangerousCore.DB.TargetClass.IsTargetSet();
                        ap[prefix + "TargetClear"] = tset.ToStringIntValue();
                        if (tset)
                        {
                            TargetClass.ClearTarget();
                            discoveryform.NewTargetSet(this);
                        }
                    }
                    else
                    {
                        string name = sp.NextQuotedWord();

                        if (name != null)
                        {

                            if (cmdname.Equals("BOOKMARK", StringComparison.InvariantCultureIgnoreCase))
                            {
                                BookmarkClass bk = GlobalBookMarkList.Instance.FindBookmarkOnSystem(name);    // has it been bookmarked?

                                if (bk != null)
                                {
                                    TargetClass.SetTargetBookmark(name, bk.id, bk.x, bk.y, bk.z);
                                    discoveryform.NewTargetSet(this);
                                }
                                else
                                    ap.ReportError("Bookmark '" + name + "' not found");

                            }
                            else if (cmdname.Equals("GMO", StringComparison.InvariantCultureIgnoreCase))
                            {
                                EliteDangerousCore.EDSM.GalacticMapObject gmo = discoveryform.galacticMapping.Find(name, true, true);

                                if (gmo != null)
                                {
                                    TargetClass.SetTargetGMO("G:" + gmo.name, gmo.id, gmo.points[0].X, gmo.points[0].Y, gmo.points[0].Z);
                                    discoveryform.NewTargetSet(this);
                                }

                                else
                                    ap.ReportError("GMO '" + name + "' not found");
                            }
                            else if (cmdname.Equals("NOTE", StringComparison.InvariantCultureIgnoreCase))
                            {
                                SystemNoteClass nc = SystemNoteClass.GetNoteOnSystem(name);        // has it got a note?
                                ISystem sc = discoveryform.history.FindSystem(name);

                                if ( sc != null && sc.HasCoordinate && nc != null )
                                {
                                    TargetClass.SetTargetNotedSystem(name, nc.id, sc.X, sc.Y, sc.Z);
                                    discoveryform.NewTargetSet(this);
                                }
                                else
                                    ap.ReportError("No Note found on entries in system '" + name + "'");
                            }
                            else
                            {
                                ap.ReportError("Unknown TARGET command");
                            }
                        }
                        else
                            ap.ReportError("Missing name in command");
                    }
                }
                else
                    ap.ReportError("Missing TARGET command");
            }
            else
                ap.ReportError(res);

            return true;
        }
    }


}
