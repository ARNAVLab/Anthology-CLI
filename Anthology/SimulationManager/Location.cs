using MongoDB.Bson.Serialization.Attributes;
using System.Numerics;

namespace Anthology.SimulationManager
{
    /**
     * Locations as they exist between the frontend and backend simulations.
     * Currently these are expected to come exclusively from the RealitySim
     * and are accessible to the frontend, but future implementations should
     * support the frontend informing the manager of locations as well
     */
    public class Location
    {
        /** The name of the location */
        public string Name { get; set; } = string.Empty;

        public struct Coords
        {
            public float X { get; set; }
            public float Y { get; set; }

            public Coords()
            {
                X = 0;
                Y = 0;
            }

            [BsonConstructor]
            public Coords(float x, float y)
            {
                X = x;
                Y = y;
            }
        }

        [BsonId]
        /** The (X,Y) position of the location */
        public Coords Coordinates { get; set; } = new();

        /** Arbitrary set of tags associated with the location */
        public HashSet<string> Tags { get; set; } = new();

        /// <summary>
        /// Directly pathable connections between locations and their distances
        /// Effectively out-edges in graph theory
        /// </summary>
        public Dictionary<string, float> Connections { get; set; } = new();
    }
}
