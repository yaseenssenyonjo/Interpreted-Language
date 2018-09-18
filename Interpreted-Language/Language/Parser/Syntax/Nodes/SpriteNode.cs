using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;

namespace Interpreted_Language.Language.Parser.Syntax.Nodes
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
        /// 
        /// </summary>
        /// <param name="characterName">The name of the character.</param>
        /// <param name="spriteId">The id of the sprite.</param>
        public SpriteNode(string characterName, int spriteId)
        {
            _characterName = characterName;
            _spriteId = spriteId;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}