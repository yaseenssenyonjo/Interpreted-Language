using Interpreted_Language.Language.Parser.Syntax;

namespace Interpreted_Language.Language.Interpreter
{
    /// <summary>
    /// Represents a halted syntax tree.
    /// </summary>
    internal struct HaltedSyntaxTree
    {
        /// <summary>
        /// The index.
        /// </summary>
        public readonly int Index;
        /// <summary>
        /// The halted syntax tree.
        /// </summary>
        public readonly SyntaxTree Tree;

        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.Language.Interpreter.HaltedSyntaxTree"/> struct.
        /// </summary>
        /// <param name="tree">The halted syntax tree.</param>
        /// <param name="index">The index.</param>
        public HaltedSyntaxTree(SyntaxTree tree, int index)
        {
            Tree = tree;
            Index = index;
        }
    }
}