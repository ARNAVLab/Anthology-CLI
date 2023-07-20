using MongoDB.Bson.Serialization.IdGenerators;
using System.Numerics;
using System.Text.Json.Serialization;

namespace Anthology.Models.MapManager
{
    public static class LocationManager
    {
        public static Dictionary<string, LocationNode> LocationsByName { get; set; } = new();

        [JsonIgnore]
        public static Dictionary<Vector2, LocationNode> LocationsByPosition { get; set; } = new();

        [JsonIgnore]
        public static Dictionary<string, HashSet<LocationNode>> LocationsByTag { get; set; } = new();

        [JsonIgnore]
        public static Dictionary<string, Dictionary<string, float>> DistanceMatrix { get; set; } = new();
        public static void Init(string path)
        {
            // TODO
        }

        public static void AddLocation(LocationNode node)
        {
            LocationsByName.Add(node.Name, node);
            LocationsByPosition.Add(node.Position, node);
            DistanceMatrix.Add(node.Name, new());

            foreach (string tag in node.Tags)
            {
                if (!LocationsByTag.ContainsKey(tag))
                    LocationsByTag.Add(tag, new());
                LocationsByTag[tag].Add(node);
            }

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

        public static void UpdateDistanceMatrix()
        {
            foreach (string loc1 in LocationsByName.Keys)
            {
                foreach (string loc2 in LocationsByName.Keys)
                {
                    if (loc1 == loc2)
                    {
                        DistanceMatrix[loc1][loc2] = 0;
                    }
                    else
                    {
                        DistanceMatrix[loc1][loc2] = float.MaxValue;
                    }
                }
            }

            foreach (KeyValuePair<string, LocationNode> loc in LocationsByName)
            {
                foreach (KeyValuePair<string, float> con in loc.Value.ConnectedLocations)
                {
                    DistanceMatrix[loc.Key][con.Key] = con.Value;
                }
            }

            foreach (string loc1 in LocationsByName.Keys)
            {
                foreach (string loc2 in LocationsByName.Keys)
                {
                    foreach (string loc3 in LocationsByName.Keys)
                    {
                        if (DistanceMatrix[loc1][loc2] > DistanceMatrix[loc1][loc3] + DistanceMatrix[loc3][loc2])
                            DistanceMatrix[loc1][loc2] = DistanceMatrix[loc1][loc3] + DistanceMatrix[loc3][loc2];
                    }
                }
            }
        }
    }
}
