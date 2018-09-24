using System.Text.RegularExpressions;
using Interpreted_Language.Language.Lexer.Tokens;

namespace Interpreted_Language.Language.Lexer
{
    /// <summary>
    /// Represents a lexical rule.
    /// </summary>
    internal class LexicalRule
    {
        /// <summary>
        /// The type of token associated with this rule.
        /// </summary>
        public readonly TokenType TokenType;
        /// <summary>
        /// The regular expression.
        /// </summary>
        public readonly Regex RegularExpression;
        /// <summary>
        /// Determines if this token type is returned when found.
        /// </summary>
        public readonly bool IsIgnored;

        /// <summary>
        /// Initializes a new instance of the <see cref="Interpreted_Language.Language.Lexer.LexicalRule"/> class.
        /// </summary>
        /// <param name="type">The token type to associate with.</param>
        /// <param name="pattern">The pattern to associate the token with.</param>
        public LexicalRule(TokenType type, string pattern)
        {
            TokenType = type;
            RegularExpression = new Regex(pattern, RegexOptions.Compiled);
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Interpreted_Language.Language.Lexer.LexicalRule"/> class.
        /// </summary>
        /// <param name="pattern">The pattern to associate the token with.</param>
        /// <remarks>Without a defined token type, the token is ignored.</remarks>
        public LexicalRule(string pattern) : this(TokenType.None, pattern)
        {
            IsIgnored = true;
        }
        
        /// <summary>
        /// Compares this instance to a specified lexical rule and returns an indication of their relative values.
        /// </summary>
        /// <param name="lexicalRule">The lexical rule to compare to.</param>
        /// <returns></returns>
        public int CompareTo(LexicalRule lexicalRule)
        {
            return TokenType.CompareTo(lexicalRule.TokenType);
        }
    }
}