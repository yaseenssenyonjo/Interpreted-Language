using InterpretedLanguage.Tokens;

namespace InterpretedLanguage.Parser.Groups.Traits
{
    internal interface IGroupStatement
    {
        bool Match(Group group, TokenList tokens);
    }
}