using Interpreted_Language.Language.Lexer.Tokens;
using Interpreted_Language.Language.Parser.Groups.Statements.Traits;

namespace Interpreted_Language.Language.Parser.Groups.Statements
{
    /// <summary>
    /// Expects and ignores a specific token.
    /// </summary>
    internal class ExpectAndIgnore : IStatement
    {
        /// <summary>
        /// The type of token to expect and ignore.
        /// </summary>
        private readonly TokenType _tokenType;

        /// <summary>
        /// The value of the token to expect and ignore.
        /// </summary>
        private readonly object _value;

        public ExpectAndIgnore(TokenType type)
        {
            _tokenType = type;
        }

        /// <summary>
        /// </summary>
        /// <param name="type">The expected token type.</param>
        /// <param name="value">The expected value of the token.</param>
        public ExpectAndIgnore(TokenType type, object value) : this(type)
        {
            _value = value;
        }

        /// <summary>
        /// Determines whether the current token type is the same and if so ignores the token.
        /// </summary>
        /// <returns><c>true</c> if the current token type is the same, otherwise <c>false</c>.</returns>
        public bool Evaluate(Group group, TokenList tokens)
        {            
            // Get the next token.
            var token = tokens.Next();

            // Ignore the token by doing nothing with it.

            // If the token type or token value (if set) is not the expected result, return false.
            return token.Type == _tokenType && (_value == null || token.Value.Equals(_value));
        }
    }
}