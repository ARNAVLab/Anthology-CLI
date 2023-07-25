using System.Numerics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
            Reset();
            World.ReadWrite.LoadLocationsFromFile(path);
            UpdateDistanceMatrix();
        }

        public static void Reset()
        {
            LocationsByName.Clear();
            LocationsByPosition.Clear();
            LocationsByTag.Clear();
            DistanceMatrix.Clear();
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
            foreach (Agent agent in AgentManager.Agents)
            {
                if (agent.CurrentLocation == node.Name)
                    node.AgentsPresent.Add(agent.Name);
            }
        }

        public static void AddLocation(string name, float x, float y, IEnumerable<string> tags, Dictionary<string, float> connections)
        {
            HashSet<string> newTags = new();
            newTags.UnionWith(tags);
            AddLocation(new() { Name = name, X = x, Y = y, Tags = newTags, Connections = connections });
        }

        public static void UpdateDistanceMatrix()
        { 
            Parallel.ForEach(LocationsByName.Keys, loc1 =>
            {
                foreach (string loc2 in LocationsByName.Keys)
                {
                    if (loc1 == loc2) DistanceMatrix[loc1][loc2] = 0;
                    else DistanceMatrix[loc1][loc2] = float.MaxValue;
                }
            });

            Parallel.ForEach(LocationsByName, loc =>
            {
                foreach (KeyValuePair<string, float> con in loc.Value.Connections)
                    DistanceMatrix[loc.Key][con.Key] = con.Value;
            });

            Parallel.ForEach(LocationsByName.Keys, loc1 =>
            {
            foreach (string loc2 in LocationsByName.Keys)
            {
                foreach (string loc3 in LocationsByName.Keys)
                {
                        if (DistanceMatrix[loc2][loc3] > DistanceMatrix[loc2][loc1] + DistanceMatrix[loc1][loc3])
                            DistanceMatrix[loc2][loc3] = DistanceMatrix[loc2][loc1] + DistanceMatrix[loc1][loc3];
                    }
                }
            });
        }

        public static HashSet<LocationNode> LocationsSatisfyingLocationRequirement(RLocation requirements)
        {
            ParallelQuery<LocationNode> matches = ParallelEnumerable.Empty<LocationNode>();
            if (requirements.HasOneOrMoreOf.Any())
            {
                foreach (string tag in requirements.HasOneOrMoreOf)
                {
                    matches.Concat(LocationsByTag[tag].AsParallel());
                }
            }
            else
            {
                matches.Concat(LocationsByName.Values.AsParallel());
            }
            if (requirements.HasAllOf.Any())
            {
                foreach (string tag in requirements.HasAllOf)
                {
                    matches.Intersect(LocationsByTag[tag].AsParallel());
                }
            }
            if (requirements.HasNoneOf.Any())
            {
                foreach (string tag in requirements.HasNoneOf)
                {
                    matches.Except(LocationsByTag[tag].AsParallel());
                }
            }
            return matches.ToHashSet();
        }

        public static HashSet<LocationNode> LocationsSatisfyingPeopleRequirement(HashSet<LocationNode> locations, RPeople requirements, string agent = "")
        {
            bool IsLocationValid(LocationNode location)
            {
                if (agent == "" || location.AgentsPresent.Contains(agent))
                {
                    return location.SatisfiesRequirements(requirements);
                }
                else
                {
                    location.AgentsPresent.Add(agent);
                    bool valid = location.SatisfiesRequirements(requirements);
                    location.AgentsPresent.Remove(agent);
                    return valid;
                }
            }
            ParallelQuery<LocationNode> satisfactoryLocations;
            satisfactoryLocations = ParallelEnumerable.Where(locations.AsParallel(), IsLocationValid);

            return satisfactoryLocations.ToHashSet();
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

       /*     Parallel.ForEach(locations, location =>
            {
                if (dist > DistanceMatrix[loc.Name][location.Name])
                {
                    dist = DistanceMatrix[loc.Name][location.Name];
                    nearest = location;
                }
            });*/
            return nearest;
        }
    }
}
