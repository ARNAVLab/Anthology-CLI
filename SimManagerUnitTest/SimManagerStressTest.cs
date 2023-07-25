using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anthology.Models;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microsoft.Win32.SafeHandles;

namespace SimManagerUnitTest
{
    /// <summary>
    /// Stress test class for simulation manager.
    /// </summary>
    [TestClass]
    public class SimManagerStressTest
    {
        /// <summary>
        /// Number of possible actions each agent can consider choosing.
        /// </summary>
        private const int NUM_ACTIONS = 10;

        /// <summary>
        /// Number of iterations to run each test.
        /// </summary>
        private const int NUM_ITERATIONS = 60;

        /// <summary>
        /// Size of the map grid for each test.
        /// </summary>
        private const int GRID_SIZE = 400;

        /// <summary>
        /// Set to true to run tests for hundred agents.
        /// </summary>
        private readonly static bool TEST_HUNDRED = false;

        /// <summary>
        /// Set to true to run tests for thousand agents.
        /// </summary>
        private readonly static bool TEST_THOUSAND = false;

        /// <summary>
        /// Set to true to run tests for ten thousand agents.
        /// </summary>
        private readonly static bool TEST_TEN_THOUSAND = false;

        /// <summary>
        /// Set to true to run tests for hundred thousand agents.
        /// </summary>
        private readonly static bool TEST_HUNDRED_THOUSAND = false;

        /// <summary>
        /// Set to true to run tests for million agents.
        /// </summary>
        private readonly static bool TEST_MILLION = false;

