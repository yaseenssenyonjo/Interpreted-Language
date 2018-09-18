using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;

namespace Interpreted_Language.Language.Parser.Syntax.Nodes
{
    internal class SayNode : INode
    {
        /// <summary>
        /// The name of the speaker.
        /// </summary>
        private readonly string _speakerName;
        /// <summary>
        /// The dialogue for the speaker.
        /// </summary>
        private readonly string _dialogue;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="speakerName">The name of the speaker.</param>
        /// <param name="dialogue">The dialogue for the speaker.</param>
        public SayNode(string speakerName, string dialogue)
        {
            _speakerName = speakerName;
            _dialogue = dialogue;
        }
        
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}