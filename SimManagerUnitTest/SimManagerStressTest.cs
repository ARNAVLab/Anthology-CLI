using System.Diagnostics;
using Anthology.Models;

namespace SimManagerUnitTest
{
    [TestClass]
    public class SimManagerStressTest
    {
        private const int NUM_ACTIONS = 10; // number of possible actions each agent can consider choosing
        private const int NUM_ITERATIONS = 60; // number of iterations to run each test
        private readonly static bool TEST_HUNDRED_AGENTS = true; // set to true to run tests for hundred agents
        private readonly static bool TEST_THOUSAND_AGENTS = false; // set to true to run tests for thousand agents
        private readonly static bool TEST_TEN_THOUSAND_AGENTS = false; // set to true to run tests for ten thousand agents
        private readonly static bool TEST_HUNDRED_THOUSAND_AGENTS = false; // set to true to run tests for hundred thousand agents
        private readonly static bool TEST_MILLION_AGENTS = false; // set to true to run tests for million agents
        private readonly static bool TEST_FIVE_LOCATIONS = false; // set to true to run tests for five locations
        private readonly static bool TEST_TEN_LOCATIONS = false; // set to true to run tests for ten locations
        private readonly static bool TEST_TWENTY_LOCATIONS = false; // set to true to run tests for twenty locations
        private readonly static bool TEST_FIFTY_LOCATIONS = false; // set to true to run tests for fifty locations
        private readonly static bool TEST_HUNDRED_LOCATIONS = false; // set to true to run tests for hundred locations
        private readonly static bool TEST_FIVE_HUNDRED_LOCATIONS = true; // set to true to run tests for five hundred locations
        private readonly static bool TEST_THOUSAND_LOCATIONS = true; // set to true to run tests for thousand locations
        private readonly static bool TEST_FIVE_THOUSAND_LOCATIONS = false; // set to true to run tests for five thousand locations
        private readonly static bool TEST_TEN_THOUSAND_LOCATIONS = false; // set to true to run tests for ten thousand locations

