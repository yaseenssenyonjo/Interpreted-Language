using InterpretedLanguage.Language.Parser.Groups.Traits;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Language.Parser.Groups.Statements
{
    internal class ExpectAndIgnore : IGroupStatement
    {
        private readonly int _type;
        private readonly object _value;

        public ExpectAndIgnore(int type)
        {
            _type = type;
        }

        public ExpectAndIgnore(int type, object value) : this(type)
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