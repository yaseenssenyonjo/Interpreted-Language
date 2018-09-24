using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax;

namespace Interpreted_Language.Language.Interpreter
{
    internal class Interpreter
    {
        /// <summary>
        /// The execution context for this interpreter.
        /// </summary>
        private readonly IExecutionContext _context;
        
        public Interpreter(IExecutionContext context)
        {
            _context = context;
        }
        
        public void Execute(SyntaxTree syntaxTree)
        {
            foreach (var node in syntaxTree)
            {
                node.Execute(_context);
            }
        }
    }
}