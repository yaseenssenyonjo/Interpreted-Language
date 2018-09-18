using System;
using System.Text;

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
        
        public Token(TokenType type, string value, int lineNumber)
        {
            Type = type;
            Value = ProcessValue(value);
            LineNumber = lineNumber;
        }

        private object ProcessValue(string value)
        {
            switch (Type)
            {
                case TokenType.String:
                {
                    var stringBuilder = new StringBuilder();
                    // The first character is the string delimiter.
                    var stringDelimiter = value[0];
                    // Starting from 1 skips the first character and minus 1 from the length skips the last character which
                    // are both quotation marks.
                    for (var i = 1; i < value.Length - 1; i++)
                    {
                        var currentCharacter = value[i];
                        var nextCharacter = i + 1 < value.Length - 1 ? value[i + 1] : '\x00';
                        // If the current character is a backslash and the next character is the string delimiter.
                        if (currentCharacter == '\\' && nextCharacter == stringDelimiter)
                        {
                            // Append the string delimiter.
                            stringBuilder.Append(nextCharacter);
                            // Increment i only once here as the for loop will do it again thus skipping to the right character.
                            i++; 
                            continue;
                        }
                    
                        stringBuilder.Append(currentCharacter);
                    }
                
                    return stringBuilder.ToString();
                }
                
                case TokenType.Integer:
                    return Convert.ToInt32(value);
                
                default:
                    return value;
            }
        }
    }
}