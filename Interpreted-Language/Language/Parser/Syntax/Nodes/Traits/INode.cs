using Interpreted_Language.Language.Interpreter.Traits;

namespace Interpreted_Language.Language.Parser.Syntax.Nodes.Traits
{
    /// <summary>
    /// Represents a node.
    /// </summary>
    internal interface INode
    {
        /// <summary>
        /// The type of node.
        /// </summary>
        int NodeType { get; set; }
        
        /// <summary>
        /// The line number for this node.
        /// </summary>
        int LineNumber { set; }
        
        /// <summary>
        /// Executes this node.
        /// </summary>
        /// <param name="context">The execution context this node is executing in.</param>
        void Execute(IExecutionContext context);
        
        // TODO: Create a custom operator overload that compares nodes using their node type.
    }
}