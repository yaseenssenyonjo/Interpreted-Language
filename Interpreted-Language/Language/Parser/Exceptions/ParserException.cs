using System;

namespace Interpreted_Language.Language.Parser.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is an issue when parsing tokens.
    /// </summary>
    internal class ParserException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.Language.Parser.Exceptions.ParserException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ParserException(string message) : base(message) {}
    }
}