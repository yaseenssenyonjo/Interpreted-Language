using System.Collections.Generic;
using Interpreted_Language.Language.Lexer;
using Interpreted_Language.Language.Lexer.Grammar;
using Interpreted_Language.Language.Lexer.Grammar.Traits;
using Interpreted_Language.Language.Lexer.Tokens;

namespace Interpreted_Language.JS.Language.Grammar
{
    internal class JSGrammar : IGrammar
    {
        public string Name { get; } = "JS";
        public List<LexicalRule> Rules { get; } = new List<LexicalRule>
        {
            new LexicalRule(TokenType.Identifier, "^([_A-Za-z][_A-Za-z0-9]*)"),
            new LexicalRule(TokenType.String, @"^([""'`])(?:(?=(\\?))\2.)*?\1"),
            new LexicalRule(TokenType.Punctuation, "^([.|(|)])"),
            new LexicalRule(TokenType.Keyword, GrammarHelper.CreateKeywordPattern("console"))
        };
    }
}