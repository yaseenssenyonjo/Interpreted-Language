using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Language.Parser.Groups.Traits
{
    internal interface IGroupStatement
    {
        bool Match(Group group, TokenList tokens);
    }
}