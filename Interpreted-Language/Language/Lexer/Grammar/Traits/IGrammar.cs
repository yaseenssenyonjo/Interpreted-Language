using System.Collections.Generic;

namespace Interpreted_Language.Language.Lexer.Grammar.Traits
{
    /// <summary>
    /// 
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