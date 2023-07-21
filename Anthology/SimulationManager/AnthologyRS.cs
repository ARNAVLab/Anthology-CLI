using Anthology.Models;
using System.Numerics;

namespace Anthology.SimulationManager
{
    public class AnthologyRS : RealitySim
    {
        
        public override void Init(string pathFile = "")
        {
            ExecutionManager.Init(pathFile);
        }

        public override void LoadNpcs(Dictionary<string, NPC> npcs)
        {
            HashSet<Agent> agents = AgentManager.Agents;
            foreach (Agent a in agents)
            {
                if (!npcs.TryGetValue(a.Name, out NPC? npc))
                    npc = new NPC();
                npc.Name = a.Name;
                npc.Location = a.CurrentLocation;
                if (a.CurrentAction != null && a.CurrentAction.Count > 0)
                {
                    npc.CurrentAction.Name = a.CurrentAction.First().Name;
                }
                npc.ActionCounter = a.OccupiedCounter;
                if (a.Destination != string.Empty)
                {
                    npc.Destination = a.Destination;
                }
                Dictionary<string, float> motives = a.Motives;
                foreach (string mote in motives.Keys)
                {
                    npc.Motives[mote] = motives[mote];
                }
                npcs[a.Name] = npc;
            }
        }

        public override void LoadLocations(Dictionary<Location.Coords, Location> locations)
        {
            locations.Clear();
            IEnumerable<LocationNode> locNodes = LocationManager.LocationsByName.Values;
            foreach(LocationNode locNode in locNodes)
            {
                Location loc = new()
                {
                    Name = locNode.Name,
                    Coordinates = new(locNode.X, locNode.Y),
                };
                loc.Tags.UnionWith(locNode.Tags);
                foreach(KeyValuePair<string, float> con in locNode.Connections)
                {
                    loc.Connections.Add(con.Key, con.Value);
                }
                locations.Add(loc.Coordinates, loc);
            }
        }

        public override void PushLocations()
        {
            /*LocationManager.LocationSet.Clear();
            LocationManager.LocationGrid.Clear();
            UI.GridSize = 0;
            foreach (Location loc in SimManager.Locations.Values)
            {
                LocationManager.AddLocation(loc.Name, loc.Coordinates.X, loc.Coordinates.Y, loc.Tags);
            }*/
        }

        public override void UpdateNpc(NPC npc)
        {
            bool shouldLog = false;
            Agent agent = AgentManager.GetAgentByName(npc.Name);
            npc.Location = agent.CurrentLocation;

            if (agent.Destination != string.Empty)
            {
                npc.Destination = agent.Destination;
            }
            else
            {
                npc.Destination = string.Empty;
            }
            Dictionary<string, float> motives = agent.Motives;
            foreach (string mote in motives.Keys)
            {
                if (!npc.Motives.ContainsKey(mote))
                {
                    npc.Motives[mote] = motives[mote];
                }
                else if (npc.Motives[mote] != motives[mote]) {
                    shouldLog |= true;
                    npc.Motives[mote] = motives[mote];
                }
            }
            if (agent.CurrentAction.Count > 0 && npc.CurrentAction.Name != agent.CurrentAction.First().Name)
            {
                shouldLog = true;
                npc.CurrentAction.Name = agent.CurrentAction.First().Name;
            }
            npc.ActionCounter = agent.OccupiedCounter;
            if (shouldLog)
            {
                SimManager.History?.AddNpcToLog(npc);
            }
        }

        public override void PushUpdatedNpc(NPC npc)
        {
            Agent agent = AgentManager.GetAgentByName(npc.Name);
            agent.CurrentLocation = npc.Location;
            Dictionary<string, float> motives = npc.Motives;
            foreach (string mote in motives.Keys)
            {
                agent.Motives[mote] = motives[mote];
            }
        }

        public override void Run(int steps = 1)
        {
            ExecutionManager.RunSim(steps);
        }
    }
}
