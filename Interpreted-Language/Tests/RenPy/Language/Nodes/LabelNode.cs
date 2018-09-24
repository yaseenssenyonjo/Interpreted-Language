using System;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax;
using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;
using Interpreted_Language.RenPy.Interpreter;

namespace Interpreted_Language.RenPy.Nodes
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
        
        /// <inheritdoc />
        public int NodeType { get; set; } = (int)Nodes.NodeType.Label;
        /// <inheritdoc />
        public int LineNumber { private get; set; }
        
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
        
        public void Execute(IExecutionContext context)
        {
            var renPyContext = (RenPyExecutionContext) context;
            
            // When this method is first called by the interpreter the context will not
            // have this label so the label registers itself thus when this node
            // is next executed is due to it being called by another node.
            
            if (!renPyContext.HasLabel(_name))
            {
                renPyContext.RegisterLabel(_name, this);
                return;
            }
            
            foreach (var node in _syntaxTree) node.Execute(context);
        }
    }
}