using System.Text.Json.Serialization;

namespace Anthology.Models
{
    public class LocationNode
    {
        public string Name { get; set; } = string.Empty;

        public float X { get; set; }

        public float Y { get; set; }

        public HashSet<string> Tags { get; set; } = new();

        public Dictionary<string, float> Connections { get; set; } = new();

        [JsonIgnore]
        public HashSet<string> AgentsPresent { get; set; } = new();

        /** checks if this location satisfies all of the passed location requirements */
        public bool SatisfiesRequirements(RLocation reqs)
        {
            return HasAllOf(reqs.HasAllOf) &&
                   HasOneOrMoreOf(reqs.HasOneOrMoreOf) &&
                   HasNoneOf(reqs.HasNoneOf);
        }

        /** checks if this location satisfies all of the passed people requirements */
        public bool SatisfiesRequirements(RPeople reqs)
        {
            return HasMinNumPeople(reqs.MinNumPeople) &&
                   HasNotMaxNumPeople(reqs.MaxNumPeople) &&
                   SpecificPeoplePresent(reqs.SpecificPeoplePresent) &&
                   SpecificPeopleAbsent(reqs.SpecificPeopleAbsent) &&
                   RelationshipsPresent(reqs.RelationshipsPresent);
        }

        /** checks if this location satisfies the passed HasAllOf requirement */
        private bool HasAllOf(HashSet<string> hasAllOf)
        {
            return hasAllOf.IsSubsetOf(Tags);
        }

        /** checks if this location satisfies the HasOneOrMOreOf requirement */
        private bool HasOneOrMoreOf(HashSet<string> hasOneOrMoreOf)
        {
            if (hasOneOrMoreOf.Count == 0) { return true; }
            return hasOneOrMoreOf.Overlaps(Tags);
        }

        /** checks if this location satisfies the HasNoneOf requirement */
        private bool HasNoneOf(HashSet<string> hasNoneOf)
        {
            if (hasNoneOf.Count == 0) { return true; }
            return !hasNoneOf.Overlaps(Tags);
        }

        /** checks if this location satisfies the MinNumPeople requirement */
        private bool HasMinNumPeople(short minNumPeople)
        {
            return minNumPeople <= AgentsPresent.Count;
        }

        /** checks if this location satifies the MaxNumPeople requirement */
        private bool HasNotMaxNumPeople(short maxNumPeople)
        {
            return maxNumPeople >= AgentsPresent.Count;
        }

        /** checks if this location satifies the SpecificPeoplePresent requirement */
        private bool SpecificPeoplePresent(HashSet<string> specificPeoplePresent)
        {
            if (specificPeoplePresent.Count == 0) { return true; }
            return specificPeoplePresent.IsSubsetOf(AgentsPresent);
        }

        /** checks if this location satisfies the SpecificPeopleAbsent requirement */
        private bool SpecificPeopleAbsent(HashSet<string> specificPeopleAbsent)
        {
            if (specificPeopleAbsent.Count == 0) { return true; }
            return !specificPeopleAbsent.Overlaps(AgentsPresent);
        }

        /** checks if this location satifies the RelationshipsPresent requirement */
        private bool RelationshipsPresent(HashSet<string> relationshipsPresent)
        {
            if (relationshipsPresent.Count == 0) { return true; }
            HashSet<string> relationshipsHere = new();
            foreach (string name in AgentsPresent)
            {
                HashSet<Relationship> ar = AgentManager.GetAgentByName(name).Relationships;
                foreach (Relationship r in ar)
                {
                    if (AgentsPresent.Contains(r.With))
                    {
                        relationshipsHere.Add(r.Type);
                    }
                }
            }
            return relationshipsPresent.IsSubsetOf(relationshipsHere);
        }

        /*[JsonIgnore]
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
        }*/
    }
}
