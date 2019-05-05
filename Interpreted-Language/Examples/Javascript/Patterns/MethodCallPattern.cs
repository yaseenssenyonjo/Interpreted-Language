using System.Collections.Generic;
using InterpretedLanguage.Language.Parser.Groups.Statements;
using InterpretedLanguage.Language.Parser.Groups.Traits;

namespace InterpretedLanguage.Examples.Javascript.Patterns
{
    internal class MethodCallPattern : IGroupPattern
    {
        public List<IGroupStatement> CreateStatements()
        {
            return new List<IGroupStatement>
            {
                new Capture(JavascriptTokens.Identifier, "methodName"),
                new ExpectAndIgnore(JavascriptTokens.Punctuation, "("),
                new ConsumeUntil(JavascriptTokens.Punctuation, ")", "arguments")
            };
        }
    }
}