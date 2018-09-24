using System;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;

namespace Interpreted_Language.RenPy.Nodes
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
        
        /// <inheritdoc />
        public int NodeType { get; set; } = (int)Nodes.NodeType.Say;
        /// <inheritdoc />
        public int LineNumber { private get; set; }
        
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
        
        public void Execute(IExecutionContext context)
        {
            Console.WriteLine($"{_speakerName} says {_dialogue}");
        }
    }
}