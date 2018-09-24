using System.Collections.Generic;

namespace Interpreted_Language.Language.Lexer.Grammar.Traits
{
    /// <summary>
    /// Represents a collection of structural rules that create the grammar.
    /// </summary>
    internal interface IGrammar
    {
        /// <summary>
        /// The name of the grammar.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The grammar rules.
        /// </summary>
        List<LexicalRule> Rules { get; }
    }
}