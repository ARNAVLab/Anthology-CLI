using System.Numerics;
using System.Text;

namespace Anthology.SimulationManager
{
  /**
   * An Agent/Actor/Individual/Unit/NPC to be maintained by the simulation manager
   * Contains data necessary for coordinating behavior across simulations and for
   * displaying information on the frontend
   */
  public class NPC
  {
        /** The name of the NPC */
        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { Dirty = true; name = value; }
        }

        /** The current location of the NPC */
        private string location = string.Empty;
        public string Location
        {
            get { return location; }
            set { Dirty = true; location = value; }
        }

        /** Data representing the knowledge/beliefs/opinions of the NPC */
        private Dictionary<string, float> knowledge = new();
        public Dictionary<string, float> Knowledge
        {
            get { return knowledge; }
            set { Dirty = true; knowledge = value; }
        }

        /** Add a subject to the NPC's knowledge at the given starting amount */
        public void SetKnowledgeSubject(string subject, float amount)
        {
            knowledge[subject] = amount;
            Dirty = true;
        }

        /** Change an existing knowledge subject by the given amount */
        public void ChangeKnowledgeSubject(string subject, float delta)
        {
            knowledge[subject] += delta;
            Dirty = true;
        }

        /** Data representing the motivations/statuses of the NPC */
        private Dictionary<string, float> motives = new();
        public Dictionary<string, float> Motives
        {
            get { return motives; }
            set { Dirty = true; motives = value; }
        }

        /** Add or set the motivation to the given starting amount */
        public void SetMotivationStatus(string motivation, float amount)
        {
            motives[motivation] = amount;
            Dirty = true;
        }

        /** Change an existing motive by the given amount */
        public void ChangeMotivation(string motivation, float delta)
        {
            motives[motivation] += delta;
            Dirty = true;
        }

        /** The action currently being performed by the NPC */
        private Action currentAction = new();
        public Action CurrentAction
        {
            get { return currentAction; }
            set { Dirty = true; currentAction = value; }
        }

        /** The remaining ticks until the completion of the current action */
        private int actionCounter = 0;
        public int ActionCounter
        {
            get { return actionCounter; }
            set { Dirty = true; actionCounter = value; }
        }

        /** The destination of the NPC if the NPC is travelling */
        private string destination = string.Empty;
        public string Destination
        {
            get { return destination; }
            set { Dirty = true; destination = value; }
        }

        /** Whether or not this NPC has been modified and needs to have its update pushed */
        public bool Dirty { get; set; } = false;

        /**
         * Gets a string representation of the NPC, in the following format:
         * 
         * "Name: {name}
         *  X: {x}, Y: {y}
         *  Current Action: {name of action}"
         */
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendFormat("Name: {0}, ", Name);
            sb.AppendFormat("Current Location: ", Location);
            sb.AppendFormat("Motives: ", Motives.ToString());
            sb.AppendFormat("Current Action: {0}", CurrentAction.Name);
            sb.AppendFormat("Current Destination: {0}", Destination);
            return sb.ToString();
        }
    }
}
