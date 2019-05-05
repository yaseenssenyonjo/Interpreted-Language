using System;

namespace InterpretedLanguage.Language.Parser.Exceptions
{
    internal class ParserException : Exception
    {
        public ParserException(string message) : base(message)
        {
        }
    }
}