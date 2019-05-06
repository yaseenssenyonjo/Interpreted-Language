using InterpretedLanguage.Language.Parser.Groups.Traits;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Language.Parser.Groups.Statements
{
    public class Capture : IGroupStatement
    {
        private readonly string _name;
        private readonly int _type;

        public Capture(int type, string name)
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