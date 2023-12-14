using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Anthology.SimulationManager
{
    /// <summary>
    /// Concrete example implementation of KnowledgeSim using the Lyra API.
    /// Currently unimplemented until the Lyra API is complete.
    /// </summary>
    public class LyraKS : KnowledgeSim
    {
        /// <summary>
        /// HTTP client to be used for calling Lyra API.
        /// </summary>
        private static readonly HttpClient client = new();

        private static readonly JsonSerializerOptions jso = new()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        /// <summary>
        /// Default URL to call Lyra from.
        /// </summary>
        private const string URI = "http://127.0.0.1:8000/lyra/api/";

        /// <summary>
        /// Path that includes the sim ID once it is determined.
        /// </summary>
        private static string simUrl = "";

        /// <summary>
        /// Path that includes the run ID once it is determined.
        /// </summary>
        private static string runUrl = "";

        // Helper for serializing generic objects into StringContent
        private static StringContent Serialize<T>(T obj)
        {
            string serialized = JsonSerializer.Serialize(obj);
            return new StringContent(serialized, Encoding.UTF8, "application/json");
        }

        // Helper for deserializing generic objects from a JSON as string
        private static T Deserialize<T>(string json)
        {
            T? deserialized = JsonSerializer.Deserialize<T>(json, jso)
                ?? throw new JsonException("Could not deserialize from JSON:\n" + json);
            return deserialized;
        }

        /// <summary>
        /// Initializes the contents of Lyra given the path of the JSON file to init from.
        /// </summary>
        /// <param name="pathFile">Path of JSON file to init from.</param>
        /// <exception cref="TimeoutException">Timeout waiting for Lyra setup response.</exception>
        public override void Init(string pathFile = "")
        {
            client.BaseAddress = new Uri(URI);
            client.DefaultRequestHeaders.ExpectContinue = false;

            // Read startup file
            string fileText = File.ReadAllText(pathFile);
            StartupJson simSetup = Deserialize<StartupJson>(fileText);
            Dictionary<string, string> simPostBody = new()
            {
                ["title"] = simSetup.SimSetup["simulation_name"],
                ["version"] = simSetup.SimSetup["version"],
                ["notes"] = simSetup.SimSetup["notes"]
            };

            // Post for a new simulation
            Task<HttpResponseMessage> postTask = client.PostAsync("simulation/", Serialize(simPostBody));
            if (!postTask.Wait(3000))
                throw new TimeoutException("Timed out waiting for Lyra setup response.");

            HttpResponseMessage postResponse = postTask.Result;
            postResponse.EnsureSuccessStatusCode();

            // Read the response to get the simulation's ID and update the URL
            Task<string> responseContent = postResponse.Content.ReadAsStringAsync();
            responseContent.Wait();
            PostSimResponse responseParsed = Deserialize<PostSimResponse>(responseContent.Result);
            simUrl = "simulation/" + responseParsed.Id + "/";

            // Start the sim so we can begin using it
            Task<HttpResponseMessage> getResponse = client.GetAsync(simUrl + "start/");
            getResponse.Wait();
            getResponse.Result.EnsureSuccessStatusCode();

            // Delegate to do initial posts for topics, oods, a new run, and agents
            SetupSimState(simSetup);
        }

        /// <summary>
        /// Used to populate the SimManager's collection of NPCs from the knowledge sim.
        /// </summary>
        /// <param name="npcs">Dictionary of SimManager's NPCs to populate.</param>
        /// <exception cref="NotImplementedException">Currently not implemented.</exception>
        public override void LoadNpcs(Dictionary<string, NPC> npcs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the knowledge sim's version of the given NPC to match the SimManager's.
        /// </summary>
        /// <param name="npc">Knowledge sim's NPC to update.</param>
        /// <exception cref="NotImplementedException">Currently not implemented.</exception>
        public override void PushUpdatedNpc(NPC npc)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Advances the knowledge sim by given amount of steps.
        /// </summary>
        /// <param name="steps">Number of steps to advance the knowledge sim.</param>
        /// <exception cref="NotImplementedException">Currently not implemented.</exception>
        public override void Run(int steps = 1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the given NPC to match the knowledge sim's version.
        /// </summary>
        /// <param name="npc">SimManager's NPC to update.</param>
        /// <exception cref="NotImplementedException">Currently not implemented.</exception>
        public override void UpdateNpc(NPC npc)
        {
            throw new NotImplementedException();
        }

        // Posts topics, oods, a run, and agents from the 
        private static void SetupSimState(StartupJson simSetup)
        {
            // post topics
            client.PostAsync(simUrl + "action/", SetupTopics(simSetup.Topics)).Wait();
            // post oods
            client.PostAsync(simUrl + "action/", SetupOods(simSetup.Oods)).Wait();
            
            // post new run and get its id
            Task<HttpResponseMessage> runTask = 
                client.PostAsync(simUrl + "action/", SetupRun(simSetup.Notes));
            runTask.Wait();
            Task<string> runContent = runTask.Result.Content.ReadAsStringAsync();
            runContent.Wait();
            PostRunResponse runResponse = Deserialize<PostRunResponse>(runContent.Result);
            runUrl = "/run" + runResponse.Output.Run.Id + "/";

            // post npcs to new run
            client.PostAsync(runUrl + "npcs/", SetupAgents(simSetup.Agents));
        }

        // Packs a list of topic titles into HttpContent for posting to Lyra
        private static HttpContent SetupTopics(List<string> topics)
        {
            PostTopic topicsBody = new();
            List<PostTopic.Topic> topicData = new();
            foreach (string t in topics)
                topicData.Add(new PostTopic.Topic { Title = t });
            topicsBody.Data["topics"] = topicData;
            return Serialize(topicsBody);
        }

        // Packs a list of startup oods into HttpContent for posting to Lyra
        private static HttpContent SetupOods(List<Ood> oods)
        {
            PostOod oodsBody = new();
            List<Ood> oodData = new();
            foreach (Ood o in oods)
                oodData.Add(o);
            oodsBody.Data["oods"] = oodData;
            return Serialize(oodsBody);
        }

        // Packs startup run notes into HttpContent for posting to Lyra
        private static HttpContent SetupRun(string notes)
        {
            PostRun runBody = new();
            runBody.Data["notes"] = notes;
            return Serialize(runBody);
        }

        private static HttpContent SetupAgents(List<Agent> agents)
        {
            PostAgent agentBody = new();
            List<Agent> agentData = agentBody.Data["agents"];
            foreach (Agent a in agents)
                agentData.Add(a);
            agentBody.Data["agents"] = agentData;
            return Serialize(agentBody);
        }

        // Used for convenient (de)serialization of actions from the setup JSON
        private class DiscussionAction
        {
            [JsonPropertyName("action_name")]
            [JsonInclude]
            public string ActionName = "";
            [JsonPropertyName("discussion_probability")]
            [JsonInclude]
            public float DiscussionProbability;
        }

        // Used for convenient (de)serialization of oods from the setup JSON
        // and for posting oods to the Lyra simulation
        private class Ood
        {
            [JsonPropertyName("topic")]
            [JsonInclude]
            public string Topic = "";
            [JsonPropertyName("title")]
            [JsonInclude]
            public string Title = "";
        }

        // Used for convenient (de)serialization of views from the setup JSON
        // and for posting agent views to the Lyra simulation
        private class View
        {
            [JsonPropertyName("ood")]
            [JsonInclude]
            public Ood Ood = new();
            [JsonPropertyName("attitude")]
            [JsonInclude]
            public float Attitude;
            [JsonPropertyName("opinion")]
            [JsonInclude]
            public float Opinion;
            [JsonPropertyName("uncertainty")]
            [JsonInclude]
            public float Uncertainty;
            [JsonPropertyName("public_compliance_thresh")]
            [JsonInclude]
            public float PublicComplianceThresh;
            [JsonPropertyName("private_acceptance_thresh")]
            [JsonInclude]
            public float PrivateAcceptanceThresh;
        }

        // Used for convenient (de)serialization of agents from the setup JSON
        private class Agent
        {
            [JsonPropertyName("name")]
            [JsonInclude]
            public string Name = "";
            [JsonPropertyName("views")]
            [JsonInclude]
            public List<View> Views = new();
            
        }

        // Used for convenient deserialization of agents from Lyra response bodies
        private class SimRun
        {
            [JsonPropertyName("id")]
            [JsonInclude]
            public short Id;
            [JsonPropertyName("number")]
            [JsonInclude]
            public short Number;
            [JsonPropertyName("notes")]
            [JsonInclude]
            public string Notes;
            [JsonPropertyName("simulation")]
            [JsonInclude]
            public short SimulationId;
        }
        
        // Used for convenient deserialization of the setup JSON
        private class StartupJson
        {
            [JsonPropertyName("sim_setup")]
            [JsonInclude]
            public Dictionary<string, string> SimSetup = new();
            [JsonPropertyName("notes")]
            [JsonInclude]
            public string Notes = "";
            [JsonPropertyName("discussion_actions")]
            [JsonInclude]
            public List<DiscussionAction> DiscussionActions = new();
            [JsonPropertyName("topics")]
            [JsonInclude]
            public List<string> Topics = new();
            [JsonPropertyName("objects_of_discussion")]
            [JsonInclude]
            public List<Ood> Oods = new();
            [JsonPropertyName("agents")]
            [JsonInclude]
            public List<Agent> Agents = new();
        }

        // Used for convenient serialization of topics for posting to Lyra
        private class PostTopic
        {
            public class Topic
            {
                [JsonPropertyName("title")]
                [JsonInclude]
                public string Title = "";
            }

            [JsonPropertyName("act_type")]
            [JsonInclude]
            public string ActType = "topics";
            [JsonPropertyName("data")]
            [JsonInclude]
            public Dictionary<string, List<Topic>> Data = new();
        }

        // Used for convenient serialization of oods for posting to Lyra
        private class PostOod
        {
            [JsonPropertyName("act_type")]
            [JsonInclude]
            public string ActType = "oods";
            [JsonPropertyName("data")]
            [JsonInclude]
            public Dictionary<string, List<Ood>> Data= new();
        }

        // Used for convenient serialization of runs for posting to Lyra
        private class PostRun
        {
            [JsonPropertyName("act_type")]
            [JsonInclude]
            public string ActType = "run";
            [JsonPropertyName("data")]
            [JsonInclude]
            public Dictionary<string, string> Data = new();
        }

        // Used for convenient serialization of agents for posting to Lyra
        private class PostAgent
        {
            [JsonPropertyName("act_type")]
            [JsonInclude]
            public string ActType = "npcs";
            [JsonPropertyName("data")]
            [JsonInclude]
            public Dictionary<string, List<Agent>> Data = new();
        }

        // Used for convenient deserialization of the Lyra post simulation response
        // This could be replaced with a simple dictionary if ID is changed to return
        // as a string instead.
        private class PostSimResponse
        {
            [JsonPropertyName("id")]
            [JsonInclude]
            public short Id;
            [JsonPropertyName("title")]
            [JsonInclude]
            public string Title = "";
            [JsonPropertyName("version")]
            [JsonInclude]
            public string Version = "";
            [JsonPropertyName("notes")]
            [JsonInclude]
            public string Notes = "";
        }

        // Used for convenient deserialization of the Lyra post run response
        private class PostRunResponse
        {
            public class DataBody
            {
                [JsonPropertyName("notes")]
                [JsonInclude]
                public string Notes = "";
                [JsonPropertyName("simulation")]
                [JsonInclude]
                public short SimulationId;
                [JsonPropertyName("number")]
                [JsonInclude]
                public short RunNumber;
            }

            public class OutputBody
            {
                [JsonPropertyName("message")]
                [JsonInclude]
                public string Message = "";
                [JsonPropertyName("run")]
                [JsonInclude]
                public SimRun Run = new();
                [JsonPropertyName("status")]
                [JsonInclude]
                public short Status;
            }

            [JsonPropertyName("id")]
            [JsonInclude]
            public short Id;
            [JsonPropertyName("act_type")]
            [JsonInclude]
            public string ActType = "run";
            [JsonPropertyName("data")]
            [JsonInclude]
            public DataBody Data = new();
            [JsonPropertyName("output")]
            [JsonInclude]
            public OutputBody Output = new();
            [JsonPropertyName("simulation")]
            [JsonInclude]
            public short SimulationId;
        }
    }
}
