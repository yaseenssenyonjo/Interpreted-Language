using InterpretedLanguage.Parser.Groups.Traits;
using InterpretedLanguage.Tokens;

namespace InterpretedLanguage.Parser.Groups.Statements
{
    internal class ExpectAndIgnore : IGroupStatement
    {
        private readonly TokenType _type;
        private readonly object _value;
        
        public ExpectAndIgnore(TokenType type)
        {
            _type = type;
        }

        public ExpectAndIgnore(TokenType type, object value) : this(type)
        {
            _value = value;
        }
        
        public bool Match(Group group, TokenList tokens)
        {
            var token = tokens.Advance();
            return token.Type == _type && (_value == null || token.Value.Equals(_value));
        }
    }
}