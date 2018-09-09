namespace Interpreted_Language.Language.Lexer.Tokens
{
    /// <summary>
    /// Represents a token.
    /// </summary>
    internal class Token
    {
        /// <summary>
        /// The type of the token.
        /// </summary>
        public readonly TokenType Type;
        /// <summary>
        /// The value of the token.
        /// </summary>
        public readonly string Value;
        
        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}