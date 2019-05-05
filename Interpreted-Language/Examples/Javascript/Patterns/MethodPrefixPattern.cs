using System.Collections.Generic;
using InterpretedLanguage.Language.Parser.Groups.Statements;
using InterpretedLanguage.Language.Parser.Groups.Traits;

namespace InterpretedLanguage.Examples.Javascript.Patterns
{
    internal class MethodPrefixPattern : IGroupPattern
    {
        private readonly string _prefix;

        public MethodPrefixPattern(string prefix)
        {
            _prefix = prefix;
        }

        public List<IGroupStatement> CreateStatements()
        {
            return new List<IGroupStatement>
            {
                new ExpectAndIgnore(JavascriptTokens.Identifier, _prefix),
                new ExpectAndIgnore(JavascriptTokens.Punctuation, ".")
            };
        }
    }
}