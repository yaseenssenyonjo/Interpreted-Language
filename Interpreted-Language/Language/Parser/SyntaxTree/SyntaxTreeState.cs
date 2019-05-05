namespace InterpretedLanguage.Language.Parser.SyntaxTree
{
    /// <summary>
    /// Specifies the state of the syntax tree.
    /// </summary>
    internal enum SyntaxTreeState
    {
        /// <summary>
        /// Specifies the syntax tree has not completed execution.
        /// </summary>
        Incomplete,
        /// <summary>
        /// Specifies the syntax tree has completed execution.
        /// </summary>
        Completed
    }
}