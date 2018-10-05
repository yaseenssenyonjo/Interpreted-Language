using System;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax;
using Interpreted_Language.Language.Parser.Syntax.Nodes;
using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;
using Interpreted_Language.RenPy.Language.Interpreter;

namespace Interpreted_Language.RenPy.Language.Nodes
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
        public int LineNumber { private get; set; }
        
        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.RenPy.Language.Nodes.LabelNode"/> class.
        /// </summary>
        /// <param name="name">The name of this label.</param>
        /// <param name="syntaxTree">The syntax tree for this label.</param>
        public LabelNode(string name, SyntaxTree syntaxTree)
        {
            _name = name;
            _syntaxTree = syntaxTree;
        }
        
        public BlockingType Execute(IExecutionContext context)
        {
            var renPyContext = (RenPyExecutionContext) context;
            
            // When this method is first called by the interpreter the context will not
            // have this label so the label registers itself thus when this node
            // is next executed is due to it being called by another node.
            
            if (!renPyContext.IsLabelRegistered(_name))
            {
                renPyContext.RegisterLabel(_name, this);
                return BlockingType.NonBlocking;
            }

            var interpreter = new Interpreted_Language.Language.Interpreter.Interpreter(context);
            interpreter.Execute(_syntaxTree);
            
            return BlockingType.NonBlocking;
        }
    }
}