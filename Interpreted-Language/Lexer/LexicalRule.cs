using System.Text.RegularExpressions;
using InterpretedLanguage.Lexer.Exceptions;
using InterpretedLanguage.Tokens;

namespace InterpretedLanguage.Lexer
{
    internal class LexicalRule
    {
        private readonly TokenType _type;
        private readonly Regex _regex;
        
        public LexicalRule(TokenType type, string pattern)
        {
            _type = type;
            _regex = new Regex(pattern, RegexOptions.Compiled);
            ValidateRegex();
        }

        private void ValidateRegex()
        {
            var numberOfCaptureGroups = _regex.GetGroupNumbers().Length - 1;
            
            if (numberOfCaptureGroups != 1)
            {
                throw new LexicalException($"There are an invalid number of capture groups. Expected 1, got {numberOfCaptureGroups}.");
            }
        }

        public bool TryCreateToken(ref LexicalLine line, out Token token)
        {
            var match = _regex.Match(line);

            if (match.Success)
            {
                token = new Token(_type, match.Groups[0].Value, line);
                line.Seek(match.Length);
                return true;
            }

            token = null;
            return false;
        }
    }
}