        /// <summary>
        /// Tests generating 100 agents and 5 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredAgentsFiveLocations()
        {
            if (!TEST_HUNDRED) return;
            AnthologyFactory.GenerateAgents(100, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(5, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);
            
            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100 agents and 10 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredAgentsTenLocations()
        {
            if (!TEST_HUNDRED) return;
            AnthologyFactory.GenerateAgents(100, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(10, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100 agents and 20 locations.
        /// </summary>

        [TestMethod]
        public void TestHundredAgentsTwentyLocations()
        {
            if (!TEST_HUNDRED) return;
            AnthologyFactory.GenerateAgents(100, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(20, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100 agents with 50 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredAgentsFiftyLocations()
        {
            if (!TEST_HUNDRED) return;
            AnthologyFactory.GenerateAgents(100, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(50, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100 agents and 100 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredAgentsHundredLocations()
        {
            if (!TEST_HUNDRED) return;
            AnthologyFactory.GenerateAgents(100, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(100, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100 agents and 500 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredAgentsFiveHundredLocations()
        {
            if (!TEST_HUNDRED) return;
            AnthologyFactory.GenerateAgents(100, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(500, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100 agents and 1000 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredAgentsThousandLocations()
        {
            if (!TEST_HUNDRED) return;
            AnthologyFactory.GenerateAgents(100, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(1000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100 agents and 5000 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredAgentsFiveThousandLocations()
        {
            if (!TEST_HUNDRED) return;
            AnthologyFactory.GenerateAgents(100, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(5000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100 agents and 10000 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredAgentsTenThousandLocations()
        {
            if (!TEST_HUNDRED) return;
            AnthologyFactory.GenerateAgents(100, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(10000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000 agents and 5 locations.
        /// </summary>
        [TestMethod]
        public void TestThousandAgentsFiveLocations()
        {
            if (!TEST_THOUSAND) return;
            AnthologyFactory.GenerateAgents(1000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(5, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000 agents and 10 locations.
        /// </summary>
        [TestMethod]
        public void TestThousandAgentsTenLocations()
        {
            if (!TEST_THOUSAND) return;
            AnthologyFactory.GenerateAgents(1000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(10, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000 agents and 20 locations.
        /// </summary>
        [TestMethod]
        public void TestThousandAgentsTwentyLocations()
        {
            if (!TEST_THOUSAND) return;
            AnthologyFactory.GenerateAgents(1000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(20, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000 agents and 50 locations.
        /// </summary>
        [TestMethod]
        public void TestThousandAgentsFiftyLocations()
        {
            if (!TEST_THOUSAND) return;
            AnthologyFactory.GenerateAgents(1000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(50, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000 agents and 100 locations.
        /// </summary>
        [TestMethod]
        public void TestThousandAgentsHundredLocations()
        {
            if (!TEST_THOUSAND) return;
            AnthologyFactory.GenerateAgents(1000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(100, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000 agents and 500 locations.
        /// </summary>
        [TestMethod]
        public void TestThousandAgentsFiveHundredLocations()
        {
            if (!TEST_THOUSAND) return;
            AnthologyFactory.GenerateAgents(1000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(500, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000 agents and 1000 locations.
        /// </summary>
        [TestMethod]
        public void TestThousandAgentsThousandLocations()
        {
            if (!TEST_THOUSAND) return;
            AnthologyFactory.GenerateAgents(1000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(1000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000 agents and 5000 locations.
        /// </summary>
        [TestMethod]
        public void TestThousandAgentsFiveThousandLocations()
        {
            if (!TEST_THOUSAND) return;
            AnthologyFactory.GenerateAgents(1000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(5000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000 agents and 10000 locations.
        /// </summary>
        [TestMethod]
        public void TestThousandAgentsTenThousandLocations()
        {
            if (!TEST_THOUSAND) return;
            AnthologyFactory.GenerateAgents(1000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(10000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 10000 agents and 5 locations.
        /// </summary>
        [TestMethod]
        public void TestTenThousandAgentsFiveLocations()
        {
            if (!TEST_TEN_THOUSAND) return; 
            AnthologyFactory.GenerateAgents(10000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(5, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 10000 agents and 10 locations.
        /// </summary>
        [TestMethod]
        public void TestTenThousandAgentsTenLocations()
        {
            if (!TEST_TEN_THOUSAND) return;
            AnthologyFactory.GenerateAgents(10000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(10, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 10000 agents and 20 locations.
        /// </summary>
        [TestMethod]
        public void TestTenThousandAgentsTwentyLocations()
        {
            if (!TEST_TEN_THOUSAND) return;
            AnthologyFactory.GenerateAgents(10000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(20, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 10000 agents and 50 locations.
        /// </summary>
        [TestMethod]
        public void TestTenThousandAgentsFiftyLocations()
        {
            if (!TEST_TEN_THOUSAND) return;
            AnthologyFactory.GenerateAgents(10000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(50, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 10000 agents and 100 locations.
        /// </summary>
        [TestMethod]
        public void TestTenThousandAgentsHundredLocations()
        {
            if (!TEST_TEN_THOUSAND) return;
            AnthologyFactory.GenerateAgents(10000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(100, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 10000 agents and 500 locations.
        /// </summary>
        [TestMethod]
        public void TestTenThousandAgentsFiveHundredLocations()
        {
            if (!TEST_TEN_THOUSAND) return;
            AnthologyFactory.GenerateAgents(10000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(500, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 10000 agents and 1000 locations.
        /// </summary>
        [TestMethod]
        public void TestTenThousandAgentsThousandLocations()
        {
            if (!TEST_TEN_THOUSAND) return;
            AnthologyFactory.GenerateAgents(10000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(1000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 10000 agents and 5000 locations.
        /// </summary>
        [TestMethod]
        public void TestTenThousandAgentsFiveThousandLocations()
        {
            if (!TEST_TEN_THOUSAND) return;
            AnthologyFactory.GenerateAgents(10000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(5000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 10000 agents and 10000 locations.
        /// </summary>
        [TestMethod]
        public void TestTenThousandAgentsTenThousandLocations()
        {
            if (!TEST_TEN_THOUSAND) return;
            AnthologyFactory.GenerateAgents(10000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(10000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100000 agents and 5 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredThousandAgentsFiveLocations()
        {
            if (!TEST_HUNDRED_THOUSAND) return;
            AnthologyFactory.GenerateAgents(100000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(5, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100000 agents and 10 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredThousandAgentsTenLocations()
        {
            if (!TEST_HUNDRED_THOUSAND) return;
            AnthologyFactory.GenerateAgents(100000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(10, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100000 agents and 20 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredThousandAgentsTwentyLocations()
        {
            if (!TEST_HUNDRED_THOUSAND) return;
            AnthologyFactory.GenerateAgents(100000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(20, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100000 agents and 50 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredThousandAgentsFiftyLocations()
        {
            if (!TEST_HUNDRED_THOUSAND) return;
            AnthologyFactory.GenerateAgents(100000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(50, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100000 agents and 100 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredThousandAgentsHundredLocations()
        {
            if (!TEST_HUNDRED_THOUSAND) return;
            AnthologyFactory.GenerateAgents(100000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(100, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100000 agents and 500 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredThousandAgentsFiveHundredLocations()
        {
            if (!TEST_HUNDRED_THOUSAND) return;
            AnthologyFactory.GenerateAgents(100000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(500, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100000 agents and 1000 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredThousandAgentsThousandLocations()
        {
            if (!TEST_HUNDRED_THOUSAND) return;
            AnthologyFactory.GenerateAgents(100000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(1000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100000 agents and 5000 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredThousandAgentsFiveThousandLocations()
        {
            if (!TEST_HUNDRED_THOUSAND) return;
            AnthologyFactory.GenerateAgents(100000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(5000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 100000 agents and 10000 locations.
        /// </summary>
        [TestMethod]
        public void TestHundredThousandAgentsTenThousandLocations()
        {
            if (!TEST_HUNDRED_THOUSAND) return;
            AnthologyFactory.GenerateAgents(100000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(10000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000000 agents and 5 locations.
        /// </summary>
        [TestMethod]
        public void TestMillionAgentsFiveLocations()
        {
            if (!TEST_MILLION) return;
            AnthologyFactory.GenerateAgents(1000000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(5, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000000 agents and 10 locations.
        /// </summary>
        [TestMethod]
        public void TestMillionAgentsTenLocations()
        {
            if (!TEST_MILLION) return;
            AnthologyFactory.GenerateAgents(1000000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(10, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000000 agents and 20 locations.
        /// </summary>
        [TestMethod]
        public void TestMillionAgentsTwentyLocations()
        {
            if (!TEST_MILLION) return;
            AnthologyFactory.GenerateAgents(1000000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(20, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000000 agents and 50 locations.
        /// </summary>
        [TestMethod]
        public void TestMillionAgentsFiftyLocations()
        {
            if (!TEST_MILLION) return;
            AnthologyFactory.GenerateAgents(1000000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(50, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000000 agents and 100 locations.
        /// </summary>
        [TestMethod]
        public void TestMillionAgentsHundredLocations()
        {
            if (!TEST_MILLION) return;
            AnthologyFactory.GenerateAgents(1000000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(100, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000000 agents and 500 locations.
        /// </summary>
        [TestMethod]
        public void TestMillionAgentsFiveHundredLocations()
        {
            if (!TEST_MILLION) return;
            AnthologyFactory.GenerateAgents(1000000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(500, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000000 agents and 1000 locations.
        /// </summary>
        [TestMethod]
        public void TestMillionAgentsThousandLocations()
        {
            if (!TEST_MILLION) return;
            AnthologyFactory.GenerateAgents(1000000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(1000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000000 agents and 5000 locations.
        /// </summary>
        [TestMethod]
        public void TestMillionAgentsFiveThousandLocations()
        {
            if (!TEST_MILLION) return;
            AnthologyFactory.GenerateAgents(1000000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(5000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000000 agents and 10000 locations.
        /// </summary>
        [TestMethod]
        public void TestMillionAgentsTenThousandLocations()
        {
            if (!TEST_MILLION) return;
            AnthologyFactory.GenerateAgents(1000000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(10000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000000 agents and 50000 locations.
        /// </summary>
        [TestMethod]
        public void TestMillionAgentsFiftyThousandLocations()
        {
            if (!TEST_MILLION) return;
            AnthologyFactory.GenerateAgents(1000000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(50000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }

        /// <summary>
        /// Tests generating 1000000 agents and 100000 locations.
        /// </summary>
        [TestMethod]
        public void TestMillionAgentsHundredThousandLocations()
        {
            if (!TEST_MILLION) return;
            AnthologyFactory.GenerateAgents(1000000, GRID_SIZE);
            AnthologyFactory.GenerateSimLocations(100000, GRID_SIZE);
            AnthologyFactory.GeneratePrimaryActions(NUM_ACTIONS);

            Stopwatch timer = Stopwatch.StartNew();
            ExecutionManager.RunSim(NUM_ITERATIONS);
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 1, "Time elapsed = " + timer.ElapsedMilliseconds + "ms.");
        }
    }
}
