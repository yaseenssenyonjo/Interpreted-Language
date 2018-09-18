using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;

namespace Interpreted_Language.Language.Parser.Syntax.Nodes
{
    internal class LabelNode : INode
    {
        /// <summary>
        /// The name of this label.
        /// </summary>
        private readonly string _name;
        /// <summary>
        /// The syntax tree for this label.
        /// </summary>
        private readonly SyntaxTree _syntaxTree;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">The name of this label.</param>
        /// <param name="syntaxTree">The syntax tree for this label.</param>
        public LabelNode(string name, SyntaxTree syntaxTree)
        {
            _name = name;
            _syntaxTree = syntaxTree;
        }
        
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}