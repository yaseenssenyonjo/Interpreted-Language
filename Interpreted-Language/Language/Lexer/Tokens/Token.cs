using System;
using System.Text.RegularExpressions;

namespace Interpreted_Language.Language.Lexer.Tokens
{
    /// <summary>
    /// Represents a token.
    /// </summary>
    internal class Token
    {
        /// <summary>
        /// The type of the token.
        /// </summary>
        public readonly TokenType Type;
        /// <summary>
        /// The value of the token.
        /// </summary>
        public readonly object Value;
        /// <summary>
        /// The line number this token is on.
        /// </summary>
        public readonly int LineNumber;
        
        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.Language.Lexer.Tokens.Token"/> class.
        /// </summary>
        /// <param name="type">The type of token.</param>
        /// <param name="value">The value of the token.</param>
        /// <param name="lineNumber">The line number of the token.</param>
        public Token(TokenType type, string value, int lineNumber)
        {
            Type = type;
            Value = ProcessValue(value);
            LineNumber = lineNumber;
        }
        
        /// <summary>
        /// Processes the value.
        /// </summary>
        /// <param name="value">The value to process.</param>
        /// <returns>An unescaped string if the token is a string, a 32-bit integer if the token is an integer; otherwise the specified value.</returns>
        private object ProcessValue(string value)
        {
            switch (Type)
            {
                case TokenType.String:
                {
                    // Unescapes the string.
                    var unescapedValue = Regex.Unescape(value);
                    // Strips the quotes by skipping the first and last character.
                    // Subtracting two as the length starts from 1.
                    return unescapedValue.Substring(1, unescapedValue.Length - 2);
                }
                
                case TokenType.Integer:
                    return Convert.ToInt32(value);
                
                default:
                    return value;
            }
        }
    }
}