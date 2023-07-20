using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Anthology.Models.MapManager
{
    public class LocationNode
    {
        public string Name { get; set; } = string.Empty;

        public Vector2 Position { get; set; }

        public HashSet<string> Tags { get; set; } = new();

        public Dictionary<string, float> ConnectedLocations { get; set; } = new();

        [JsonIgnore]
        public HashSet<Agent> AgentsPresent { get; set; } = new();

        [JsonIgnore]
        public Dictionary<string, Tuple<LocationNode, float>> ClosestLocationByTag { get; set; } = new();

        public void UpdateClosestLocationsByTags(HashSet<string> tags)
        {
            ClosestLocationByTag.Clear();
            foreach (string tag in tags)
            {
                ClosestLocationByTag.Add(tag, FindClosestLocationByTag(tag));
            }
        }

        private Tuple<LocationNode, float> FindClosestLocationByTag(string tag)
        {
            Dictionary<string, float> matches = new();
            HashSet<string> searched = new();
            float shortest = float.MaxValue;
            ClosestLocationHelper(tag, ref shortest, matches, searched);

            foreach (KeyValuePair<string, float> kvp in matches)
            {
                if (kvp.Value == shortest)
                {
                    return new Tuple<LocationNode, float>(LocationManager.LocationsByName[kvp.Key], shortest);
                }
            }

            throw new ArgumentException("Unable to reach a location with tag " + tag + " from " + Name);
        }

        private void ClosestLocationHelper(string tag, ref float shortest, Dictionary<string, float> matches, HashSet<string> searched, float csf = 0)
        {
            searched.Add(this.Name);
            if (Tags.Contains(tag))
            {
                matches.Add(this.Name, csf);
                if (csf < shortest)
                    shortest = csf;
                return;
            }
            
            foreach (KeyValuePair<string, float> kvp in ConnectedLocations)
            {
                if (searched.Contains(kvp.Key) || csf + kvp.Value > shortest) continue;
                LocationManager.LocationsByName[kvp.Key].ClosestLocationHelper(tag, ref shortest, matches, searched, csf + kvp.Value);
            }
        }
    }
}
