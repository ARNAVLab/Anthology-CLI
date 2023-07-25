using System.Security.Cryptography;

namespace Anthology.Models
{
    /// <summary>
    /// Factory class that generates agents, locations, and actions.
    /// </summary>
    public static class AnthologyFactory
    {
        /// <summary>
        /// Generates all agents given amount of agents and grid size. 
        /// </summary>
        /// <param name="n">Number of agents to add.</param>
        /// <param name="gridSize">Size of the square grid (in terms of rows/columns).</param>
        public static void GenerateAgents(uint n, int gridSize)
        {
            UI.GridSize = gridSize;
            AgentManager.Agents.Clear();

            Random r = new();
            for (uint i = 0; i < n; i++)
            {
                Agent a = new()
                {
                    Name = "a_" + i,
                    Motives =
                    {
                        { "m1", r.Next(4) + 1 },
                        { "m2", r.Next(4) + 1 },
                        { "m3", r.Next(4) + 1 },
                        { "m4", r.Next(4) + 1 },
                        { "m5", r.Next(4) + 1 }
                    },
                    XLocation = r.Next(gridSize),
                    YLocation = r.Next(gridSize),
                };
                AgentManager.Agents.Add(a);
            }
        }

        /// <summary>
        /// Generates locations in the grid given number of locations.
        /// </summary>
        /// <param name="n">Number of locations to generate.</param>
        /// <param name="gridSize">Size of the square grid (in terms of rows/columns)</param>
        public static void GenerateSimLocations(uint n, int gridSize)
        {
            UI.GridSize = gridSize;
            LocationManager.LocationSet.Clear();
            LocationManager.LocationGrid.Clear();
            for (int i = 0; i < gridSize; i++)
            {
                LocationManager.LocationGrid[i] = new Dictionary<int, SimLocation>();
                for (int k = 0; k < gridSize; k++)
                {
                    LocationManager.LocationGrid[i][k] = new SimLocation();
                }
            }

            Random r = new();

            for (uint i = 0; i < n; i++)
            {
                int x = r.Next(gridSize);
                int y = r.Next(gridSize);
                while (LocationManager.LocationGrid[x][y].Name != string.Empty)
                {
                    x = r.Next(gridSize);
                    y = r.Next(gridSize);
                }
                SimLocation sl = new()
                {
                    Name = "l_" + i,
                    X = x,
                    Y = y,
                    Tags =
                    {
                        "t_" + (i % 3),
                        "t_" + ((i % 7) + 3)
                    }
                };
                LocationManager.AddLocation(sl);
            }
        }

        /// <summary>
        /// Generates primary actions given number of primary actions.
        /// </summary>
        /// <param name="n">Number of primary actions to generate.</param>
        public static void GeneratePrimaryActions(uint n)
        {
            ActionManager.Actions.PrimaryActions.Clear();
            ActionManager.Actions.PrimaryActions.Add(new PrimaryAction() { Name = "travel_action" });
            ActionManager.Actions.PrimaryActions.Add(new PrimaryAction() { Name = "wait_action" });

            Random r = new();
            for (uint i = 0; i < n; i++)
            {
                int rltype = r.Next(2);
                RLocation rl = new();
                switch (rltype)
                {
                    case 0:
                        rl.HasAllOf.Add("t_" + r.Next(9));
                        break;
                    case 1:
                        rl.HasNoneOf.Add("t_" + r.Next(9));
                        break;
                    case 2:
                        rl.HasOneOrMoreOf.Add("t_" + r.Next(0));
                        break;
                }

                PrimaryAction a = new()
                {
                    Name = "action_" + i,
                    Effects =
                    {
                        { "m1", r.Next(2) },
                        { "m2", r.Next(2) },
                        { "m3", r.Next(2) },
                        { "m4", r.Next(2) },
                        { "m5", r.Next(2) }
                    },
                    MinTime = r.Next(285) + 15,
                    Requirements =
                    {
                        Locations = new() { rl }
                    }
                };
                ActionManager.Actions.AddAction(a);
            }
        }
    }
}
