using InterpretedLanguage.Parser.Groups.Traits;
using InterpretedLanguage.Tokens;

namespace InterpretedLanguage.Parser.Groups.Statements
{
    internal class ConsumeUntil : IGroupStatement
    {
        private readonly TokenType _type;
        private readonly string _name;

        public ConsumeUntil(TokenType type, string name)
        {
            _type = type;
            _name = name;
        }
        
        public bool Match(Group group, TokenList tokens)
        {
            var consumedTokens = new TokenList();

            while (!tokens.EndOfStream())
            {
                var token = tokens.Advance();

                if (token.Type == TokenType.NewLine) return false;
                if (token.Type == _type) break;
                
                consumedTokens.Add(token);
            }

            group.AddVariable(_name, consumedTokens);
            return true;
        }
    }
}