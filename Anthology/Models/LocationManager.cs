namespace Anthology.Models
{
    /// <summary>
    /// Provides functionality for checking location-centric conditions.
    /// </summary>
    public static class LocationManager
    {
        /// <summary>
        /// Locations in the simulation as a set for set operations.
        /// </summary>
        public static HashSet<SimLocation> LocationSet { get; set; } = new HashSet<SimLocation>();

        /// <summary>
        /// Locations in the simulation as a grid for coordinate access.
        /// </summary>
        public static Dictionary<int, Dictionary<int, SimLocation>> LocationGrid { get; set; } = new Dictionary<int, Dictionary<int, SimLocation>>();

        /// <summary>
        /// Initialize/reset all static location manager variables and fill an empty N x N grid.
        /// </summary>
        /// <param name="n">Number of rows/columns of location grid.</param>
        /// <param name="path">Path of JSON file to load locations from.</param>
        public static void Init(int n, string path)
        {
            LocationSet.Clear();
            LocationGrid.Clear();
            for (int i = 0; i < n; i++)
            {
                LocationGrid[i] = new();
                for (int k = 0; k < n; k++)
                {
                    LocationGrid[i][k] = new SimLocation();
                }
            }
            World.ReadWrite.LoadLocationsFromFile(path);
        }

        /// <summary>
        /// Adds a location to both the location set and the location grid.
        /// </summary>
        /// <param name="location">The location to add.</param>
        public static void AddLocation(SimLocation location)
        {
            foreach (Agent a in AgentManager.Agents)
            {
                if (a.XLocation == location.X && a.YLocation == location.Y)
                {
                    location.AgentsPresent.Add(a.Name);
                }
            }
            LocationSet.Add(location);
            int max = Math.Max(location.X, location.Y);
            if (max >= UI.GridSize)
            {
                for (int i = UI.GridSize; i <= max; i++)
                {
                    LocationGrid.Add(i, new());
                    for (int k = 0; k <= max; k++)
                    {
                        LocationGrid[i].Add(k, new());
                    }
                }
                for (int i = 0; i < UI.GridSize; i++)
                {
                    for (int k = UI.GridSize; k <= max; k++)
                    {
                        LocationGrid[i].Add(k, new());
                    }
                }
                UI.GridSize = max + 1;
            }
            LocationGrid[location.X][location.Y] = location;
        }

        /// <summary>
        /// Creates and adds a location to both the location set and the location grid.
        /// </summary>
        /// <param name="name">Name of the location.</param>
        /// <param name="x">X-coordinate of the location.</param>
        /// <param name="y">Y-coordinate of the location.</param>
        /// <param name="tags">Relevant tags of the location.</param>
        public static void AddLocation(string name, int x, int y, IEnumerable<string> tags)
        {
            HashSet<string> newTags = new();
            newTags.UnionWith(tags);
            AddLocation(new() { Name = name, X = x, Y = y, Tags = newTags });
        }

        /// <summary>
        /// Finds the location with the matching name.
        /// </summary>
        /// <param name="name">The name of the location.</param>
        /// <returns>Location with name matching the given name.</returns>
        public static SimLocation GetSimLocationByName(string name)
        {
            bool IsNameMatch(SimLocation location)
            {
                return location.Name == name;
            }

            SimLocation location = LocationSet.First(IsNameMatch);
            return location;
        }

        /// <summary>
        /// Filter given set of locations to find those locations that satisfy conditions specified in the location requirement.
        /// Returns a set of locations that match the HasAllOf, HasOneOrMOreOf, and HasNoneOf constraints.
        /// Returns all the locations that satisfied the given requirement, or an empty set is none match.
        /// </summary>
        /// <param name="locations">The set of locations to filter.</param>
        /// <param name="requirements">Requirements that locations must satisfy in order to be returned.</param>
        /// <param name="agent">Agent relevant for handling agent requirement(s).</param>
        /// <returns></returns>
        public static HashSet<SimLocation> LocationsSatisfyingLocationRequirement(HashSet<SimLocation> locations, RLocation requirements, string agent = "")
        {
            bool IsLocationInvalid(SimLocation location)
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

            HashSet<SimLocation> satisfactoryLocations = new();
            satisfactoryLocations.UnionWith(locations);
            satisfactoryLocations.RemoveWhere(IsLocationInvalid);

            return satisfactoryLocations;
        }

        /// <summary>
        /// Filter given set of locations to find those locations that satisfy conditions specified in the people requirement.
        /// Returns a set of locations that match the MinNumPeople, MaxNumPeople, SpecificPeoplePresent, SpecificPeopleAbsent,
        /// RelationshipsPresent, and RelationshipsAbsent requirements.
        /// Returns all the locations that satisfied the given requirement, or an empty set is none match.
        /// </summary>
        /// <param name="locations">The set of locations to filter.</param>
        /// <param name="requirements">Requirements that locations must satisfy to be returned.</param>
        /// <param name="agent">Agent relevant for handling agent requirement(s).</param>
        /// <returns></returns>
        public static HashSet<SimLocation> LocationsSatisfyingPeopleRequirement(HashSet<SimLocation> locations, RPeople requirements, string agent = "")
        {
            bool IsLocationInvalid(SimLocation location)
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

            HashSet<SimLocation> satisfactoryLocations = new();
            satisfactoryLocations.UnionWith(locations);
            satisfactoryLocations.RemoveWhere(IsLocationInvalid);

            return satisfactoryLocations;
        }

        /// <summary>
        /// Returns the SimLocation at the given (X,Y) coordinates, or null if one does not exist.
        /// </summary>
        /// <param name="locations">The set of locations to search from.</param>
        /// <param name="x">X-coordinate of potential location to return.</param>
        /// <param name="y">Y-coordinate of potential location to return.</param>
        /// <returns>Location at designated coordinates. Null if not found.</returns>
        public static SimLocation? FindSimLocationAt(HashSet<SimLocation> locations, float x, float y)
        {
            foreach (SimLocation loc in locations)
            {
                if (loc.X == x && loc.Y == y)
                {
                    return loc;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the SimLocation nearest the given SimLocation, or null if one does not exist.
        /// </summary>
        /// <param name="locations">Set of locations to search from.</param>
        /// <param name="from">The location whose coordinates act as search origin.</param>
        /// <returns>Location closest to given location.</returns>
        public static SimLocation? FindNearestLocationFrom(HashSet<SimLocation> locations, SimLocation from)
        {
            return FindNearestLocationXY(locations, from.X, from.Y);
        }

        /// <summary>
        /// Returns the SimLocation nearest the given Agent, or null if one does not exist.
        /// </summary>
        /// <param name="locations">Set of locations to search from.</param>
        /// <param name="from">The agent whose coordinates act as search origin.</param>
        /// <returns>Location closest to agent.</returns>
        public static SimLocation? FindNearestLocationFrom(HashSet<SimLocation> locations, Agent from)
        {
            SimLocation locFrom = LocationGrid[from.XLocation][from.YLocation];
            return FindNearestLocationFrom(locations, locFrom);
        }


        /// <summary>
        /// Finds the manhattan distance between two locations.
        /// </summary>
        /// <param name="a">Location whose coordinates act as origin.</param>
        /// <param name="b">Location whose coordinates act as destination.</param>
        /// <returns>Distance between the two locations.</returns>
        public static int FindManhattanDistance(SimLocation a, SimLocation b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        /// <summary>
        /// Finds the manhattan distance between a location and specified (X,Y) coordinates.
        /// </summary>
        /// <param name="loc">Location whose coordinate acts as origin.</param>
        /// <param name="x">X-coordinate of destination.</param>
        /// <param name="y">Y-coordinate of destination.</param>
        /// <returns>Distance between the given location's coordinate and the (X, Y) coordinates.</returns>
        public static int FindManhattanDistance(SimLocation loc, int x, int y)
        {
            return Math.Abs(loc.X - x) + Math.Abs(loc.Y - y);
        }

        /// <summary>
        /// Helper function that finds the location nearest to the given (X,Y) coordinates.
        /// </summary>
        /// <param name="locations">Set of locations to search from.</param>
        /// <param name="x">X-coordinate of desired location.</param>
        /// <param name="y">Y-coordinate of desired location.</param>
        /// <returns>Location that is nearest to the given (X, Y) coordinates.</returns>
        private static SimLocation? FindNearestLocationXY(HashSet<SimLocation> locations, int x, int y)
        {
            HashSet<SimLocation> locationsToCheck = new();
            locationsToCheck.UnionWith(locations);

            if (locationsToCheck.Count == 0) return null;

            HashSet<SimLocation> closestSet = new();
            int closestDist = int.MaxValue;

            foreach (SimLocation loc in locationsToCheck)
            {
                int dist = FindManhattanDistance(loc, x, y);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestSet.Clear();
                    closestSet.Add(loc);
                }
                else if (dist == closestDist)
                {
                    closestSet.Add(loc);
                }
            }

            Random r = new();
            int idx = r.Next(0, closestSet.Count);
            return closestSet.ElementAt(idx);
        }
    }
}
