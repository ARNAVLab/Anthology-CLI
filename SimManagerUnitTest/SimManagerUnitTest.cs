using Anthology.Models;
using Anthology.SimulationManager;
using Anthology.SimulationManager.HistoryManager;
using System.Text.Json;

namespace SimManagerUnitTest
{
    /// <summary>
    /// Simulation Manager Test class.
    /// </summary>
    [TestClass]
    public class SimManagerUnitTest
    {
        /// <summary>
        /// Path of test JSON file.
        /// </summary>
        private const string DATA_JSON = "Data\\Paths.json";

        /// <summary>
        /// Tests initialization of SimManager using concrete types AnthologyRS, LyraKS, and MongoHM.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            try
            {
                SimManager.Init(DATA_JSON, typeof(AnthologyRS), typeof(LyraKS), typeof(MongoHM));
            }
            catch (Exception e)
            {
                Assert.Fail("Failed to initialize Sim Manager: " + e.ToString());
            }
        }

        /// <summary>
        /// Tests Anthology-Lyra combination.
        /// </summary>
        [TestMethod]
        public void TestInitAnthologyLyra()
        {
            Assert.IsNotNull(SimManager.Reality);
            // Assert.IsNotNull(SimManager.Knowledge); ignored until LyraKS is implemented
            Assert.IsTrue(SimManager.NPCs.Count > 0);
            Assert.IsTrue(SimManager.Locations.Count > 0);
        }

        /// <summary>
        /// Tests only the knowledge sim.
        /// </summary>
        [TestMethod]
        public void TestKnowledge()
        {
            SimManager.Knowledge?.Run();

        }

        /// <summary>
        /// Tests only the reality sim.
        /// </summary>
        [TestMethod]
        public void TestReality()
        {
            Dictionary<string, NPC> npcs = new();
            SimManager.Reality?.LoadNpcs(npcs);
            Assert.IsTrue(npcs.Count > 0);
            Assert.IsTrue(npcs.ContainsKey("Norma"));
            Assert.IsTrue(npcs.ContainsKey("Abnorma"));
            Assert.IsTrue(npcs.ContainsKey("Quentin"));
            Assert.IsTrue(npcs.ContainsKey("MathProf"));
            Assert.IsTrue(npcs.ContainsKey("PhysicsProf"));
            Assert.IsTrue(npcs["Norma"].Motives.Count > 0);
            Assert.IsTrue(npcs["Norma"].Motives.ContainsKey("accomplishment"));
            Assert.IsTrue(npcs["Norma"].Motives.ContainsKey("emotional"));
            Assert.IsTrue(npcs["Norma"].Motives.ContainsKey("financial"));
            Assert.IsTrue(npcs["Norma"].Motives.ContainsKey("social"));
            Assert.IsTrue(npcs["Norma"].Motives.ContainsKey("physical"));
            Assert.AreEqual(2, npcs["Norma"].Motives["accomplishment"]);
            Assert.AreEqual(3, npcs["Norma"].Motives["emotional"]);
            Assert.AreEqual(5, npcs["Norma"].Motives["financial"]);
            Assert.AreEqual(1, npcs["Norma"].Motives["social"]);
            Assert.AreEqual(4, npcs["Norma"].Motives["physical"]);

            {
                NPC npc = new() { Name = "Norma" };
                SimManager.Reality?.UpdateNpc(npc);
                Assert.AreEqual(npc.Name, "Norma");
                Assert.AreEqual(npc.CurrentAction.Name, "wait_action");
            }

            Dictionary<Location.Coords, Location> locations = new();
            SimManager.Reality?.LoadLocations(locations);
            Assert.IsTrue(locations.Count > 0);
            Assert.IsTrue(locations.ContainsKey(new Location.Coords(5, 5)));
            Assert.IsTrue(locations.ContainsKey(new Location.Coords(4, 5)));
            Assert.IsTrue(locations.ContainsKey(new Location.Coords(3, 2)));
            Assert.IsTrue(locations.ContainsKey(new Location.Coords(1, 2)));
            Assert.IsTrue(locations.ContainsKey(new Location.Coords(1, 1)));
            Assert.IsTrue(locations.ContainsKey(new Location.Coords(1, 3)));
            Assert.AreEqual("Physics Hall", locations[new Location.Coords(1, 3)].Name);
            Assert.IsTrue(locations[new Location.Coords(1, 2)].Tags.Contains("outdoor"));
        }

        /// <summary>
        /// Tests running iterations of the SimManager.
        /// </summary>
        [TestMethod]
        public void TestIterations()
        {
            Assert.AreEqual("wait_action", SimManager.NPCs["Norma"].CurrentAction.Name);
            SimManager.GetIteration();
            Assert.AreEqual("travel_action", SimManager.NPCs["Norma"].CurrentAction.Name);
            SimManager.GetIteration(61);
            Assert.AreEqual("go_for_walk", SimManager.NPCs["Norma"].CurrentAction.Name);
        }

        /*
        /// <summary>
        /// Tests pushing locations from SimManager to RealitySim's LocationManager.
        /// </summary>
        [TestMethod]
        public void TestPushingLocationsFromFrontend()
        {
            SimManager.Locations.Clear();
            SimManager.Locations.Add(new(1, 1), new() { Coordinates = new() { X = 1, Y = 1 }, Name = "Gas Station", Tags = { "Gas", "Parking" } });
            SimManager.Locations.Add(new(2, 2), new() { Coordinates = new() { X = 2, Y = 2 }, Name = "Grocery Store", Tags = { "Food", "Parking" } });
            SimManager.Reality?.PushLocations();
            Assert.AreEqual(2, LocationManager.LocationSet.Count);
            Assert.AreEqual("Gas Station", LocationManager.LocationGrid[1][1].Name);
            Assert.AreEqual("Grocery Store", LocationManager.LocationGrid[2][2].Name);
        }
        */
    }
}