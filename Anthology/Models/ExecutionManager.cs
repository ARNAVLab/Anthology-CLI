using SimSharp;
using System.Text.Json;

namespace Anthology.Models
{
    public static class ExecutionManager
    {
        /** Initializes the simulation, delegates to World.Init() */
        public static void Init(string pathToFiles)
        {
            World.ReadWrite.InitWorldFromPaths(pathToFiles);
        }

        private static IEnumerable<Event> Source()
        {
            while(true)
                foreach(Agent a in AgentManager.Agents)
                {
                    var agent = a.Turn();
                    yield return World.Env.Process(agent);
                }
        }

        /**
         * Executes a turn for each agent every tick.
         * Executes a single turn and then must be called again
         */
        public static void RunSim(int steps = 1)
        {
            if(ToContinue())
            {
                World.Env.Process(Source());
                World.Env.Run(TimeSpan.FromMinutes(steps));
                World.IncrementTime();
            }

            UI.Update();
        }

        /**
         * Tests whether the simulation should continue.
         * First checks whether the stopping function for the simulation has been met.
         * Next checks if the user has paused the simulation
         */
        public static bool ToContinue()
        {
            if (AgentManager.AllAgentsContent())
            {
                return false;
            }
            else if (!UI.Paused)
            {
                return false;
            }
            return true;
        }

        /**
         * Interrupts the agent from the current action they are performing.
         * Potential future implementation: Optionally add the interrupted action (with the remaining occupied counter) to the end of the action queue.
         */
        public static void Interrupt(Agent agent)
        {
            agent.OccupiedCounter = 0;
            agent.XDestination = -1;
            agent.YDestination = -1;
            Action interrupted = agent.CurrentAction.First();
            agent.CurrentAction.RemoveFirst();
            Console.WriteLine("Agent: " + agent.Name + " was interrupted from action: " + interrupted.Name);
        }

        public static void Interrupt(string agentName)
        {
            Agent? agent = AgentManager.GetAgentByName(agentName);
            if (agent != null)
            {
                Interrupt(agent);
            }
        }
    }
}
