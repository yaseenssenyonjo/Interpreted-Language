using InterpretedLanguage.Language.Parser.Groups.Traits;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Language.Parser.Groups.Statements
{
    public class ConsumeUntil : IGroupStatement
    {
        private readonly string _name;
        private readonly int _type;
        private readonly object _value;

        public ConsumeUntil(int type, string name)
        {
            _type = type;
            _name = name;
        }

        public ConsumeUntil(int type, object value, string name) : this(type, name)
        {
            _value = value;
        }

        public bool Match(Group group, TokenList tokens)
        {
            var consumedTokens = new TokenList();

            while (!tokens.EndOfStream())
            {
                var token = tokens.Advance();

                if (token.Type == ReservedTokens.NewLine) return false;
                if (token.Type == _type && (_value == null || token.Value.Equals(_value))) break;

                consumedTokens.Add(token);
            }

            group.AddVariable(_name, consumedTokens);
            return true;
        }
    }
}