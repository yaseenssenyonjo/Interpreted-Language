using System;

namespace Interpreted_Language.Language.Lexer.Exceptions
{
    internal class LexerException  : Exception
    {
        public LexerException(string message) : base(message) {}
    }
}