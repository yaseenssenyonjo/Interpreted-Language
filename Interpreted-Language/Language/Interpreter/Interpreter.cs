using System;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax;
using Interpreted_Language.Language.Parser.Syntax.Nodes;

namespace Interpreted_Language.Language.Interpreter
{
    /// <summary>
    /// Represents an interpreter.
    /// </summary>
    internal class Interpreter
    {
        /// <summary>
        /// The execution context for this interpreter.
        /// </summary>
        private readonly IExecutionContext _context;
        
        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.Language.Interpreter.Interpreter"/> class.
        /// </summary>
        /// <param name="context">The execution context for this interpreter.</param>
        public Interpreter(IExecutionContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Executes the specified syntax tree.
        /// </summary>
        /// <param name="syntaxTree"></param>
        public void Execute(SyntaxTree syntaxTree)
        {
            foreach (var node in syntaxTree)
            {
                if (node.Execute(_context) == BlockingType.Blocking)
                {
                    // TODO: Come up with an alternative method to processing blocking and non-blocking statements.
                    // Currently feels off to me.
                    
                    // This is an example of what the node could do if it is blocking.
                    // Another example is waiting for a mouse click.
                    Console.WriteLine("Waiting for user input before continuing...");
                    Console.ReadKey();
                }
            }
        }
    }
}