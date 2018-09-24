using System.Collections.Generic;
using Interpreted_Language.Language.Lexer;
using Interpreted_Language.Language.Lexer.Grammar;
using Interpreted_Language.Language.Lexer.Grammar.Traits;
using Interpreted_Language.Language.Lexer.Tokens;

namespace Interpreted_Language.RenPy.Language.Grammar
{
    internal class RenPyGrammar : IGrammar
    {
        public string Name { get; } = "Ren'Py";
        public List<LexicalRule> Rules { get; } = new List<LexicalRule>
        {
            new LexicalRule(TokenType.Identifier, "^([_A-Za-z][_A-Za-z0-9]*)"), // Pattern ensures that it starts with an alphanumeric character.
            // TODO: Try full understand how this string regex pattern functions.
            new LexicalRule(TokenType.String, @"^([""'`])(?:(?=(\\?))\2.)*?\1"), // https://stackoverflow.com/questions/171480/regex-grabbing-values-between-quotation-marks
            new LexicalRule("^(#.*)"), // Matches and ignores comments (anything starting with a #).
            new LexicalRule(TokenType.Keyword, GrammarHelper.CreateKeywordPattern("character", "room", "sprite", "label", "jump")),
            new LexicalRule(TokenType.Equal, "^(=)"),
            new LexicalRule(TokenType.Tab, @"^(\t| {4})"), // This pattern also treats 4 spaces consecutively as a tab.
            new LexicalRule("^(,)"),
            new LexicalRule(TokenType.Punctuation, "^([:|(|)])"),
            new LexicalRule(TokenType.Integer, @"^(\d+)")
        };
    }
}