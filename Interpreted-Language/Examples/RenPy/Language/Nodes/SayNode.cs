using System;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax.Nodes;
using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;
using Interpreted_Language.RenPy.Language.Interpreter;

namespace Interpreted_Language.RenPy.Language.Nodes
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
        public int LineNumber { private get; set; }
        
        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.RenPy.Language.Nodes.SayNode"/> class.
        /// </summary>
        /// <param name="speakerName">The name of the speaker.</param>
        /// <param name="dialogue">The dialogue for the speaker.</param>
        public SayNode(string speakerName, string dialogue)
        {
            _speakerName = speakerName;
            _dialogue = dialogue;
        }
        
        public BlockingType Execute(IExecutionContext context)
        {
            var character = ((RenPyExecutionContext)context).GetCharacter(_speakerName, LineNumber);
            Console.WriteLine($"{character.Id} says {_dialogue}");
            return BlockingType.Blocking;
        }
    }
}