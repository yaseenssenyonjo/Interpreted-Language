using System;

namespace InterpretedLanguage.Language.Lexer.Exceptions
{
    internal class LexicalException : Exception
    {
        public LexicalException(string message) : base(message)
        {
        }
    }
}