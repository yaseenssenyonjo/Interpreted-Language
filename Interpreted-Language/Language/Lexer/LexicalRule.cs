using System;
using System.Text.RegularExpressions;
using InterpretedLanguage.Language.Lexer.Exceptions;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Language.Lexer
{
    /// <summary>
    /// Represents a lexical rule.
    /// </summary>
    internal class LexicalRule
    {
        /// <summary>
        /// The type of token.
        /// </summary>
        private readonly int _type;
        /// <summary>
        /// The regular expression.
        /// </summary>
        private readonly Regex _regex;
        /// <summary>
        /// The value pre-processor function.
        /// </summary>
        private Func<string, object> _processValueFunc;
        
        /// <summary>
        /// Initialises a new instance of the <see cref="LexicalRule"/> class.
        /// </summary>
        /// <param name="type">The type of token.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        public LexicalRule(int type, string pattern)
        {
            _type = type;
            _regex = new Regex(pattern, RegexOptions.Compiled);
            ValidateRegex();
        }
        
        /// <summary>
        /// Validate the regex pattern.
        /// </summary>
        /// <exception cref="LexicalException">There are zero capture groups.</exception>
        private void ValidateRegex()
        {
            var numberOfCaptureGroups = _regex.GetGroupNumbers().Length - 1;

            if (numberOfCaptureGroups == 0)
            {
                throw new LexicalException($"There are no capture groups for {_type}. There must be at least one capture group.");
            }
        }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="line">The current line.</param>
        /// <param name="token">When this method returns, contains the token, if the token is created; otherwise, null. This parameter is passed uninitialized.</param>
        /// <returns>true, if the token was created; otherwise, false</returns>
        public bool TryCreateToken(ref LexicalLine line, out Token token)
        {
            var match = _regex.Match(line);

            if (match.Success)
            {
                object value = match.Groups[0].Value;

                if (_processValueFunc != null)
                {
                    value = _processValueFunc.Invoke(value.ToString());
                }

                token = new Token(_type, value, line);
                line.Seek(match.Length);
                return true;
            }

            token = null;
            return false;
        }
        
        /// <summary>
        /// Sets the value pre-processor function.
        /// </summary>
        /// <param name="func">The function.</param>
        public void ProcessValue(Func<string, object> func)
        {
            _processValueFunc = func;
        }
    }
}