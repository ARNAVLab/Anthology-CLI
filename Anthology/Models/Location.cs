using System.Text.Json.Serialization;

namespace Anthology.Models
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
        public HashSet<string> Tags { get; set; } = new HashSet<String>();

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
        private bool HasAllOf(HashSet<string> hasAllOf)
        {
            return hasAllOf.IsSubsetOf(Tags);
        }

        /// <summary>
        /// Checks if location satisfies at least one tag specified.
        /// 
        /// </summary>
        /// <param name="hasOneOrMoreOf">The set of tags to check.</param>
        /// <returns>True if location has at least one of the tags specified.</returns>
        private bool HasOneOrMoreOf(HashSet<string> hasOneOrMoreOf)
        {
            if (hasOneOrMoreOf.Count == 0) { return true; }
            return hasOneOrMoreOf.Overlaps(Tags);
        }

        /// <summary>
        /// Checks if this location has none of the given tags.
        /// </summary>
        /// <param name="hasNoneOf">The set of tags to check.</param>
        /// <returns>True if location has none of the given tags.</returns>
        private bool HasNoneOf(HashSet<string> hasNoneOf)
        {
            if (hasNoneOf.Count == 0) { return true; }
            return !hasNoneOf.Overlaps(Tags);
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
        private bool SpecificPeoplePresent(HashSet<string> specificPeoplePresent)
        {
            if (specificPeoplePresent.Count == 0) { return true; }
            return specificPeoplePresent.IsSubsetOf(AgentsPresent);
        }

        /// <summary>
        /// Checks if location does not have given people.
        /// </summary>
        /// <param name="specificPeopleAbsent">The set of people to check.</param>
        /// <returns>True if location does not have the given people.</returns>
        private bool SpecificPeopleAbsent(HashSet<string> specificPeopleAbsent)
        {
            if (specificPeopleAbsent.Count == 0) { return true; }
            return !specificPeopleAbsent.Overlaps(AgentsPresent);
        }

        /// <summary>
        /// Checks if given relationships are present at location.
        /// </summary>
        /// <param name="relationshipsPresent">The set of relationships to check.</param>
        /// <returns>True if given relationships are present at location.</returns>
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
    }
}
