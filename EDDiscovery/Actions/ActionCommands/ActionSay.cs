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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDDiscovery.Actions
{
    public class ActionSay : Action
    {
        public override bool AllowDirectEditingOfUserData { get { return true; } }    // and allow editing?

        public static string globalvarspeechvolume = "SpeechVolume";
        public static string globalvarspeechrate = "SpeechRate";
        public static string globalvarspeechvoice = "SpeechVoice";
        public static string globalvarspeecheffects = "SpeechEffects";
        public static string globalvarspeechculture = "SpeechCulture";

        static string volumename = "Volume";
        static string voicename = "Voice";
        static string ratename = "Rate";
        static string waitname = "Wait";
        static string priorityname = "Priority";
        static string startname = "StartEvent";
        static string finishname = "FinishEvent";
        static string culturename = "Culture";
        static string literalname = "Literal";
        static string dontspeakname = "DontSpeak";

        public bool FromString(string s, out string saying, out ConditionVariables vars)
        {
            vars = new ConditionVariables();

            if (s.IndexOf(',') == -1 && s.IndexOf('"') == -1) // no quotes, no commas, just the string, probably typed in..
            {
                saying = s;
                return true;
            }
            else
            {
                StringParser p = new StringParser(s);
                saying = p.NextQuotedWord(", ");        // stop at space or comma..

                if (saying != null && (p.IsEOL || (p.IsCharMoveOn(',') && vars.FromString(p, ConditionVariables.FromMode.MultiEntryComma))))   // normalise variable names (true)
                    return true;

                saying = "";
                return false;
            }
        }

        public string ToString(string saying, ConditionVariables cond)
        {
            if (cond.Count > 0)
                return saying.QuoteString(comma: true) + ", " + cond.ToString();
            else
                return saying.QuoteString(comma: true);
        }

        public override string VerifyActionCorrect()
        {
            string saying;
            ConditionVariables vars;
            return FromString(userdata, out saying, out vars) ? null : "Say command line not in correct format";
        }

        public override bool ConfigurationMenu(Form parent, EDDiscoveryForm discoveryform, List<string> eventvars)
        {
            string saying;
            ConditionVariables vars;
            FromString(userdata, out saying, out vars);

            Audio.SpeechConfigure cfg = new Audio.SpeechConfigure();
            cfg.Init(discoveryform.AudioQueueSpeech, discoveryform.SpeechSynthesizer,
                        "Set Text to say (use ; to separate randomly selectable phrases and {} to group)", "Configure Say Command",
                        saying,
                        vars.Exists(waitname), vars.Exists(literalname),
                        Audio.AudioQueue.GetPriority(vars.GetString(priorityname, "Normal")),
                        vars.GetString(startname, ""),
                        vars.GetString(finishname, ""),
                        vars.GetString(voicename, "Default"),
                        vars.GetString(volumename, "Default"),
                        vars.GetString(ratename, "Default"),
                        vars
                        );

            if (cfg.ShowDialog(parent) == DialogResult.OK)
            {
                ConditionVariables cond = new ConditionVariables(cfg.Effects);// add on any effects variables (and may add in some previous variables, since we did not purge
                cond.SetOrRemove(cfg.Wait, waitname, "1");
                cond.SetOrRemove(cfg.Literal, literalname, "1");
                cond.SetOrRemove(cfg.Priority != Audio.AudioQueue.Priority.Normal, priorityname, cfg.Priority.ToString());
                cond.SetOrRemove(cfg.StartEvent.Length > 0, startname, cfg.StartEvent);
                cond.SetOrRemove(cfg.StartEvent.Length > 0, finishname, cfg.FinishEvent);
                cond.SetOrRemove(!cfg.VoiceName.Equals("Default", StringComparison.InvariantCultureIgnoreCase), voicename, cfg.VoiceName);
                cond.SetOrRemove(!cfg.Volume.Equals("Default", StringComparison.InvariantCultureIgnoreCase), volumename, cfg.Volume);
                cond.SetOrRemove(!cfg.Rate.Equals("Default", StringComparison.InvariantCultureIgnoreCase), ratename, cfg.Rate);

                userdata = ToString(cfg.SayText, cond);
                return true;
            }

            return false;
        }

        class AudioEvent
        {
            public ActionProgramRun apr;
            public bool wait;
            public string triggername;
            public string eventname;
        }

        Random rnd = new Random();

        public override bool ExecuteAction(ActionProgramRun ap)
        {
            string say;
            ConditionVariables statementvars;
            if (FromString(userdata, out say, out statementvars))
            {
                string errlist = null;
                ConditionVariables vars = statementvars.ExpandAll(ap.functions,statementvars, out errlist);

                if (errlist == null)
                {
                    bool wait = vars.GetInt(waitname, 0) != 0;
                    Audio.AudioQueue.Priority priority = Audio.AudioQueue.GetPriority(vars.GetString(priorityname, "Normal"));
                    string start = vars.GetString(startname);
                    string finish = vars.GetString(finishname);
                    string voice = vars.Exists(voicename) ? vars[voicename] : (ap.VarExist(globalvarspeechvoice) ? ap[globalvarspeechvoice] : "Default");

                    int vol = vars.GetInt(volumename, -999);
                    if (vol == -999)
                        vol = ap.variables.GetInt(globalvarspeechvolume, 60);

                    int rate = vars.GetInt(ratename, -999);
                    if (rate == -999)
                        rate = ap.variables.GetInt(globalvarspeechrate, 0);

                    string culture = vars.Exists(culturename) ? vars[culturename] : (ap.VarExist(globalvarspeechculture) ? ap[globalvarspeechculture] : "Default");

                    bool literal = vars.GetInt(literalname, 0) != 0;
                    bool dontspeak = vars.GetInt(dontspeakname, 0) != 0;

                    Audio.SoundEffectSettings ses = new Audio.SoundEffectSettings(vars);        // use the rest of the vars to place effects

                    if (!ses.Any && !ses.OverrideNone && ap.VarExist(globalvarspeecheffects))  // if can't see any, and override none if off, and we have a global, use that
                    {
                        vars = new ConditionVariables(ap[globalvarspeecheffects], ConditionVariables.FromMode.MultiEntryComma);
                    }

                    string expsay;
                    if (ap.functions.ExpandString(say, out expsay) != EDDiscovery.ConditionFunctions.ExpandResult.Failed)
                    {
                        if ( !literal ) 
                            expsay = expsay.PickOneOfGroups(rnd);       // expand grouping if not literal

                        ap["SaySaid"] = expsay;

                        if ((ap.VarExist("SpeechDebug") && ap["SpeechDebug"].Contains("Print")))
                        {
                            ap.actioncontroller.LogLine("Say: " + expsay);
                            expsay = "";
                        }

                        if (dontspeak)
                            expsay = "";

                        System.IO.MemoryStream ms = ap.actioncontroller.DiscoveryForm.SpeechSynthesizer.Speak(expsay, culture, voice, rate);

                        if (ms != null)
                        {
                            Audio.AudioQueue.AudioSample audio = ap.actioncontroller.DiscoveryForm.AudioQueueSpeech.Generate(ms, vars, true);

                            if (audio != null)
                            {
                                if (start != null && start.Length > 0)
                                {
                                    audio.sampleStartTag = new AudioEvent { apr = ap, eventname = start, triggername = "onSayStarted" };
                                    audio.sampleStartEvent += Audio_sampleEvent;

                                }
                                if (wait || (finish != null && finish.Length > 0))       // if waiting, or finish call
                                {
                                    audio.sampleOverTag = new AudioEvent() { apr = ap, wait = wait, eventname = finish, triggername = "onSayFinished" };
                                    audio.sampleOverEvent += Audio_sampleEvent;
                                }

                                ap.actioncontroller.DiscoveryForm.AudioQueueSpeech.Submit(audio, vol, priority);

                                return !wait;       //False if wait, meaning terminate and wait for it to complete, true otherwise, continue
                            }
                            else
                                ap.ReportError("Say could not create audio, check Effects settings");
                        }
                    }
                    else
                        ap.ReportError(expsay);
                }
                else
                    ap.ReportError(errlist);
            }
            else
                ap.ReportError("Say command line not in correct format");


            return true;
        }

        private void Audio_sampleEvent(Audio.AudioQueue sender, object tag)
        {
            AudioEvent af = tag as AudioEvent;

            if (af.eventname != null && af.eventname.Length>0)
                af.apr.actioncontroller.ActionRun(af.triggername, "ActionProgram", null, new ConditionVariables("EventName", af.eventname), now: false);    // queue at end an event

            if (af.wait)
                af.apr.ResumeAfterPause();
        }
    }
}
