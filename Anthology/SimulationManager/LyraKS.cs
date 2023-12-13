using Amazon.Runtime.Internal;
using MongoDB.Bson.IO;
using System.Text.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Net;

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
        private static readonly HttpClient client = new HttpClient();

        public static readonly JsonSerializerOptions jso = new()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        /// <summary>
        /// Default URL to call Lyra from.
        /// </summary>
        private const string URL = "http://127.0.0.1:8000/lyra/api/";

        private static short simId = -1;

        public static string simUrl = "";

        static StringContent Serialize<T>(T obj)
        {
            string serialized = JsonSerializer.Serialize(obj);
            return new StringContent(serialized, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Initializes the contents of Lyra given the path of the JSON file to init from.
        /// </summary>
        /// <param name="pathFile">Path of JSON file to init from.</param>
        /// <exception cref="TimeoutException">Timeout waiting for Lyra setup response.</exception>
        public override void Init(string pathFile = "")
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.ExpectContinue = false;

            string fileText = File.ReadAllText(pathFile);
            StartupJson? simSetup = JsonSerializer.Deserialize<StartupJson>(fileText, jso) 
                ?? throw new JsonException("Setup file could not be read correctly.");
            Dictionary<string, string> simPostBody = new()
            {
                ["title"] = simSetup.SimSetup["simulation_name"],
                ["version"] = simSetup.SimSetup["version"],
                ["notes"] = simSetup.SimSetup["notes"]
            };

            HttpContent postBody = new StringContent(JsonSerializer.Serialize(simPostBody));

            Task<HttpResponseMessage> postTask = client.PostAsync("simulation/", postBody);
            if (!postTask.Wait(3000))
                throw new TimeoutException("Timed out waiting for Lyra setup response.");

            HttpResponseMessage postResponse = postTask.Result;
            postResponse.EnsureSuccessStatusCode();

            Task<string> responseContent = postResponse.Content.ReadAsStringAsync();
            responseContent.Wait();
            PostNewSimResponseBody responseParsed = JsonSerializer.Deserialize<PostNewSimResponseBody>(responseContent.Result, jso)
                ?? throw new JsonException("Unable to read post response content as JSON:\n" + responseContent.Result);

            simId = responseParsed.Id;
            simUrl = "simulation/" + simId;
            Task<HttpResponseMessage> getResponse = client.GetAsync(simUrl);
            getResponse.Wait();
            getResponse.Result.EnsureSuccessStatusCode();
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

        public class PostNewSimResponseBody
        {
            [JsonPropertyName("id")]
            [JsonInclude]
            public short Id;
            [JsonPropertyName("title")]
            [JsonInclude]
            public string Title;
            [JsonPropertyName("version")]
            [JsonInclude]
            public string Version;
            [JsonPropertyName("notes")]
            [JsonInclude]
            public string Notes;
        }

        public class StartupDiscussionAction
        {
            [JsonPropertyName("action_name")]
            [JsonInclude]
            public string ActionName;
            [JsonPropertyName("discussion_probability")]
            [JsonInclude]
            public float DiscussionProbability;
        }

        public class StartupOod
        {
            [JsonPropertyName("topic")]
            [JsonInclude]
            public string Topic;
            [JsonPropertyName("title")]
            [JsonInclude]
            public string Title;
        }

        public class StartupView
        {
            [JsonPropertyName("agent")]
            [JsonInclude]
            public string Agent;
            [JsonPropertyName("ood")]
            [JsonInclude]
            public Dictionary<string, string> Ood;
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
        
        public class StartupJson
        {
            [JsonPropertyName("sim_setup")]
            [JsonInclude]
            public Dictionary<string, string> SimSetup;
            [JsonPropertyName("notes")]
            [JsonInclude]
            public string Notes;
            [JsonPropertyName("discussion_actions")]
            [JsonInclude]
            public List<StartupDiscussionAction> DiscussionActions;
            [JsonPropertyName("topics")]
            [JsonInclude]
            public List<string> Topics;
            [JsonPropertyName("objects_of_discussion")]
            [JsonInclude]
            public List<StartupOod> Oods;
            [JsonPropertyName("views")]
            [JsonInclude]
            public List<StartupView> Views;
        }
    }
}
