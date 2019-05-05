namespace InterpretedLanguage.Language.Tokens
{
    /// <summary>
    /// Represents a token.
    /// </summary>
    internal class Token
    {
        /// <summary>
        /// The type of token.
        /// </summary>
        public readonly int Type;
        /// <summary>
        /// The value of the token.
        /// </summary>
        public readonly object Value;
        /// <summary>
        /// The line number of the token.
        /// </summary>
        public readonly int LineNumber;

        /// <summary>
        /// Initialises a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="type">The type of token.</param>
        /// <param name="value">The value of the token.</param>
        /// <param name="lineNumber">The line number of the token.</param>
        public Token(int type, object value, int lineNumber)
        {
            Type = type;
            Value = value;
            LineNumber = lineNumber;
        }

        public override string ToString()
        {
            return $"[Token]: Type: {Type}, Value: {Value}, Line Number: {LineNumber}";
        }
    }
}