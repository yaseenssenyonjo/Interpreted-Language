using System;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax.Nodes;
using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;
using Interpreted_Language.RenPy.Language.Interpreter;

namespace Interpreted_Language.RenPy.Language.Nodes
{
    internal class JumpNode : INode
    {
        /// <summary>
        /// The name of the label to jump to.
        /// </summary>
        private readonly string _labelName;
        
        /// <inheritdoc />
        public int LineNumber { private get; set; }
        
        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.RenPy.Language.Nodes.JumpNode"/> class.
        /// </summary>
        /// <param name="labelName"></param>
        public JumpNode(string labelName)
        {
            _labelName = labelName;
        }
        
        public BlockingType Execute(IExecutionContext context)
        {
            Console.WriteLine($"Jumping to {_labelName}.");
            
            var renPyContext = (RenPyExecutionContext) context;
            renPyContext.TryExecuteLabel(_labelName);
            return BlockingType.NonBlocking;
        }
    }
}