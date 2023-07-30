﻿using System.Collections;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Anthology.Models
{
    public static class LocationManager
    {
        public static Dictionary<string, LocationNode> LocationsByName { get; set; } = new();

        public static Dictionary<Vector2, LocationNode> LocationsByPosition { get; set; } = new();

        public static Dictionary<string, HashSet<LocationNode>> LocationsByTag { get; set; } = new();

        //public static Dictionary<string, Dictionary<string, float>> DistanceMatrix { get; set; } = new();

        public static int LocationCount { get; set; } = 0;

        public static float[] DistanceMat { get; set; } = Array.Empty<float>();

        public static void Init(string path)
        {
            Reset();
            World.ReadWrite.LoadLocationsFromFile(path);
            UpdateDistanceMat();
        }

        public static void Reset()
        {
            LocationsByName.Clear();
            LocationsByPosition.Clear();
            LocationsByTag.Clear();
            DistanceMat = Array.Empty<float>();
            LocationCount = 0;
        }

        public static void AddLocation(LocationNode node)
        {
            LocationsByName.Add(node.Name, node);
            LocationsByPosition.Add(new(node.X, node.Y), node);
            node.ID = LocationCount++;

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

        /*public static void UpdateDistanceMatrix()
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
        }*/

        public static void UpdateDistanceMat()
        {
            DistanceMat = new float[LocationCount * LocationCount];
            Parallel.For(0, LocationCount, loc1 =>
            {
                for (int loc2 = 0; loc2 < LocationCount; loc2++)
                {
                    if (loc1 == loc2) DistanceMat[loc1 * LocationCount + loc2] = 0;
                    else DistanceMat[loc1 * LocationCount + loc2] = (float.MaxValue / 2f) - 1f;
                }
            });
            IEnumerable<LocationNode> locationNodes = LocationsByName.Values;
            Parallel.ForEach(locationNodes, loc1 =>
            {
                foreach (KeyValuePair<string, float> con in loc1.Connections)
                {
                    LocationNode loc2 = LocationsByName[con.Key];
                    DistanceMat[loc1.ID * LocationCount + loc2.ID] = con.Value;
                }
            });
            for (int k = 0; k < LocationCount; k++)
            {
                Parallel.For(0, LocationCount, i =>
                {
                    for (int j = 0; j < LocationCount; j++)
                    {
                        float d = DistanceMat[i * LocationCount + k] + DistanceMat[k * LocationCount + j];
                        if (DistanceMat[i * LocationCount + j] > DistanceMat[i * LocationCount + k] + DistanceMat[k * LocationCount + j])
                            DistanceMat[i * LocationCount + j] = DistanceMat[i * LocationCount + k] + DistanceMat[k * LocationCount + j];
                    }
                });
            }
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

        public static LocationNode FindNearestLocationFrom(LocationNode loc, IEnumerable<LocationNode> locations)
        {
            IEnumerator<LocationNode> iterator = locations.GetEnumerator();
            iterator.MoveNext();
            LocationNode nearest = iterator.Current;
            float dist = DistanceMat[loc.ID * LocationCount + nearest.ID];

            while (iterator.MoveNext())
            {
                if (dist > DistanceMat[loc.ID * LocationCount + iterator.Current.ID])
                {
                    nearest = iterator.Current;
                    dist = DistanceMat[loc.ID * LocationCount + nearest.ID];
                }
            }

            return nearest;
        }
    }
}