        [TestMethod]
        public void Test01HundredAgentsFiveLocations()
        {
            if (!TEST_HUNDRED_AGENTS || !TEST_FIVE_LOCATIONS) return;
            int numLocations = 5;
            AnthologyFactory.GenerateAgents(100, numLocations);
            AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);
            
            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(60);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test06HundredAgentsTenLocations()
        {
            if (!TEST_HUNDRED_AGENTS || !TEST_TEN_LOCATIONS) return;
            int numLocations = 10;
            AnthologyFactory.GenerateAgents(100, numLocations);
            AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test11HundredAgentsTwentyLocations()
        {
            if (!TEST_HUNDRED_AGENTS || !TEST_TWENTY_LOCATIONS) return;
            int numLocations = 20;
            AnthologyFactory.GenerateAgents(100, numLocations);
            AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test16HundredAgentsFiftyLocations()
        {
            if (!TEST_HUNDRED_AGENTS || !TEST_FIFTY_LOCATIONS) return;
            int numLocations = 50;
            AnthologyFactory.GenerateAgents(100, numLocations);
            AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test21HundredAgentsHundredLocations()
        {
            if (!TEST_HUNDRED_AGENTS || !TEST_HUNDRED_LOCATIONS) return;
            int numLocations = 100;
            AnthologyFactory.GenerateAgents(100, numLocations);
            AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test26HundredAgentsFiveHundredLocations()
        {
            if (!TEST_HUNDRED_AGENTS || !TEST_FIVE_HUNDRED_LOCATIONS) return;
            int numLocations = 500;
            AnthologyFactory.GenerateAgents(100, numLocations);
            AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test31HundredAgentsThousandLocations()
        {
            if (!TEST_HUNDRED_AGENTS || !TEST_THOUSAND_LOCATIONS) return;
            int numLocations = 1000;
            AnthologyFactory.GenerateAgents(100, numLocations);
            AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test36HundredAgentsFiveThousandLocations()
        {
            if (!TEST_HUNDRED_AGENTS || !TEST_FIVE_THOUSAND_LOCATIONS) return;
            int numLocations = 5000;
            AnthologyFactory.GenerateAgents(100, numLocations);
            AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test41HundredAgentsTenThousandLocations()
        {
            if (!TEST_HUNDRED_AGENTS || !TEST_TEN_THOUSAND_LOCATIONS) return;
            int numLocations = 10000;
            AnthologyFactory.GenerateAgents(100, numLocations);
            AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test02ThousandAgentsFiveLocations()
        {
            if (!TEST_THOUSAND_AGENTS || !TEST_FIVE_LOCATIONS) return;
            int numLocations = 5;
            AnthologyFactory.GenerateAgents(1000, numLocations);
            if (!TEST_HUNDRED_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test07ThousandAgentsTenLocations()
        {
            if (!TEST_THOUSAND_AGENTS || !TEST_TEN_LOCATIONS) return;
            int numLocations = 10;
            AnthologyFactory.GenerateAgents(1000, numLocations);
            if (!TEST_HUNDRED_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test12ThousandAgentsTwentyLocations()
        {
            if (!TEST_THOUSAND_AGENTS || !TEST_TWENTY_LOCATIONS) return;
            int numLocations = 20;
            AnthologyFactory.GenerateAgents(1000, numLocations);
            if (!TEST_HUNDRED_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test17ThousandAgentsFiftyLocations()
        {
            if (!TEST_THOUSAND_AGENTS || !TEST_FIFTY_LOCATIONS) return;
            int numLocations = 50;
            AnthologyFactory.GenerateAgents(1000, numLocations);
            if (!TEST_HUNDRED_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test22ThousandAgentsHundredLocations()
        {
            if (!TEST_THOUSAND_AGENTS || !TEST_HUNDRED_LOCATIONS) return;
            int numLocations = 100;
            AnthologyFactory.GenerateAgents(1000, numLocations);
            if (!TEST_HUNDRED_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test27ThousandAgentsFiveHundredLocations()
        {
            if (!TEST_THOUSAND_AGENTS || !TEST_FIVE_HUNDRED_LOCATIONS) return;
            int numLocations = 500;
            AnthologyFactory.GenerateAgents(1000, numLocations);
            if (!TEST_HUNDRED_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test32ThousandAgentsThousandLocations()
        {
            if (!TEST_THOUSAND_AGENTS || !TEST_THOUSAND_LOCATIONS) return;
            int numLocations = 1000;
            AnthologyFactory.GenerateAgents(1000, numLocations);
            if (!TEST_HUNDRED_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test37ThousandAgentsFiveThousandLocations()
        {
            if (!TEST_THOUSAND_AGENTS || !TEST_FIVE_THOUSAND_LOCATIONS) return;
            int numLocations = 5000;
            AnthologyFactory.GenerateAgents(1000, numLocations);
            if (!TEST_HUNDRED_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test42ThousandAgentsTenThousandLocations()
        {
            if (!TEST_THOUSAND_AGENTS || !TEST_TEN_THOUSAND_LOCATIONS) return;
            int numLocations = 10000;
            AnthologyFactory.GenerateAgents(1000, numLocations);
            if (!TEST_HUNDRED_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test03TenThousandAgentsFiveLocations()
        {
            if (!TEST_TEN_THOUSAND_AGENTS || !TEST_FIVE_LOCATIONS) return;
            int numLocations = 5;
            AnthologyFactory.GenerateAgents(10000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test08TenThousandAgentsTenLocations()
        {
            if (!TEST_TEN_THOUSAND_AGENTS || !TEST_TEN_LOCATIONS) return;
            int numLocations = 10;
            AnthologyFactory.GenerateAgents(10000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test13TenThousandAgentsTwentyLocations()
        {
            if (!TEST_TEN_THOUSAND_AGENTS || !TEST_TWENTY_LOCATIONS) return;
            int numLocations = 20;
            AnthologyFactory.GenerateAgents(10000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test18TenThousandAgentsFiftyLocations()
        {
            if (!TEST_TEN_THOUSAND_AGENTS || !TEST_FIFTY_LOCATIONS) return;
            int numLocations = 50;
            AnthologyFactory.GenerateAgents(10000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test23TenThousandAgentsHundredLocations()
        {
            if (!TEST_TEN_THOUSAND_AGENTS || !TEST_HUNDRED_LOCATIONS) return;
            int numLocations = 100;
            AnthologyFactory.GenerateAgents(10000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test28TenThousandAgentsFiveHundredLocations()
        {
            if (!TEST_TEN_THOUSAND_AGENTS || !TEST_FIVE_HUNDRED_LOCATIONS) return;
            int numLocations = 500;
            AnthologyFactory.GenerateAgents(10000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test33TenThousandAgentsThousandLocations()
        {
            if (!TEST_TEN_THOUSAND_AGENTS || !TEST_THOUSAND_LOCATIONS) return;
            int numLocations = 1000;
            AnthologyFactory.GenerateAgents(10000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test38TenThousandAgentsFiveThousandLocations()
        {
            if (!TEST_TEN_THOUSAND_AGENTS || !TEST_FIVE_THOUSAND_LOCATIONS) return;
            int numLocations = 5000;
            AnthologyFactory.GenerateAgents(10000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test43TenThousandAgentsTenThousandLocations()
        {
            if (!TEST_TEN_THOUSAND_AGENTS || !TEST_TEN_THOUSAND_LOCATIONS) return;
            int numLocations = 10000;
            AnthologyFactory.GenerateAgents(10000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test04HundredThousandAgentsFiveLocations()
        {
            if (!TEST_HUNDRED_THOUSAND_AGENTS || !TEST_FIVE_LOCATIONS) return;
            int numLocations = 5;
            AnthologyFactory.GenerateAgents(100000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test09HundredThousandAgentsTenLocations()
        {
            if (!TEST_HUNDRED_THOUSAND_AGENTS || !TEST_TEN_LOCATIONS) return;
            int numLocations = 10;
            AnthologyFactory.GenerateAgents(100000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test14HundredThousandAgentsTwentyLocations()
        {
            if (!TEST_HUNDRED_THOUSAND_AGENTS || !TEST_TWENTY_LOCATIONS) return;
            int numLocations = 20;
            AnthologyFactory.GenerateAgents(100000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test19HundredThousandAgentsFiftyLocations()
        {
            if (!TEST_HUNDRED_THOUSAND_AGENTS || !TEST_FIFTY_LOCATIONS) return;
            int numLocations = 50;
            AnthologyFactory.GenerateAgents(100000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test24HundredThousandAgentsHundredLocations()
        {
            if (!TEST_HUNDRED_THOUSAND_AGENTS || !TEST_HUNDRED_LOCATIONS) return;
            int numLocations = 100;
            AnthologyFactory.GenerateAgents(100000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test29HundredThousandAgentsFiveHundredLocations()
        {
            if (!TEST_HUNDRED_THOUSAND_AGENTS || !TEST_FIVE_HUNDRED_LOCATIONS) return;
            int numLocations = 500;
            AnthologyFactory.GenerateAgents(100000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test34HundredThousandAgentsThousandLocations()
        {
            if (!TEST_HUNDRED_THOUSAND_AGENTS || !TEST_THOUSAND_LOCATIONS) return;
            int numLocations = 1000;
            AnthologyFactory.GenerateAgents(100000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test39HundredThousandAgentsFiveThousandLocations()
        {
            if (!TEST_HUNDRED_THOUSAND_AGENTS || !TEST_FIVE_THOUSAND_LOCATIONS) return;
            int numLocations = 5000;
            AnthologyFactory.GenerateAgents(100000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test44HundredThousandAgentsTenThousandLocations()
        {
            if (!TEST_HUNDRED_THOUSAND_AGENTS || !TEST_TEN_THOUSAND_LOCATIONS) return;
            int numLocations = 10000;
            AnthologyFactory.GenerateAgents(100000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test05MillionAgentsFiveLocations()
        {
            if (!TEST_MILLION_AGENTS || !TEST_FIVE_LOCATIONS) return;
            int numLocations = 5;
            AnthologyFactory.GenerateAgents(1000000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS && !TEST_HUNDRED_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test10MillionAgentsTenLocations()
        {
            if (!TEST_MILLION_AGENTS || !TEST_TEN_LOCATIONS) return;
            int numLocations = 10;
            AnthologyFactory.GenerateAgents(1000000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS && !TEST_HUNDRED_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test15MillionAgentsTwentyLocations()
        {
            if (!TEST_MILLION_AGENTS || !TEST_TWENTY_LOCATIONS) return;
            int numLocations = 20;
            AnthologyFactory.GenerateAgents(1000000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS && !TEST_HUNDRED_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test20MillionAgentsFiftyLocations()
        {
            if (!TEST_MILLION_AGENTS || !TEST_FIFTY_LOCATIONS) return;
            int numLocations = 50;
            AnthologyFactory.GenerateAgents(1000000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS && !TEST_HUNDRED_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test25MillionAgentsHundredLocations()
        {
            if (!TEST_MILLION_AGENTS || !TEST_HUNDRED_LOCATIONS) return;
            int numLocations = 100;
            AnthologyFactory.GenerateAgents(1000000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS && !TEST_HUNDRED_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test30MillionAgentsFiveHundredLocations()
        {
            if (!TEST_MILLION_AGENTS || !TEST_FIVE_HUNDRED_LOCATIONS) return;
            int numLocations = 500;
            AnthologyFactory.GenerateAgents(1000000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS && !TEST_HUNDRED_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test35MillionAgentsThousandLocations()
        {
            if (!TEST_MILLION_AGENTS || !TEST_THOUSAND_LOCATIONS) return;
            int numLocations = 1000;
            AnthologyFactory.GenerateAgents(1000000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS && !TEST_HUNDRED_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test40MillionAgentsFiveThousandLocations()
        {
            if (!TEST_MILLION_AGENTS || !TEST_FIVE_THOUSAND_LOCATIONS) return;
            int numLocations = 5000;
            AnthologyFactory.GenerateAgents(1000000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS && !TEST_HUNDRED_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        [TestMethod]
        public void Test45MillionAgentsTenThousandLocations()
        {
            if (!TEST_MILLION_AGENTS || !TEST_TEN_THOUSAND_LOCATIONS) return;
            int numLocations = 10000;
            AnthologyFactory.GenerateAgents(1000000, numLocations);
            if (!TEST_HUNDRED_AGENTS && !TEST_THOUSAND_AGENTS && !TEST_TEN_THOUSAND_LOCATIONS && !TEST_HUNDRED_THOUSAND_AGENTS)
                AnthologyFactory.GenerateLocations(numLocations);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }
    }
}
