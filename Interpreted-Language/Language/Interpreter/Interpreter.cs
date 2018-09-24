using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax;

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
                node.Execute(_context);
            }
        }
    }
}