﻿using System.Text.Json.Serialization;

namespace Anthology.Models.Deprecated
{
    /// <summary>
    /// Encapsulates a location within the simulation. A location can have a name, tags, a list of 
    /// agents occupying the location, and mandatory coordinates.
    /// </summary>
    public class SimLocation
    {
        /// <summary>
        /// Optional name of the location. Eg. Restaurant, Home, Movie Theatre, etc.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// X-coordinate of location.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y-coordinate of location.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Optional set of tags associated with the location. Eg. Restaurant could have 'food', 'delivery' as tags.
        /// </summary>
        public HashSet<string> Tags { get; set; } = new HashSet<string>();

        /// <summary>
        /// Set of agents at the location.
        /// </summary>
        [JsonIgnore]
        public HashSet<string> AgentsPresent { get; set; } = new HashSet<string>();

        /// <summary>
        /// Returns true if the specified agent is at this location.
        /// </summary>
        /// <param name="npc">The agent to determine if present at location.</param>
        /// <returns>True if agent is at location.</returns>
        public bool IsAgentHere(Agent npc)
        {
            return AgentsPresent.Contains(npc.Name);
        }

        /// <summary>
        /// Checks if this location satisfies all of the passed location requirements.
        /// </summary>
        /// <param name="reqs">Requirements to check for location.</param>
        /// <returns>True if location satisfies all requirements.</returns>
        public bool SatisfiesRequirements(RLocation reqs)
        {
            return HasAllOf(reqs.HasAllOf) &&
                   HasOneOrMoreOf(reqs.HasOneOrMoreOf) &&
                   HasNoneOf(reqs.HasNoneOf);
        }

        /// <summary>
        /// Checks if this location satisfies all of the passed people requirements.
        /// </summary>
        /// <param name="reqs">People requirements to check for.</param>
        /// <returns>True if location satisfies given requirements.</returns>
        public bool SatisfiesRequirements(RPeople reqs)
        {
            return HasMinNumPeople(reqs.MinNumPeople) &&
                   HasNotMaxNumPeople(reqs.MaxNumPeople) &&
                   SpecificPeoplePresent(reqs.SpecificPeoplePresent) &&
                   SpecificPeopleAbsent(reqs.SpecificPeopleAbsent) &&
                   RelationshipsPresent(reqs.RelationshipsPresent);
        }

        /// <summary>
        /// Checks if location has all tags specified.
        /// </summary>
        /// <param name="hasAllOf">All tags to check.</param>
        /// <returns>True if location has all tags given.</returns>
        private bool HasAllOf(IEnumerable<string> hasAllOf)
        {
            IEnumerator<string> enumerator = hasAllOf.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!Tags.Contains(enumerator.Current)) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if location satisfies at least one tag specified.
        /// </summary>
        /// <param name="hasOneOrMoreOf">The set of tags to check.</param>
        /// <returns>True if location has at least one of the tags specified.</returns>
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

        /// <summary>
        /// Checks if this location has none of the given tags.
        /// </summary>
        /// <param name="hasNoneOf">The set of tags to check.</param>
        /// <returns>True if location has none of the given tags.</returns>
        private bool HasNoneOf(IEnumerable<string> hasNoneOf)
        {
            IEnumerator<string> enumerator = hasNoneOf.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (Tags.Contains(enumerator.Current)) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if this location has at least a given amount of people.
        /// </summary>
        /// <param name="minNumPeople">The minimum amount of people.</param>
        /// <returns>True if location has at least the given amount of people.</returns>
        private bool HasMinNumPeople(short minNumPeople)
        {
            return minNumPeople <= AgentsPresent.Count;
        }

        /// <summary>
        /// Checks if location has less than or equal to given amount of people.
        /// </summary>
        /// <param name="maxNumPeople">The max amount of people.</param>
        /// <returns>True if location has less than or equal to given amount of people.</returns>
        private bool HasNotMaxNumPeople(short maxNumPeople)
        {
            return maxNumPeople >= AgentsPresent.Count;
        }

        /// <summary>
        /// Checks if location has given people.
        /// </summary>
        /// <param name="specificPeoplePresent">The set of people to check.</param>
        /// <returns>True if location has given people.</returns>
        private bool SpecificPeoplePresent(IEnumerable<string> specificPeoplePresent)
        {
            IEnumerator<string> enumerator = specificPeoplePresent.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!AgentsPresent.Contains(enumerator.Current)) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if location does not have given people.
        /// </summary>
        /// <param name="specificPeopleAbsent">The set of people to check.</param>
        /// <returns>True if location does not have the given people.</returns>
        private bool SpecificPeopleAbsent(IEnumerable<string> specificPeopleAbsent)
        {
            IEnumerator<string> enumerator = specificPeopleAbsent.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (AgentsPresent.Contains(enumerator.Current)) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if given relationships are present at location.
        /// </summary>
        /// <param name="relationshipsPresent">The set of relationships to check.</param>
        /// <returns>True if given relationships are present at location.</returns>
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
