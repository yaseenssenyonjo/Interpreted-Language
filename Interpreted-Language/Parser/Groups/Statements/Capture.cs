using InterpretedLanguage.Parser.Groups.Traits;
using InterpretedLanguage.Tokens;

namespace InterpretedLanguage.Parser.Groups.Statements
{
    internal class Capture : IGroupStatement
    {
        private readonly TokenType _type;
        private readonly string _name;

        public Capture(TokenType type, string name)
        {
            _type = type;
            _name = name;
        }
        
        public bool Match(Group group, TokenList tokens)
        {
            var token = tokens.Advance();
            if (token.Type != _type) return false;
            group.AddVariable(_name, token.Value);
            return true;
        }
    }
}