using Anthology.SimulationManager.HistoryManager;
using MongoDB.Driver;
using Anthology.SimulationManager;

namespace SimManagerUnitTest
{
    /// <summary>
    /// History Manager test class.
    /// </summary>
    [TestClass]
    public class HistoryManagerUnitTest
    {
        /// <summary>
        /// Path of test JSON file.
        /// </summary>
        private const string DATA_JSON = "Data\\Paths.json";

        /// <summary>
        /// MongoDB test database.
        /// </summary>
        private readonly IMongoDatabase db = new MongoClient("mongodb://localhost:27017/").GetDatabase("SimManager");

        /// <summary>
        /// Save state collection name.
        /// </summary>
        private const string SAVE_STATES_COLLECTION = "save_states";

        /// <summary>
        /// Tests initialization of the history manager.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            try
            {
                SimManager.Init(DATA_JSON, typeof(AnthologyRS), typeof(LyraKS), typeof(MongoHM));
                SimManager.History?.ClearStates();
                SimManager.History?.ClearLog("test_log");
            }
            catch (Exception e)
            {
                Assert.Fail("Failed to initialize Sim Manager: " + e.Message + "\n" + e.ToString());
            }
        }

        /// <summary>
        /// Tests history manager collections.
        /// </summary>
        [TestMethod]
        public void TestHistoryLogger()
        {
            MongoHM? mongoHM = SimManager.History as MongoHM;
            Assert.IsNotNull(mongoHM);
            Assert.IsTrue(mongoHM.IsConnected());
            Assert.IsFalse(db.ListCollectionNames().ToList().Contains(SAVE_STATES_COLLECTION));
            SimManager.History?.SaveState("test_state");
            Assert.IsTrue(db.ListCollectionNames().ToList().Contains("save_states"));
            Assert.IsTrue(db.GetCollection<SimState>("save_states").Find(simState => simState.SimName.Equals("test_state")).CountDocuments() > 0);

            SimManager.GetIteration();
            SimManager.LoadState("test_state");
        }
    }
}
