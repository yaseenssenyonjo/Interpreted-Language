using Interpreted_Language.Language.Interpreter.Traits;

namespace Interpreted_Language.Language.Parser.Syntax.Nodes.Traits
{
    /// <summary>
    /// Represents a node.
    /// </summary>
    internal interface INode
    {
        /// <summary>
        /// The line number for this node.
        /// </summary>
        int LineNumber { set; }
        
        /// <summary>
        /// Executes this node.
        /// </summary>
        /// <param name="context">The execution context this node is executing in.</param>
        void Execute(IExecutionContext context);
    }
}