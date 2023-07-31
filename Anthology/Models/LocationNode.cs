﻿using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public int ID { get; set; }

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

        /** returns true if the specified agent is at this location */
        public bool IsAgentHere(Agent npc)
        {
            return AgentsPresent.Contains(npc.Name);
        }

        /** checks if this location satisfies the passed HasAllOf requirement */
        private bool HasAllOf(IEnumerable<string> hasAllOf)
        {
            IEnumerator<string> enumerator = hasAllOf.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!Tags.Contains(enumerator.Current)) return false;
            }
            return true;
        }

        /** checks if this location satisfies the HasOneOrMOreOf requirement */
        private bool HasOneOrMoreOf(IEnumerable<string> hasOneOrMoreOf)
        {
            IEnumerator<string> enumerator = hasOneOrMoreOf.GetEnumerator();
            if (!enumerator.MoveNext()) return true;
            do
            {
                if (Tags.Contains(enumerator.Current)) return true;
            } while (enumerator.MoveNext());
            return false;
        }

        /** checks if this location satisfies the HasNoneOf requirement */
        private bool HasNoneOf(IEnumerable<string> hasNoneOf)
        {
            IEnumerator<string> enumerator = hasNoneOf.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (Tags.Contains(enumerator.Current)) return false;
            }
            return true;
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
        private bool SpecificPeoplePresent(IEnumerable<string> specificPeoplePresent)
        {
            IEnumerator<string> enumerator = specificPeoplePresent.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!AgentsPresent.Contains(enumerator.Current)) return false;
            }
            return true;
        }

        /** checks if this location satisfies the SpecificPeopleAbsent requirement */
        private bool SpecificPeopleAbsent(IEnumerable<string> specificPeopleAbsent)
        {
            IEnumerator<string> enumerator = specificPeopleAbsent.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (AgentsPresent.Contains(enumerator.Current)) return false;
            }
            return true;
        }

        /** checks if this location satifies the RelationshipsPresent requirement */
        private bool RelationshipsPresent(IEnumerable<string> relationshipsPresent)
        {
            IEnumerator<string> enumerator = relationshipsPresent.GetEnumerator();
            if (!enumerator.MoveNext()) { return true; }
            List<string> relationshipsHere = new();
            foreach (string name in AgentsPresent)
            {
                IEnumerable<Relationship> ar = AgentManager.GetAgentByName(name).Relationships;
                foreach (Relationship r in ar)
                {
                    if (AgentsPresent.Contains(r.With))
                    {
                        relationshipsHere.Add(r.Type);
                    }
                }
            }
            do
            {
                if (!relationshipsHere.Contains(enumerator.Current)) return false;
            } while (enumerator.MoveNext());
            return true;
        }
    }
}
