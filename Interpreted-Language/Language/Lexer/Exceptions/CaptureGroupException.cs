using System;

namespace Interpreted_Language.Language.Lexer.Exceptions
{
    internal class CaptureGroupException : Exception
    {
        public CaptureGroupException(string message) : base(message) {}
    }
}