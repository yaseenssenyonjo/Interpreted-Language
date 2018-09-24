using System;

namespace Interpreted_Language.Language.Lexer.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is an issue when tokenising input.
    /// </summary>
    internal class LexerException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.Language.Lexer.Exceptions.LexerException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LexerException(string message) : base(message) {}
    }
}