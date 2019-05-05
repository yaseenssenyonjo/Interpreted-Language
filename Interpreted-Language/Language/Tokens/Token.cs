namespace InterpretedLanguage.Language.Tokens
{
    internal class Token
    {
        public readonly int LineNumber;
        public readonly int Type;
        public readonly object Value;

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