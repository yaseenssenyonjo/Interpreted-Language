using System;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax.Nodes;
using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;
using Interpreted_Language.RenPy.Language.Interpreter;

namespace Interpreted_Language.RenPy.Language.Nodes
{
    internal class SpriteNode : INode
    {
        /// <summary>
        /// The name of the character whose sprite to change.
        /// </summary>
        private readonly string _characterName;
        /// <summary>
        /// The id of the sprite to switch to.
        /// </summary>
        private readonly int _spriteId;

        /// <inheritdoc />
        public int LineNumber { private get; set; }
        
        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.RenPy.Language.Nodes.SpriteNode"/> class.
        /// </summary>
        /// <param name="characterName">The name of the character.</param>
        /// <param name="spriteId">The id of the sprite.</param>
        public SpriteNode(string characterName, int spriteId)
        {
            _characterName = characterName;
            _spriteId = spriteId;
        }

        public BlockingType Execute(IExecutionContext context)
        {
            var character = ((RenPyExecutionContext)context).GetCharacter(_characterName, LineNumber);
            Console.WriteLine($"{character.Id} switches to sprite {_spriteId}");
            return BlockingType.NonBlocking;
        }

    }
}