using System;

namespace InterpretedLanguage.Lexer.Exceptions
{
    internal class LexicalException : Exception
    {
        public LexicalException(string message) : base(message)
        {
        }
    }
}