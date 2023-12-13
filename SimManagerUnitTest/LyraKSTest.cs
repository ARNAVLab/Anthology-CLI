using Anthology.SimulationManager;
using System.Text.Json;

namespace SimManagerUnitTest
{
    [TestClass]
    public class LyraKSTest
    {

        public LyraKS lyra = new();

        [TestMethod]
        public void TestSetup()
        {
            lyra.Init("Data\\Lyra\\Lyra.json");
            Assert.IsTrue(LyraKS.simUrl.Contains("simulation/"));
        }
    }
}
