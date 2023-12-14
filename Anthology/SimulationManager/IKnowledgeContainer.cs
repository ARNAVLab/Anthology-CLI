namespace Anthology.SimulationManager
{
    /// <summary>
    /// A brief interface that should be implemented in concert with any custom
    /// implementations of the KnowledgeSim abstract class in order to maintain
    /// simulation-specific data types.
    /// 
    /// See LyraKnowledgeContainer.cs for an example implementation.
    /// </summary>
    public interface IKnowledgeContainer
    {
        /// <summary>
        /// The unique ID used to reference a particular knowledge 
        /// container in the NPC.Knowledge Dictionary.
        /// </summary>
        public int Id{ get; set; }

        /*/// <summary>
        /// The knowledge/motivation/belief values about this topic.
        /// </summary>
        public Dictionary<string, float> Values { get; set; }*/

        /// <summary>
        /// Modifies the fields of the knowledge container to match those of the new container.
        /// </summary>
        /// <param name="newContainer">The knowledge container with fields to copy into this.</param>
        public void Update(IKnowledgeContainer newContainer);
    }
}
