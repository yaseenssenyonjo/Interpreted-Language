using System;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;

namespace Interpreted_Language.RenPy.Nodes
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
        
        /// <summary>
        /// The line number for this node.
        /// </summary>
        public int LineNumber { private get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="characterName">The name of the character.</param>
        /// <param name="spriteId">The id of the sprite.</param>
        public SpriteNode(string characterName, int spriteId)
        {
            _characterName = characterName;
            _spriteId = spriteId;
        }

        public void Execute(IExecutionContext context)
        {
            Console.WriteLine($"{_characterName} switch to sprite {_spriteId}");
        }
    }
}