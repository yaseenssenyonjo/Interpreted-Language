using InterpretedLanguage.Parser.Groups.Traits;
using InterpretedLanguage.Tokens;

namespace InterpretedLanguage.Parser.Groups.Statements
{
    internal class ConsumeIfExists : IGroupStatement
    {
        private readonly TokenType _type;

        public ConsumeIfExists(TokenType type)
        {
            _type = type;
        }
        
        public bool Match(Group group, TokenList tokens)
        {
            if (tokens.EndOfStream()) return true;
            
            var token = tokens.Peek();
            if (token.Type == _type) tokens.Advance();

            return true;
        }
    }
}