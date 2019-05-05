using System.Collections.Generic;

namespace InterpretedLanguage.Language.Parser.Groups.Traits
{
    internal interface IGroupPattern
    {
        List<IGroupStatement> CreateStatements();
    }
}