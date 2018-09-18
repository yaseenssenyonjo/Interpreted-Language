using System.Collections.Generic;
using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;

namespace Interpreted_Language.Language.Parser.Syntax
{
    internal class SyntaxTree
    {
        /// <summary>
        /// The nodes.
        /// </summary>
        private readonly List<INode> _nodes = new List<INode>();
        
        /// <summary>
        /// Adds the specified node to the tree.
        /// </summary>
        /// <param name="node">The node to add.</param>
        public void Add(INode node)
        {
            _nodes.Add(node);
        }
        
        // TODO: Use nodes for when executing (maybe make it a queue - FIFO).
    }
}