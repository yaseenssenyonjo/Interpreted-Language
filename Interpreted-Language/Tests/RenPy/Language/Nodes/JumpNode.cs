using System;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;
using Interpreted_Language.RenPy.Interpreter;

namespace Interpreted_Language.RenPy.Language.Nodes
{
    internal class JumpNode : INode
    {
        private string _labelName;
        
        public int NodeType { get; set; }
        public int LineNumber { get; set; }

        public JumpNode(string labelName)
        {
            _labelName = labelName;
        }
        
        public void Execute(IExecutionContext context)
        {
            Console.WriteLine($"Jumping to {_labelName}.");
            
            var renPyContext = (RenPyExecutionContext) context;
            renPyContext.TryExecuteLabel(_labelName);
        }
    }
}