using MongoDB.Bson.Serialization.IdGenerators;
using System.Numerics;
using System.Text.Json.Serialization;

namespace Anthology.Models
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
            LocationsByName.Clear();
            LocationsByPosition.Clear();
            LocationsByTag.Clear();
            World.ReadWrite.LoadLocationsFromFile(path);
            UpdateDistanceMatrix();
        }

        public static void AddLocation(LocationNode node)
        {
            LocationsByName.Add(node.Name, node);
            LocationsByPosition.Add(new(node.X, node.Y), node);
            DistanceMatrix.Add(node.Name, new());

            foreach (string tag in node.Tags)
            {
                if (!LocationsByTag.ContainsKey(tag))
                    LocationsByTag.Add(tag, new());
                LocationsByTag[tag].Add(node);
            }

            foreach (Agent a in AgentManager.Agents)
            {
                if (a.XLocation == node.X && a.YLocation == node.Y)
                {
                    node.AgentsPresent.Add(a.Name);
                }
            }
        }

        public static void AddLocation(string name, float x, float y, IEnumerable<string> tags, Dictionary<string, float> connections)
        {
            HashSet<string> newTags = new();
            newTags.UnionWith(tags);
            AddLocation(new() { Name = name, X = x, Y = y, Tags = newTags, Connections = connections });
        }

        /*public static void UpdateClosestLocationsByTags()
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
        }*/

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
                foreach (KeyValuePair<string, float> con in loc.Value.Connections)
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
                        if (DistanceMatrix[loc2][loc3] > DistanceMatrix[loc2][loc1] + DistanceMatrix[loc1][loc3])
                            DistanceMatrix[loc2][loc3] = DistanceMatrix[loc2][loc1] + DistanceMatrix[loc1][loc3];
                    }
                }
            }
        }

        public static HashSet<LocationNode> LocationsSatisfyingLocationRequirement(RLocation requirements)
        {
            HashSet<LocationNode> matches = new();
            if (requirements.HasOneOrMoreOf.Any())
            {
                foreach (string tag in requirements.HasOneOrMoreOf)
                {
                    matches.UnionWith(LocationsByTag[tag]);
                }
            }
            else
            {
                matches.UnionWith(LocationsByName.Values);
            }
            if (requirements.HasAllOf.Any())
            {
                foreach (string tag in requirements.HasAllOf)
                {
                    matches.IntersectWith(LocationsByTag[tag]);
                }
            }
            if (requirements.HasNoneOf.Any())
            {
                foreach (string tag in requirements.HasNoneOf)
                {
                    matches.ExceptWith(LocationsByTag[tag]);
                }
            }
            return matches;
        }

        public static HashSet<LocationNode> LocationsSatisfyingPeopleRequirement(HashSet<LocationNode> locations, RPeople requirements, string agent = "")
        {
            bool IsLocationInvalid(LocationNode location)
            {
                if (agent == "" || location.AgentsPresent.Contains(agent))
                {
                    return !location.SatisfiesRequirements(requirements);
                }
                else
                {
                    location.AgentsPresent.Add(agent);
                    bool invalid = !location.SatisfiesRequirements(requirements);
                    location.AgentsPresent.Remove(agent);
                    return invalid;
                }
            }

            HashSet<LocationNode> satisfactoryLocations = new();
            satisfactoryLocations.UnionWith(locations);
            satisfactoryLocations.RemoveWhere(IsLocationInvalid);

            return satisfactoryLocations;
        }

        public static LocationNode FindNearestLocationFrom(LocationNode loc, HashSet<LocationNode> locations)
        {
            LocationNode nearest = locations.First();
            float dist = DistanceMatrix[loc.Name][nearest.Name];

            foreach (LocationNode location in locations)
            {
                if (dist > DistanceMatrix[loc.Name][location.Name])
                {
                    dist = DistanceMatrix[loc.Name][location.Name];
                    nearest = location;
                }
            }
            return nearest;
        }
    }
}
