using System;

namespace InterpretedLanguage.Language.Lexer.Exceptions
{
    internal class TokenConflictException : Exception
    {
        public TokenConflictException(string message) : base(message)
        {
        }
    }
}