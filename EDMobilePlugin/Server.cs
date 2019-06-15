﻿using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDMobilePlugin
{
    public static class WebSocketServer
    {
        private static HttpListener Listener;
        private static CancellationTokenSource TokenSource;
        private static CancellationToken Token;

        private static int SocketCounter = 0;

        // The dictionary key corresponds to active socket IDs, and the BlockingCollection wraps
        // the default ConcurrentQueue to store broadcast messages for each active socket.
        private static ConcurrentDictionary<int, BlockingCollection<string>> BroadcastQueues = new ConcurrentDictionary<int, BlockingCollection<string>>();

        public static void Start(string uriPrefix)
        {
            TokenSource = new CancellationTokenSource();
            Token = TokenSource.Token;
            Listener = new HttpListener();
            Listener.Prefixes.Add(uriPrefix);
            Listener.Start();
            if (Listener.IsListening)
            {
                Debug.WriteLine("Connect browser for a basic echo-back web page.");
                Debug.WriteLine($"Server listening: {uriPrefix}");
                // listen on a separate thread so that Listener.Stop can interrupt GetContextAsync
                Task.Run(() => Listen().ConfigureAwait(false));
            }
            else
            {
                Debug.WriteLine("Server failed to start.");
            }
        }

        public static void Stop()
        {
            if (Listener?.IsListening ?? false)
            {
                TokenSource.Cancel();
                Debug.WriteLine("\nServer is stopping.");
                Listener.Stop();
                Listener.Close();
                TokenSource.Dispose();
            }
        }

        public static void Broadcast(string message)
        {
            Debug.WriteLine($"Broadcast: {message}");
            foreach (var kvp in BroadcastQueues)
                kvp.Value.Add(message);
        }

        private static async Task Listen()
        {
            while (!Token.IsCancellationRequested)
            {
                HttpListenerContext context = await Listener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    // HTTP is only the initial connection; upgrade to a client-specific websocket
                    HttpListenerWebSocketContext wsContext = null;
                    try
                    {
                        wsContext = await context.AcceptWebSocketAsync(subProtocol: null);
                        int socketId = Interlocked.Increment(ref SocketCounter);
                        Debug.WriteLine($"Socket {socketId}: New connection.");
                        _ = Task.Run(() => ProcessWebSocket(wsContext, socketId).ConfigureAwait(false));
                    }
                    catch (Exception)
                    {
                        // server error if upgrade from HTTP to WebSocket fails
                        context.Response.StatusCode = 500;
                        context.Response.Close();
                        return;
                    }
                }
                else
                {
                    {
                        context.Response.StatusCode = 404;
                    }
                    context.Response.Close();
                }
            }
        }

        private static async Task ProcessWebSocket(HttpListenerWebSocketContext context, int socketId)
        {
            var socket = context.WebSocket;

            BroadcastQueues.TryAdd(socketId, new BlockingCollection<string>());
            var broadcastTokenSource = new CancellationTokenSource();
            _ = Task.Run(() => WatchForBroadcasts(socketId, socket, broadcastTokenSource.Token));

            try
            {
                byte[] buffer = new byte[4096];
                while (socket.State == WebSocketState.Open && !Token.IsCancellationRequested)
                {
                    var receiveResult = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), Token);
                    Debug.WriteLine($"Socket {socketId}: Received {receiveResult.MessageType} frame ({receiveResult.Count} bytes).");
                    if (receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        Debug.WriteLine($"Socket {socketId}: Closing websocket.");
                        broadcastTokenSource.Cancel();
                        BroadcastQueues.TryRemove(socketId, out _);
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", Token);
                    }
                    else
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, receiveResult.Count);
                        
                        // if message is "ready" then send back some data..
                        if (message == ".ready")
                        {

                            string[] entries = { @"{ 'timestamp':'2019-05-26T18:00:17Z', 'event':'Progress', 'Combat':97, 'Trade':26, 'Explore':58, 'Empire':8, 'Federation':48, 'CQC':46 }",
                                 @"{ 'timestamp':'2019-05-26T18:00:17Z', 'event':'Reputation', 'Empire':75.000000, 'Federation':75.000000, 'Alliance':75.000000 }",
                                 @"{ 'timestamp':'2019-05-26T18:00:17Z', 'event':'EngineerProgress', 'Engineers':[ { 'Engineer':'Zacariah Nemo', 'EngineerID':300050, 'Progress':'Unlocked', 'RankProgress':42, 'Rank':4 }, { 'Engineer':'Tiana Fortune', 'EngineerID':300270, 'Progress':'Invited' }, { 'Engineer':'Marco Qwent', 'EngineerID':300200, 'Progress':'Unlocked', 'RankProgress':19, 'Rank':3 }, { 'Engineer':'Petra Olmanova', 'EngineerID':300130, 'Progress':'Invited' }, { 'Engineer':'Hera Tani', 'EngineerID':300090, 'Progress':'Unlocked', 'RankProgress':16, 'Rank':4 }, { 'Engineer':'Tod 'The Blaster' McQuinn', 'EngineerID':300260, 'Progress':'Unlocked', 'RankProgress':0, 'Rank':5 }, { 'Engineer':'Marsha Hicks', 'EngineerID':300150, 'Progress':'Invited' }, { 'Engineer':'Selene Jean', 'EngineerID':300210, 'Progress':'Unlocked', 'RankProgress':0, 'Rank':5 }, { 'Engineer':'Lei Cheung', 'EngineerID':300120, 'Progress':'Unlocked', 'RankProgress':0, 'Rank':5 }, { 'Engineer':'Juri Ishmaak', 'EngineerID':300250, 'Progress':'Unlocked', 'RankProgress':0, 'Rank':2 }, { 'Engineer':'Felicity Farseer', 'EngineerID':300100, 'Progress':'Unlocked', 'RankProgress':0, 'Rank':5 }, { 'Engineer':'Broo Tarquin', 'EngineerID':300030, 'Progress':'Unlocked', 'RankProgress':0, 'Rank':5 }, { 'Engineer':'Elvira Martuuk', 'EngineerID':300160, 'Progress':'Unlocked', 'RankProgress':47, 'Rank':4 }, { 'Engineer':'The Dweller', 'EngineerID':300180, 'Progress':'Unlocked', 'RankProgress':11, 'Rank':4 }, { 'Engineer':'Liz Ryder', 'EngineerID':300080, 'Progress':'Unlocked', 'RankProgress':70, 'Rank':1 }, { 'Engineer':'Didi Vatermann', 'EngineerID':300000, 'Progress':'Invited' }, { 'Engineer':'Mel Brandon', 'EngineerID':300280, 'Progress':'Known' }, { 'Engineer':'Ram Tah', 'EngineerID':300110, 'Progress':'Unlocked', 'RankProgress':34, 'Rank':3 }, { 'Engineer':'Bill Turner', 'EngineerID':300010, 'Progress':'Unlocked', 'RankProgress':0, 'Rank':5 } ] }",
                                 @"{ 'timestamp':'2019-05-26T18:00:26Z', 'event':'ReceiveText', 'From':'', 'Message':'$COMMS_entered:#name=BD-15 447;', 'Message_Localised':'Entered Channel: BD-15 447', 'Channel':'npc' }",
                                 @"{ 'timestamp':'2019-05-26T18:00:26Z', 'event':'Location', 'Docked':false, 'StarSystem':'BD-15 447', 'SystemAddress':2007997813450, 'StarPos':[8.00000,-81.25000,-40.12500], 'SystemAllegiance':'Empire', 'SystemEconomy':'$economy_Industrial;', 'SystemEconomy_Localised':'Industrial', 'SystemSecondEconomy':'$economy_Refinery;', 'SystemSecondEconomy_Localised':'Refinery', 'SystemGovernment':'$government_Corporate;', 'SystemGovernment_Localised':'Corporate', 'SystemSecurity':'$SYSTEM_SECURITY_high;', 'SystemSecurity_Localised':'High Security', 'Population':11253203, 'Body':'BD-15 447 A B Belt', 'BodyID':8, 'BodyType':'StellarRing', 'Factions':[ { 'Name':'BD-15 447 Republic Party', 'FactionState':'Election', 'Government':'Democracy', 'Influence':0.076000, 'Allegiance':'Federation', 'Happiness':'$Faction_HappinessBand2;', 'Happiness_Localised':'Happy', 'MyReputation':0.000000, 'ActiveStates':[ { 'State':'Election' } ] }, { 'Name':'Pilots' Federation Local Branch', 'FactionState':'None', 'Government':'Democracy', 'Influence':0.000000, 'Allegiance':'PilotsFederation', 'Happiness':'', 'MyReputation':0.000000 }, { 'Name':'BD-15 447 Bureau', 'FactionState':'None', 'Government':'Dictatorship', 'Influence':0.054000, 'Allegiance':'Independent', 'Happiness':'$Faction_HappinessBand2;', 'Happiness_Localised':'Happy', 'MyReputation':0.000000, 'RecoveringStates':[ { 'State':'Outbreak', 'Trend':0 } ] }, { 'Name':'Natural Epsilon Ceti Liberty Party', 'FactionState':'None', 'Government':'Dictatorship', 'Influence':0.045000, 'Allegiance':'Independent', 'Happiness':'$Faction_HappinessBand2;', 'Happiness_Localised':'Happy', 'MyReputation':0.000000 }, { 'Name':'BD-15 447 Company', 'FactionState':'None', 'Government':'Corporate', 'Influence':0.089000, 'Allegiance':'Federation', 'Happiness':'$Faction_HappinessBand2;', 'Happiness_Localised':'Happy', 'MyReputation':0.000000 }, { 'Name':'BD-15 447 Mafia', 'FactionState':'None', 'Government':'Anarchy', 'Influence':0.014000, 'Allegiance':'Independent', 'Happiness':'$Faction_HappinessBand2;', 'Happiness_Localised':'Happy', 'MyReputation':0.000000 }, { 'Name':'HR 692 Future', 'FactionState':'Election', 'Government':'Democracy', 'Influence':0.076000, 'Allegiance':'Federation', 'Happiness':'$Faction_HappinessBand2;', 'Happiness_Localised':'Happy', 'MyReputation':0.000000, 'ActiveStates':[ { 'State':'Election' } ] }, { 'Name':'East India Company', 'FactionState':'Boom', 'Government':'Corporate', 'Influence':0.646000, 'Allegiance':'Empire', 'Happiness':'$Faction_HappinessBand2;', 'Happiness_Localised':'Happy', 'MyReputation':0.000000, 'PendingStates':[ { 'State':'Expansion', 'Trend':0 } ], 'ActiveStates':[ { 'State':'Boom' } ] } ], 'SystemFaction':{ 'Name':'East India Company', 'FactionState':'Boom' }, 'Conflicts':[ { 'WarType':'election', 'Status':'active', 'Faction1':{ 'Name':'BD-15 447 Republic Party', 'Stake':'Gutierrez Port', 'WonDays':1 }, 'Faction2':{ 'Name':'HR 692 Future', 'Stake':'Jordan Point', 'WonDays':1 } } ] }",
                                 @"{ 'timestamp':'2019-05-26T18:00:26Z', 'event':'Music', 'MusicTrack':'NoTrack' }",
                                 @"{ 'timestamp':'2019-05-26T18:00:26Z', 'event':'Missions', 'Active':[  ], 'Failed':[  ], 'Complete':[  ] }",
                                 @"{ 'timestamp':'2019-05-26T18:00:26Z', 'event':'Loadout', 'Ship':'asp', 'ShipID':43, 'ShipName':'nebula grazer', 'ShipIdent':'WO-03A', 'HullValue':6145793, 'ModulesValue':21852708, 'HullHealth':0.981959, 'UnladenMass':403.100006, 'CargoCapacity':72, 'MaxJumpRange':54.794777, 'FuelCapacity':{ 'Main':32.000000, 'Reserve':0.630000 }, 'Rebuy':1399927, 'Modules':[ { 'Slot':'ShipCockpit', 'Item':'asp_cockpit', 'On':true, 'Priority':1, 'Health':0.989355 }, { 'Slot':'CargoHatch', 'Item':'modularcargobaydoor', 'On':true, 'Priority':2, 'Health':0.991331 }, { 'Slot':'SmallHardpoint1', 'Item':'hpt_dumbfiremissilerack_fixed_small', 'On':true, 'Priority':0, 'AmmoInClip':8, 'AmmoInHopper':16, 'Health':0.981248 }, { 'Slot':'SmallHardpoint2', 'Item':'hpt_dumbfiremissilerack_fixed_small', 'On':true, 'Priority':0, 'AmmoInClip':8, 'AmmoInHopper':16, 'Health':0.990017 }, { 'Slot':'SmallHardpoint3', 'Item':'hpt_cannon_gimbal_small', 'On':true, 'Priority':0, 'AmmoInClip':5, 'AmmoInHopper':100, 'Health':0.995114 }, { 'Slot':'SmallHardpoint4', 'Item':'hpt_guardian_shardcannon_turret_small', 'On':true, 'Priority':0, 'AmmoInClip':5, 'AmmoInHopper':180, 'Health':0.984116 }, { 'Slot':'MediumHardpoint1', 'Item':'hpt_beamlaser_gimbal_medium', 'On':true, 'Priority':0, 'Health':0.982912 }, { 'Slot':'MediumHardpoint2', 'Item':'hpt_beamlaser_gimbal_medium', 'On':true, 'Priority':0, 'Health':0.983273 }, { 'Slot':'TinyHardpoint1', 'Item':'hpt_plasmapointdefence_turret_tiny', 'On':true, 'Priority':0, 'AmmoInClip':12, 'AmmoInHopper':10000, 'Health':0.991996 }, { 'Slot':'TinyHardpoint2', 'Item':'hpt_shieldbooster_size0_class5', 'On':true, 'Priority':0, 'Health':0.992540 }, { 'Slot':'TinyHardpoint3', 'Item':'hpt_shieldbooster_size0_class5', 'On':true, 'Priority':0, 'Health':0.980213 }, { 'Slot':'TinyHardpoint4', 'Item':'hpt_chafflauncher_tiny', 'On':true, 'Priority':0, 'AmmoInClip':1, 'AmmoInHopper':10, 'Health':0.958143 }, { 'Slot':'Armour', 'Item':'asp_armour_grade1', 'On':true, 'Priority':1, 'Health':1.000000, 'Engineering':{ 'Engineer':'Selene Jean', 'EngineerID':300210, 'BlueprintID':128673642, 'BlueprintName':'Armour_HeavyDuty', 'Level':3, 'Quality':0.930000, 'Modifiers':[ { 'Label':'DefenceModifierHealthMultiplier', 'Value':119.023987, 'OriginalValue':79.999992, 'LessIsGood':0 }, { 'Label':'KineticResistance', 'Value':-16.484011, 'OriginalValue':-20.000004, 'LessIsGood':0 }, { 'Label':'ThermicResistance', 'Value':2.929997, 'OriginalValue':0.000000, 'LessIsGood':0 }, { 'Label':'ExplosiveResistance', 'Value':-35.898006, 'OriginalValue':-39.999996, 'LessIsGood':0 } ] } }, { 'Slot':'PowerPlant', 'Item':'int_powerplant_size5_class5', 'On':true, 'Priority':1, 'Health':0.999355, 'Engineering':{ 'Engineer':'Hera Tani', 'EngineerID':300090, 'BlueprintID':128673765, 'BlueprintName':'PowerPlant_Boosted', 'Level':1, 'Quality':1.000000, 'Modifiers':[ { 'Label':'Integrity', 'Value':100.699997, 'OriginalValue':106.000000, 'LessIsGood':0 }, { 'Label':'PowerCapacity', 'Value':22.848000, 'OriginalValue':20.400000, 'LessIsGood':0 }, { 'Label':'HeatEfficiency', 'Value':0.420000, 'OriginalValue':0.400000, 'LessIsGood':1 } ] } }, { 'Slot':'MainEngines', 'Item':'int_engine_size4_class5', 'On':true, 'Priority':0, 'Health':0.981098, 'Engineering':{ 'Engineer':'Elvira Martuuk', 'EngineerID':300160, 'BlueprintID':128673666, 'BlueprintName':'Engine_Tuned', 'Level':2, 'Quality':0.847000, 'Modifiers':[ { 'Label':'Integrity', 'Value':84.479996, 'OriginalValue':88.000000, 'LessIsGood':0 }, { 'Label':'PowerDraw', 'Value':5.116800, 'OriginalValue':4.920000, 'LessIsGood':1 }, { 'Label':'EngineOptimalMass', 'Value':403.200012, 'OriginalValue':420.000000, 'LessIsGood':0 }, { 'Label':'EngineOptPerformance', 'Value':112.339996, 'OriginalValue':100.000000, 'LessIsGood':0 }, { 'Label':'EngineHeatRate', 'Value':0.929890, 'OriginalValue':1.300000, 'LessIsGood':1 } ] } }, { 'Slot':'FrameShiftDrive', 'Item':'int_hyperdrive_size5_class5', 'On':true, 'Priority':0, 'Health':0.997210, 'Engineering':{ 'Engineer':'Elvira Martuuk', 'EngineerID':300160, 'BlueprintID':128673693, 'BlueprintName':'FSD_LongRange', 'Level':4, 'Quality':0.634000, 'ExperimentalEffect':'special_fsd_fuelcapacity', 'ExperimentalEffect_Localised':'Deep Charge', 'Modifiers':[ { 'Label':'Mass', 'Value':25.000000, 'OriginalValue':20.000000, 'LessIsGood':1 }, { 'Label':'Integrity', 'Value':105.599998, 'OriginalValue':120.000000, 'LessIsGood':0 }, { 'Label':'PowerDraw', 'Value':0.705600, 'OriginalValue':0.600000, 'LessIsGood':1 }, { 'Label':'FSDOptimalMass', 'Value':1484.069946, 'OriginalValue':1050.000000, 'LessIsGood':0 }, { 'Label':'MaxFuelPerJump', 'Value':5.500000, 'OriginalValue':5.000000, 'LessIsGood':0 } ] } }, { 'Slot':'LifeSupport', 'Item':'int_lifesupport_size4_class5', 'On':true, 'Priority':0, 'Health':0.981923 }, { 'Slot':'PowerDistributor', 'Item':'int_powerdistributor_size3_class5', 'On':true, 'Priority':0, 'Health':0.995854, 'Engineering':{ 'Engineer':'The Dweller', 'EngineerID':300180, 'BlueprintID':128673731, 'BlueprintName':'PowerDistributor_HighCapacity', 'Level':2, 'Quality':0.730000, 'Modifiers':[ { 'Label':'Integrity', 'Value':79.743996, 'OriginalValue':70.000000, 'LessIsGood':0 }, { 'Label':'WeaponsCapacity', 'Value':27.801601, 'OriginalValue':24.000000, 'LessIsGood':0 }, { 'Label':'WeaponsRecharge', 'Value':2.632000, 'OriginalValue':2.800000, 'LessIsGood':0 }, { 'Label':'EnginesCapacity', 'Value':20.851200, 'OriginalValue':18.000000, 'LessIsGood':0 }, { 'Label':'EnginesRecharge', 'Value':1.222000, 'OriginalValue':1.300000, 'LessIsGood':0 }, { 'Label':'SystemsCapacity', 'Value':20.851200, 'OriginalValue':18.000000, 'LessIsGood':0 }, { 'Label':'SystemsRecharge', 'Value':1.222000, 'OriginalValue':1.300000, 'LessIsGood':0 } ] } }, { 'Slot':'Radar', 'Item':'int_sensors_size5_class1', 'On':true, 'Priority':0, 'Health':0.993190 }, { 'Slot':'FuelTank', 'Item':'int_fueltank_size5_class3', 'On':true, 'Priority':1, 'Health':1.000000 }, { 'Slot':'Slot01_Size6', 'Item':'int_cargorack_size6_class1', 'On':true, 'Priority':1, 'Health':1.000000 }, { 'Slot':'Slot02_Size5', 'Item':'int_guardianfsdbooster_size5', 'On':true, 'Priority':0, 'Health':0.982192 }, { 'Slot':'Slot03_Size3', 'Item':'int_shieldgenerator_size3_class3_fast', 'On':true, 'Priority':0, 'Health':0.989639 }, { 'Slot':'Slot04_Size3', 'Item':'int_cargorack_size3_class1', 'On':true, 'Priority':1, 'Health':1.000000 }, { 'Slot':'Slot05_Size3', 'Item':'int_fuelscoop_size3_class5', 'On':true, 'Priority':0, 'Health':0.977174 }, { 'Slot':'Slot06_Size2', 'Item':'int_buggybay_size2_class1', 'On':true, 'Priority':0, 'Health':0.994453 }, { 'Slot':'Slot07_Size2', 'Item':'int_supercruiseassist', 'On':true, 'Priority':0, 'Health':0.985642 }, { 'Slot':'Slot08_Size1', 'Item':'int_dockingcomputer_advanced', 'On':true, 'Priority':0, 'Health':0.981683 }, { 'Slot':'PlanetaryApproachSuite', 'Item':'int_planetapproachsuite', 'On':true, 'Priority':1, 'Health':1.000000 }, { 'Slot':'VesselVoice', 'Item':'voicepack_victor', 'On':true, 'Priority':1, 'Health':1.000000 } ] }" };


                            foreach (var entry in entries)
                            {
                                Debug.WriteLine($"Socket {socketId}: sending {entry} to queue.");

                                BroadcastQueues[socketId].Add(entry);
                            }

                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // normal upon task/token cancellation, disregard
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\nSocket {socketId}:\n  Exception {ex.GetType().Name}: {ex.Message}");
                if (ex.InnerException != null) Debug.WriteLine($"  Inner Exception {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
            }
            finally
            {
                socket?.Dispose();
                broadcastTokenSource?.Cancel();
                broadcastTokenSource?.Dispose();
                BroadcastQueues?.TryRemove(socketId, out _);
            }
        }

        private const int BROADCAST_WAKEUP_INTERVAL = 250; // milliseconds

        private static async Task WatchForBroadcasts(int socketId, WebSocket socket, CancellationToken socketToken)
        {
            while (!socketToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(BROADCAST_WAKEUP_INTERVAL, socketToken);
                    if (!socketToken.IsCancellationRequested && BroadcastQueues[socketId].TryTake(out var message))
                    {
                        Debug.WriteLine($"Socket {socketId}: Sending from queue.");
                        var msgbuf = new ArraySegment<byte>(Encoding.ASCII.GetBytes(message));
                        await socket.SendAsync(msgbuf, WebSocketMessageType.Text, endOfMessage: true, socketToken);
                    }
                }
                catch (OperationCanceledException)
                {
                    // normal upon task/token cancellation, disregard
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"\nSocket {socketId} broadcast task:\n  Exception {ex.GetType().Name}: {ex.Message}");
                    if (ex.InnerException != null) Debug.WriteLine($"  Inner Exception {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
                }
            }
        }
    }
}