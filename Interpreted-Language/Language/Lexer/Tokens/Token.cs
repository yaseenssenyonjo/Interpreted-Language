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
        /// <summary>
        /// The line number this token is on.
        /// </summary>
        public readonly int LineNumber;
        
        public Token(TokenType type, string value, int lineNumber)
        {
            Type = type;
            Value = value;
            LineNumber = lineNumber;
        }
    }
}