using System.Text.Json;

namespace Anthology.Models
{
    /// <summary>
    /// JSON serialization and deserialization manager for .NET framework to use.
    /// </summary>
    public class NetJson : JsonRW 
    {
        /// <summary>
        /// Gets the serialization options for JSON serialization.
        /// </summary>
        public static JsonSerializerOptions Jso { get; } = new()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        /// <summary>
        /// Initializes the world given a path to the JSON file containing paths for action, location, and agent
        /// JSON files.
        /// </summary>
        /// <param name="pathsFile">Path to JSON file containing paths to JSON files needed to initialize agents, locations, and actions.</param>
        /// <exception cref="FormatException">Thrown when JSON file does not have appropriate internal file paths.</exception>
        public override void InitWorldFromPaths(string pathsFile)
        {
            using FileStream os = File.OpenRead(pathsFile);
            Dictionary<string, string>? filePaths = JsonSerializer.Deserialize<Dictionary<string, string>>(os, Jso);
            if (filePaths == null || filePaths.Count < 3) { throw new FormatException("Unable to load Anthology world state from file"); ; }
            World.Init(filePaths["Actions"], filePaths["Agents"], filePaths["Locations"]);
        }

        /// <summary>
        /// Loads all actions from a JSON file.
        /// </summary>
        /// <param name="path">Path to JSON file containing all actions.</param>
        public override void LoadActionsFromFile(string path) 
        {
            string actionsText = File.ReadAllText(path);
            ActionContainer? actions = JsonSerializer.Deserialize<ActionContainer>(actionsText, Jso);
            if (actions == null) return;
            ActionManager.Actions = actions;
        }

        /// <summary>
        /// Serializes all actions and formats them to a string.
        /// </summary>
        /// <returns>String representation of all actions.</returns>
        public override string SerializeAllActions()
        {
            return JsonSerializer.Serialize(ActionManager.Actions, Jso);
        }

        /// <summary>
        /// Loads all agents from a JSON file.
        /// </summary>
        /// <param name="path">Path of JSON file to load agents from.</param>
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

        /// <summary>
        /// Serializes all agents and formats them into a string.
        /// </summary>
        /// <returns>String representation of all serialized agents.</returns>
        public override string SerializeAllAgents()
        {
            List<SerializableAgent> sAgents = new();
            foreach(Agent a in AgentManager.Agents)
            {
                sAgents.Add(SerializableAgent.SerializeAgent(a));
            }

            return JsonSerializer.Serialize(sAgents, Jso);
        }

        /// <summary>
        /// Loads all locations from a JSON file.
        /// </summary>
        /// <param name="path">Path of JSON file to load locations from.</param>
        public override void LoadLocationsFromFile(string path) 
        {
            string locationsText = File.ReadAllText(path);
            IEnumerable<SimLocation>? sLocations = JsonSerializer.Deserialize<IEnumerable<SimLocation>>(locationsText, Jso);

            if (sLocations == null) return;
            foreach (SimLocation l in sLocations)
            {
                LocationManager.AddLocation(l);
            }
        }

        /// <summary>
        /// Serializes all locations and formats them into a string.
        /// </summary>
        /// <returns>String representation of all serialized locations.</returns>
        public override string SerializeAllLocations()
        {
            static bool HasName(SimLocation simLocation)
            {
                return simLocation.Name != string.Empty;
            }

            IEnumerable<SimLocation> namedLocations = LocationManager.LocationSet.Where(HasName);
            return JsonSerializer.Serialize(namedLocations, Jso);
        }
    }
}