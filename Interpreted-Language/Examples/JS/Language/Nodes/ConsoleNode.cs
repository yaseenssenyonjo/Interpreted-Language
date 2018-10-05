using System;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax.Nodes;
using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;

namespace Interpreted_Language.JS.Language.Nodes
{
    internal class ConsoleNode : INode
    {       
        /// <summary>
        /// The name of the method.
        /// </summary>
        private readonly string _methodName;
        /// <summary>
        /// The arguments for the method.
        /// </summary>
        private readonly object[] _methodArguments;
        
        /// <inheritdoc />
        public int LineNumber { private get; set; }
        
        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.JS.Language.Nodes.ConsoleNode"/> class.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="methodArguments"></param>
        public ConsoleNode(string methodName, object[] methodArguments)
        {
            _methodName = methodName;
            _methodArguments = methodArguments;
        }
        
        public BlockingType Execute(IExecutionContext context)
        {
            switch(_methodName)
            {
                case "log":
                    Console.WriteLine(_methodArguments[0]);
                    break;
			        
                case "clear":
                    Console.Clear();
                    break;
            }

            return BlockingType.NonBlocking;
        }
    }
}