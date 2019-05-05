namespace InterpretedLanguage.Language.Parser.SyntaxTree.Traits
{
    /// <summary>
    /// Provides the contract for nodes to implement.
    /// </summary>
    internal interface INode
    {
        /// <summary>
        /// Executes the node.
        /// </summary>
        /// <param name="tree">The syntax tree for this node.</param>
        /// <returns>The advancement type.</returns>
        AdvancementType Execute(SyntaxTree tree);
    }
}