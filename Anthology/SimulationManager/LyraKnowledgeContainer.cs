namespace Anthology.SimulationManager
{
    /// <summary>
    /// An implementation of the IKnowledgeContainer that is used to conjunction with
    /// LyraKS.cs and the Lyra simulation. 
    /// </summary>
    public class LyraKnowledgeContainer : IKnowledgeContainer
    {
        /// <summary>
        /// The unique ID used to reference a particular knowledge 
        /// container in the NPC.Knowledge Dictionary.
        /// 
        /// This corresponds to the Lyra view IDs.
        /// </summary>
        public int Id { get; set; } = -1;

        /// <summary>
        /// The ID of the agent in the Lyra system that this container refers to.
        /// </summary>
        public int Agent { get; set; } = -1;

        /// <summary>
        /// The ID of the topic that this container refers to.
        /// </summary>
        public int Topic { get; set; } = -1;

        /// <summary>
        /// The ID of the object of discussion that this container refers to, if any.
        /// </summary>
        public int? Ood { get; set; }

        /// <summary>
        /// A dictionray of string:float pairs representing the value system in Lyra,
        /// such as the attitude, opinion, and uncertainty of a view.
        /// </summary>
        public Dictionary<string, float> BeliefValues { get; set; } = new();

        /// <summary>
        /// Updates the belief values dictionary to match those of the given container.
        /// </summary>
        /// <param name="newContainer">The container of values to match to.</param>
        /// <exception cref="ArgumentException">Throws exception if the container IDs do not match.</exception>
        public void Update(IKnowledgeContainer newContainer)
        {
            if (newContainer is LyraKnowledgeContainer newViews)
            {
                if (newViews.Id != Id)
                    throw new ArgumentException("The ID of these containers do not match.");
                Dictionary<string, float> newBeliefs = newViews.BeliefValues;
                foreach (KeyValuePair<string, float> kvp in newBeliefs)
                {
                    BeliefValues[kvp.Key] = kvp.Value;
                }
            }
            else
            {
                throw new ArgumentException("The given container is not of the type LyraKnowledgeContainer.");
            }
        }
    }
}
