using System.Text.Json;

namespace Anthology.Models
{
    public class NetJson : JsonRW 
    {
        public static JsonSerializerOptions Jso { get; } = new()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        public override void InitWorldFromPaths(string pathsFile)
        {
            using FileStream os = File.OpenRead(pathsFile);
            Dictionary<string, string>? filePaths = JsonSerializer.Deserialize<Dictionary<string, string>>(os, Jso);
            if (filePaths == null || filePaths.Count < 3) { throw new FormatException("Unable to load Anthology world state from file"); ; }
            World.Init(filePaths["Actions"], filePaths["Agents"], filePaths["Locations"]);
        }

        public override void LoadActionsFromFile(string path) 
        {
            string actionsText = File.ReadAllText(path);
            ActionContainer? actions = JsonSerializer.Deserialize<ActionContainer>(actionsText, Jso);
            if (actions == null) return;
            ActionManager.Actions = actions;
        }

        public override string SerializeAllActions()
        {
            return JsonSerializer.Serialize(ActionManager.Actions, Jso);
        }

        public override void LoadAgentsFromFile(string path) 
        {
            string agentsText = File.ReadAllText(path);
            List<SerializableAgent>? sAgents = JsonSerializer.Deserialize<List<SerializableAgent>>(agentsText, Jso);

            if (sAgents == null) return;
            foreach (SerializableAgent s in sAgents)
            {
                AgentManager.Agents.Add(SerializableAgent.DeserializeToAgent(s));
            }
        }

        public override string SerializeAllAgents()
        {
            List<SerializableAgent> sAgents = new();
            foreach(Agent a in AgentManager.Agents)
            {
                sAgents.Add(SerializableAgent.SerializeAgent(a));
            }

            return JsonSerializer.Serialize(sAgents, Jso);
        }

        public override void LoadLocationsFromFile(string path) 
        {
            string locationsText = File.ReadAllText(path);
            IEnumerable<LocationNode>? locationNodes = JsonSerializer.Deserialize<IEnumerable<LocationNode>>(locationsText, Jso);

            if (locationNodes == null) return;
            foreach (LocationNode node in locationNodes)
            {
                LocationManager.AddLocation(node);
            }
        }

        public override string SerializeAllLocations()
        {
            return JsonSerializer.Serialize(LocationManager.LocationsByName.Values, Jso);
        }
    }
}