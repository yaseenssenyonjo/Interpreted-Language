namespace InterpretedLanguage.Tokens
{
    internal class Token
    {
        public readonly TokenType Type;
        public readonly string Value;
        public readonly int LineNumber;
        
        public Token(TokenType type, string value, int lineNumber)
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