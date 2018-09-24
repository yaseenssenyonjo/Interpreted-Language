using System;

namespace Interpreted_Language.Language.Lexer.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is an issue with a capture group.
    /// </summary>
    internal class CaptureGroupException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.Language.Lexer.Exceptions.CaptureGroupException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CaptureGroupException(string message) : base(message) {}
    }
}