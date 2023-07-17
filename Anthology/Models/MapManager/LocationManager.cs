using System.Numerics;
using System.Text.Json.Serialization;

namespace Anthology.Models.MapManager
{
    public static class LocationManager
    {
        public static Dictionary<string, LocationNode> LocationsByName { get; set; } = new();

        [JsonIgnore]
        public static Dictionary<Vector2, LocationNode> LocationsByPosition { get; set; } = new();

        public static void Init(string path)
        {
            // TODO
        }

        public static void AddLocation(LocationNode node)
        {
            LocationsByName.Add(node.Name, node);
            LocationsByPosition.Add(node.Position, node);

            foreach (Agent a in AgentManager.Agents)
            {
                if (a.XLocation == node.Position.X && a.YLocation == node.Position.Y)
                {
                    node.AgentsPresent.Add(a);
                }
            }
        }

        public static void AddLocation(string name, float x, float y, IEnumerable<string> tags, Dictionary<string, float> connections)
        {
            HashSet<string> newTags = new();
            newTags.UnionWith(tags);
            AddLocation(new() { Name = name, Position = new(x, y), Tags = newTags, ConnectedLocations = connections });
        }

        public static void UpdateClosestLocationsByTags()
        {
            HashSet<string> tags = new();
            foreach (LocationNode loc in LocationsByName.Values)
            {
                tags.UnionWith(loc.Tags);
            }

            foreach (LocationNode loc in LocationsByName.Values)
            {
                loc.UpdateClosestLocationsByTags(tags);
            }
        }
    }
}
