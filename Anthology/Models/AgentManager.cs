namespace Anthology.Models
{
    /// <summary>
    /// Manages agents and their motives.
    /// </summary>
    public static class AgentManager
    {
        /// <summary>
        /// Agents in the simulation.
        /// </summary>
        public static HashSet<Agent> Agents { get; set; } = new HashSet<Agent>();

        /// <summary>
        /// Initializes/resets all agent manager variables.
        /// </summary>
        /// <param name="path">Path of JSON file to load from.</param>
        public static void Init(string path)
        {
            Agents.Clear();
            World.ReadWrite.LoadAgentsFromFile(path);
        }

        /// <summary>
        /// Gets the agent in the simulation with the matching name.
        /// </summary>
        /// <param name="name">Name of the agent to retrieve.</param>
        /// <returns>Agent with given name.</returns>
        public static Agent GetAgentByName(string name)
        {
            bool MatchName(Agent a)
            {
                return a.Name == name;
            }

            Agent agent = Agents.First(MatchName);
            return agent;
        }

        /// <summary>
        /// Checks whether the agent satisfies the motive requirement for an action.
        /// </summary>
        /// <param name="agent">The agent to check.</param>
        /// <param name="reqs">The requirements to check.</param>
        /// <returns>True if agent satisfies all requirements for an action.</returns>
        public static bool AgentSatisfiesMotiveRequirement(Agent agent, HashSet<Requirement> reqs)
        {
            foreach (Requirement r in reqs)
            {
                if (r is RMotive rMotive)
                {
                    string t = rMotive.MotiveType;
                    float c = rMotive.Threshold;
                    switch (rMotive.Operation)
                    {
                        case BinOps.EQUALS:
                            if (!(agent.Motives[t] == c))
                            {
                                return false;
                            }
                            break;
                        case BinOps.LESS:
                            if (!(agent.Motives[t] < c))
                            {
                                return false;
                            }
                            break;
                        case BinOps.GREATER:
                            if (!(agent.Motives[t] > c))
                            {
                                return false;
                            }
                            break;
                        case BinOps.LESS_EQUALS:
                            if (!(agent.Motives[t] <= c))
                            {
                                return false;
                            }
                            break;
                        case BinOps.GREATER_EQUALS:
                            if (!(agent.Motives[t] >= c))
                            {
                                return false;
                            }
                            break;
                        default:
                            Console.WriteLine("ERROR - JSON BinOp specification mistake for Motive Requirement for action");
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Stopping condition for the simulation.
        /// Stops the sim when all agents are content.
        /// </summary>
        /// <returns>True if all agents are content.</returns>
        public static bool AllAgentsContent()
        {
            foreach (Agent a in Agents)
            {
                if (!a.IsContent()) return false;
            }
            return true;
        }

        /// <summary>
        /// Decrements the motives of every agent in the simulation.
        /// </summary>
        public static void DecrementMotives()
        {
            foreach (Agent a in Agents)
            {
                a.DecrementMotives();
            }
        }
    }
}
