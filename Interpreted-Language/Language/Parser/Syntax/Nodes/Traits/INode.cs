namespace Interpreted_Language.Language.Parser.Syntax.Nodes.Traits
{
    /// <summary>
    /// Represents a node.
    /// </summary>
    internal interface INode
    {
        /// <summary>
        /// Executes this node.
        /// </summary>
        void Execute();
    }
}