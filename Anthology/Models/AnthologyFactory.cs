using Anthology.Models.Deprecated;

namespace Anthology.Models
{
    public static class AnthologyFactory
    {
        public static void GenerateAgents(int numAgents, int numLocations)
        {
            AgentManager.Reset();
            Random r = new();
            for (uint i = 0; i < numAgents; i++)
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
                    CurrentLocation = "l_" + r.Next(numLocations)
                };
                AgentManager.AddAgent(a);
            }
        }

        public static void GenerateLocations(int n)
        {
            if (n < 5)
                throw new ArgumentException("Please only use this factory for systems with at least 5 locations");
            LocationManager.Reset();
            Random r = new();
            int[] c = new int[3];

            for (int i = 0; i < n; i++)
            {
                c[0] = i > 0 ? i - 1 : n - 1;
                c[1] = i < n - 1 ? i + 1 : 0;
                c[2] = r.Next(n);
                if (c[2] == i) c[2] += 1;
                if (c[2] == c[0])
                {
                    if (c[2] == 0) c[2] = n - 1;
                    else c[2] -= 1;
                }
                else if (c[2] == c[1])
                {
                    if (c[2] == n - 1) c[2] = 0;
                    else c[2] += 1;
                }

                LocationNode node = new()
                {
                    Name = "l_" + i,
                    X = i,
                    Y = i,
                    Tags =
                    {
                        "t_" + (i % 3),
                        "t_" + ((i % 7) + 3)
                    },
                    Connections =
                    {
                        { "l_" + c[0], r.Next(100) },
                        { "l_" + c[1], r.Next(100) },
                        { "l_" + c[2], r.Next(100) }
                    }
                };
                LocationManager.AddLocation(node);
            }
            LocationManager.UpdateDistanceMat();
        }

        public static void GeneratePrimaryActions(uint n)
        {
            ActionManager.Reset();
            ActionManager.AddAction(new PrimaryAction() { Name = "wait_action" });
            ActionManager.AddAction(new PrimaryAction() { Name = "travel_action" });

            Random r = new();
            for (uint i = 0; i < n; i++)
            {
                int rltype = r.Next(2);
                RLocation rl = new();
                switch (rltype)
                {
                    case 0:
                        rl.HasAllOf.Add("t_" + r.Next(8));
                        break;
                    case 1:
                        rl.HasNoneOf.Add("t_" + r.Next(8));
                        break;
                    case 2:
                        rl.HasOneOrMoreOf.Add("t_" + r.Next(8));
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
