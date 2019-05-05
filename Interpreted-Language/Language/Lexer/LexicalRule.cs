using System;
using System.Text.RegularExpressions;
using InterpretedLanguage.Language.Lexer.Exceptions;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Language.Lexer
{
    internal class LexicalRule
    {
        private readonly Regex _regex;
        private readonly int _type;
        private Func<string, object> _processValueFunc;

        public LexicalRule(int type, string pattern)
        {
            _type = type;
            _regex = new Regex(pattern, RegexOptions.Compiled);
            ValidateRegex();
        }

        private void ValidateRegex()
        {
            var numberOfCaptureGroups = _regex.GetGroupNumbers().Length - 1;

            if (numberOfCaptureGroups == 0)
                throw new LexicalException(
                    $"There are no capture groups for {_type}. There must be at least one capture group.");
        }

        public bool TryCreateToken(ref LexicalLine line, out Token token)
        {
            var match = _regex.Match(line);

            if (match.Success)
            {
                object value = match.Groups[0].Value;

                if (_processValueFunc != null) value = _processValueFunc.Invoke(value.ToString());

                token = new Token(_type, value, line);
                line.Seek(match.Length);
                return true;
            }

            token = null;
            return false;
        }

        public void ProcessValue(Func<string, object> func)
        {
            _processValueFunc = func;
        }
    }
}