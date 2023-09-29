using Amazon.Runtime.Internal;
using MongoDB.Bson.IO;
using System.Text.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Serializers;

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

        /// <summary>
        /// Default URL to call Lyra from.
        /// </summary>
        private const string URL = "http://127.0.0.1:8000/lyra/api/";

        private static string? simId = "";

        private static string simUrl = "";

        static StringContent Serialize<T>(T obj)
        {
            string serialized = JsonSerializer.Serialize(obj);
            return new StringContent(serialized, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Initializes the contents of Lyra given the path of the JSON file to init from.
        /// </summary>
        /// <param name="pathFile">Path of JSON file to init from.</param>
        public override void Init(string pathFile = "")
        {
            client.BaseAddress = new Uri(URL);

            var sim = new
            {
                title = "Anthology",
                version = "1.0.0",
                notes = "This is maintained by the SimManager C# project."
            };
            Task<HttpResponseMessage> postResponse = client.PostAsync("simulation/", Serialize(sim));
            postResponse.Wait();
            postResponse.Result.RequestMessage?.Options.TryGetValue<string>(new HttpRequestOptionsKey<string>("id"), out simId);
            if (simId == null) throw new BadHttpRequestException("Unable to post a new LYRA simulation");
            simUrl = URL + simId;
            client.GetAsync(simUrl).Wait();
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

        public struct ViewBody
        {
            [JsonPropertyName("ood")]
            private int ood;
            [JsonPropertyName("topic")]
            private int topic;
            [JsonPropertyName("attitude")]
            private float attitude;
            [JsonPropertyName("opinion")]
            private float opinion;
            [JsonPropertyName("uncertainty")]
            private float uncertainty;
        }

        public struct AgentBody
        {
            [JsonPropertyName("name")]
            private string name;
            [JsonPropertyName("views")]
            private List<ViewBody> views;
        }

        public struct PostBody
        {
            [JsonPropertyName("act_type")]
            private string actType;
            [JsonPropertyName("agents")]
            private List<AgentBody> agents;

            public PostBody(string actType, List<AgentBody> agents)
            {
                this.actType = actType;
                this.agents = agents;
            }
        }
    }
}
