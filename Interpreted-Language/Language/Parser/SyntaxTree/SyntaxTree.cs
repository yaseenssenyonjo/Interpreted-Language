using System.Collections.Generic;
using InterpretedLanguage.Language.Parser.SyntaxTree.Traits;

namespace InterpretedLanguage.Language.Parser.SyntaxTree
{
    /// <summary>
    /// Represents a syntax tree.
    /// </summary>
    public class SyntaxTree
    {
        /// <summary>
        /// The nodes.
        /// </summary>
        private readonly List<INode> _nodes = new List<INode>();
        /// <summary>
        /// The current index.
        /// </summary>
        private int _index;
        
        /// <summary>
        /// The environment for this syntax tree.
        /// </summary>
        public SyntaxTreeEnvironment Environment { get; }
        
        /// <summary>
        /// Initialises a new instance of the <see cref="SyntaxTree"/> class.
        /// </summary>
        public SyntaxTree()
        {
            Environment = null;
        }
        
        /// <summary>
        /// Initialises a new instance of the <see cref="SyntaxTree"/> class with the specified environment.
        /// </summary>
        /// <param name="environment"></param>
        public SyntaxTree(SyntaxTreeEnvironment environment)
        {
            Environment = environment;
        }
        
        /// <summary>
        /// Adds the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        public void Add(INode node)
        {
            _nodes.Add(node);
        }
        
        /// <summary>
        /// Advances until the next halting node. 
        /// </summary>
        /// <returns>The state of the syntax tree.</returns>
        public SyntaxTreeState Advance()
        {
            while (_index < _nodes.Count)
            {
                if (_nodes[_index++].Execute(this) == AdvancementType.Halt)
                {
                    return SyntaxTreeState.Incomplete;
                }
            }

            return SyntaxTreeState.Completed;
        }
        
        /// <summary>
        /// Resets the syntax tree.
        /// </summary>
        public void Reset()
        {
            _index = 0;
        }
    }
}