using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Anthology.Models.MapManager;
using MongoDB.Bson;

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
            TestLocations.Clear();
            TestLocations.Add(new()
            {
                Name = "A",
                Position = new(0, 2),
                ConnectedLocations = { { "C", 1 } },
                Tags = { "Restaurant", "Food" }
            });

            TestLocations.Add(new()
            {
                Name = "B",
                Position = new(1, 2),
                ConnectedLocations = { { "D", 3 } },
                Tags = { "Recreational", "Park" }
            });

            TestLocations.Add(new()
            {
                Name = "C",
                Position = new(0, 1),
                ConnectedLocations = { { "A", 1 }, { "D", 5 }, { "F", 2 } },
                Tags = { "Park", "Pathway" }
            });

            TestLocations.Add(new()
            {
                Name = "D",
                Position = new(1, 1),
                ConnectedLocations = { { "B", 3 }, { "C", 5 }, { "E", 4 } },
                Tags = { "Recreational", "Pathway" }
            });

            TestLocations.Add(new()
            {
                Name = "F",
                Position = new(0, 0),
                ConnectedLocations = { { "C", 2 } },
                Tags = { "Food" }
            });

            TestLocations.Add(new()
            {
                Name = "E",
                Position = new(1, 0),
                ConnectedLocations = { { "D", 4 } },
                Tags = { "House" }
            });
        }

        [TestMethod]
        public void TestLocationManager()
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
            Assert.AreEqual(0, LocationManager.DistanceMatrix["A"]["A"]);
            Assert.AreEqual(1, LocationManager.DistanceMatrix["A"]["C"]);
            Assert.AreEqual(10, LocationManager.DistanceMatrix["A"]["E"]);
            Assert.AreEqual("", JsonSerializer.Serialize(LocationManager.DistanceMatrix, Jso));
        }
    }
}
