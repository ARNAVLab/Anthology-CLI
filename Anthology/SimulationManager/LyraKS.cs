using Amazon.Runtime.Internal;
using MongoDB.Bson.IO;
using System.Text.Json;
using System.Text;
using System.Text.Json.Nodes;

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
                notes = "AAAAAAAAA"
            };
            client.PostAsync("simulation/", Serialize(sim)).Wait();
            client.GetAsync("simulation/1/start/").Wait();
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
    }
}
