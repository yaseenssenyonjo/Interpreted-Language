namespace Interpreted_Language.Language.Lexer.Tokens
{
    /// <summary>
    /// Represents a token type.
    /// </summary>
    internal enum TokenType
    {
        Keyword,
        Identifier,
        String,
        Integer,
        NewLine,
        Equal,
        Punctuation,
        Tab,
        None
    }
}