using Anthology.Models;

namespace SimManagerUnitTest
{
    /// <summary>
    /// Tests Anthology's location system.
    /// </summary>
    [TestClass]
    public class AnthologyLocationTest
    {
        /// <summary>
        /// Set of locations used for testing
        /// </summary>
        private HashSet<LocationNode> TestLocations { get; set; } = new();

        /// <summary>
        /// Initializes some basic locations for testing.
        /// </summary>
        [TestInitialize] 
        public void MapLocations()
        {
            LocationManager.Reset();
            TestLocations.Clear();
            TestLocations.Add(new()
            {
                Name = "A",
                X = 0f,
                Y = 2f,
                Connections = { { "C", 1 } },
                Tags = { "Restaurant", "Food" }
            });

            TestLocations.Add(new()
            {
                Name = "B",
                X = 1f,
                Y = 2f,
                Connections = { { "D", 3 } },
                Tags = { "Recreational", "Park" }
            });

            TestLocations.Add(new()
            {
                Name = "C",
                X = 0f,
                Y = 1f,
                Connections = { { "A", 1 }, { "D", 5 }, { "F", 2 } },
                Tags = { "Park", "Pathway" }
            });

            TestLocations.Add(new()
            {
                Name = "D",
                X = 1f,
                Y = 1f,
                Connections = { { "B", 3 }, { "C", 5 }, { "E", 4 } },
                Tags = { "Recreational", "Pathway" }
            });

            TestLocations.Add(new()
            {
                Name = "F",
                X = 0f,
                Y = 0f,
                Connections = { { "C", 2 } },
                Tags = { "Food" }
            });

            TestLocations.Add(new()
            {
                Name = "E",
                X = 1f,
                Y = 0f,
                Connections = { { "D", 4 } },
                Tags = { "House" }
            });
        }

        /// <summary>
        /// Tests that the graph is populated and functions correctly.
        /// </summary>
        [TestMethod]
        public void TestLocationGraphFunctionality()
        {
            Assert.AreEqual(0, LocationManager.LocationsByName.Count);
            foreach (LocationNode node in TestLocations)
            {
                LocationManager.AddLocation(node);
            }
            LocationManager.UpdateDistanceMatrix();
            Assert.AreEqual(6, LocationManager.LocationsByName.Count);
            Assert.IsTrue(LocationManager.LocationsByName.ContainsKey("A"));
            Assert.IsTrue(LocationManager.LocationsByPosition.ContainsKey(new(0, 1)));
            Assert.IsTrue(LocationManager.LocationsByTag.ContainsKey("Park"));
            Assert.AreEqual(2, LocationManager.LocationsByTag["Park"].Count);
            Assert.AreEqual(1, LocationManager.LocationsByTag["House"].Count);
            LocationNode locA = LocationManager.LocationsByName["A"];
            LocationNode locC = LocationManager.LocationsByName["C"];
            LocationNode locE = LocationManager.LocationsByName["E"];
            Assert.AreEqual(0, LocationManager.DistanceMatrix[locA.ID * LocationManager.LocationCount + locA.ID]);
            Assert.AreEqual(1, LocationManager.DistanceMatrix[locA.ID * LocationManager.LocationCount + locC.ID]);
            Assert.AreEqual(10, LocationManager.DistanceMatrix[locA.ID * LocationManager.LocationCount + locE.ID]);
            Assert.AreEqual(10, LocationManager.DistanceMatrix[locE.ID * LocationManager.LocationCount + locA.ID]);
        }

        /// <summary>
        /// Tests some simple location manager functionality.
        /// </summary>
        [TestMethod]
        public void TestLocationManager()
        {
            LocationManager.Init("Data\\Locations2.json");
            Assert.AreEqual(6, LocationManager.LocationsByName.Count);
            Assert.IsTrue(LocationManager.LocationsByName.ContainsKey("Dorm"));
            Assert.IsTrue(LocationManager.LocationsByName.ContainsKey("Math Hall"));
            LocationNode mathHall = LocationManager.LocationsByName["Math Hall"];
            LocationNode dorm = LocationManager.LocationsByName["Dorm"];
            Assert.AreEqual(3, LocationManager.DistanceMatrix[mathHall.ID * LocationManager.LocationCount + dorm.ID]);
        }
    }
}
