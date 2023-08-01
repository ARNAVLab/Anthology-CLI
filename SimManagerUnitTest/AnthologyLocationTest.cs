using System.Text.Json;
using Anthology.Models;

namespace SimManagerUnitTest
{
    [TestClass]
    public class AnthologyLocationTest
    {
        HashSet<LocationNode> TestLocations = new();

        JsonSerializerOptions Jso { get; } = new()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

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
