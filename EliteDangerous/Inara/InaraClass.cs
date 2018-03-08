/*
 * Copyright © 2016 EDDiscovery development team
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
using EliteDangerousCore;
using EliteDangerousCore.DB;
using EliteDangerousCore.JournalEvents;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web;
using System.Linq;
using EliteDangerous.Inara;

namespace EliteDangerousCore.Inara
{
    public partial class InaraClass : BaseUtils.HttpCom
    {
        // use if you need an API/name pair to get info from EDSM.  Not all queries need it
        public bool ValidCredentials { get { return !string.IsNullOrEmpty(commanderName) && !string.IsNullOrEmpty(apiKey); } }

        private string commanderName;
        private string apiKey;

        private readonly string fromSoftwareVersion;
        private readonly string fromSoftware;
        //static private Dictionary<long, List<JournalScan>> DictEDSMBodies = new Dictionary<long, List<JournalScan>>();

        public InaraClass()
        {
            fromSoftware = "EDDiscovery";
            var assemblyFullName = Assembly.GetEntryAssembly().FullName;
            fromSoftwareVersion = assemblyFullName.Split(',')[1].Split('=')[1];

            base.httpserveraddress = "https://inara.cz/inapi/v1/";

            apiKey = EDCommander.Current.InaraAPIKey;
            commanderName = EDCommander.Current.Name;
        }

        public InaraClass(EDCommander cmdr) : this()
        {
            if (cmdr != null)
            {
                apiKey = cmdr.InaraAPIKey;
                commanderName = cmdr.Name;
            }
        }
        private JObject GenerateHeader()
        {
            JObject Header = new JObject();

            Header["appName"] = fromSoftware;
            Header["appVersion"] = fromSoftwareVersion;
            Header["isDeveloped"] = true;
            Header["APIkey"] = apiKey;
            Header["commanderName"] = commanderName;

            return Header;
        }

        public void SendEvents(List<InaraEvent> ievents)
        {
            JObject data = new JObject();

            data["header"] = GenerateHeader();

            JArray events = new JArray();

            foreach (var ie in ievents)
                events.Add(ie.ToJObject());

            data["events"] = events;

            PostMessage(data);


        }


        public bool PostMessage(JObject msg)
        {
            try
            {
                BaseUtils.ResponseData resp = RequestPost(msg.ToString(), "");

                JObject result = JObject.Parse(resp.Body);


                JObject header = (JObject)result["header"];


                if ((int)header["eventstatus"]==200)
                {
                    return true;
                }
                else
                {
                    System.Diagnostics.Trace.WriteLine($"Inara message post failed - status: {header["eventstatus"].ToNullSafeString()}\nInara Message: {msg.ToString().Replace(apiKey, "**********")}");
                    return false;
                }
            }
            catch (System.Net.WebException ex)
            {
                System.Net.HttpWebResponse response = ex.Response as System.Net.HttpWebResponse;
                System.Diagnostics.Trace.WriteLine($"Inara message post failed - status: {response?.StatusCode.ToString() ?? ex.Status.ToString()} Inara Message: {msg.ToString().Replace(apiKey, "**********")}");
                return false;
            }
        }

    }
}
