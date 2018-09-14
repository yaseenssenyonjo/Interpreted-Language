using System;

namespace Interpreted_Language.Language.Parser.Exceptions
{
    internal class ParserException : Exception
    {
        public ParserException(string message) : base(message)
        {
        }
    }
}