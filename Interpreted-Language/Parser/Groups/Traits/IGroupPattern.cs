using System.Collections.Generic;

namespace InterpretedLanguage.Parser.Groups.Traits
{
    internal interface IGroupPattern
    {
        List<IGroupStatement> CreateStatements();
    }
}