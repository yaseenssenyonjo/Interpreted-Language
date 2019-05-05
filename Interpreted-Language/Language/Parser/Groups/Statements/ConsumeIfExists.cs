using InterpretedLanguage.Language.Parser.Groups.Traits;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Language.Parser.Groups.Statements
{
    internal class ConsumeIfExists : IGroupStatement
    {
        private readonly int _type;
        private readonly object _value;

        public ConsumeIfExists(int type)
        {
            _type = type;
        }

        public ConsumeIfExists(int type, object value) : this(type)
        {
            _value = value;
        }

        public bool Match(Group group, TokenList tokens)
        {
            if (tokens.EndOfStream()) return true;

            var token = tokens.Peek();
            if (token.Type == _type && (_value == null || token.Value.Equals(_value))) tokens.Advance();

            return true;
        }
    }
}