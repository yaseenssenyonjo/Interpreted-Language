using System.Collections.Generic;
using InterpretedLanguage.Parser.SyntaxTree.Traits;

namespace InterpretedLanguage.Parser.SyntaxTree
{
    internal class SyntaxTree
    {
        private readonly List<INode> _nodes = new List<INode>();
        private int _index;
        
        public void Add(INode node)
        {
            _nodes.Add(node);
        }

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
    }
}