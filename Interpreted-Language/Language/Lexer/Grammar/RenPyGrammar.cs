using System.Collections.Generic;
using Interpreted_Language.Language.Lexer.Grammar.Traits;
using Interpreted_Language.Language.Lexer.Tokens;

namespace Interpreted_Language.Language.Lexer.Grammar
{
    internal class RenPyGrammar : IGrammar
    {
        public string Name { get; } = "Ren'Py";
        public IEnumerable<LexicalRule> Rules { get; } = new List<LexicalRule>()
        {
            new LexicalRule(TokenType.Identifier, "^([_A-Za-z][_A-Za-z0-9]*)"), // Pattern ensures that it starts with an alphanumeric character.
            // TODO: Try full understand how this string regex pattern functions.
            new LexicalRule(TokenType.String, @"^([""'`])(?:(?=(\\?))\2.)*?\1"), // https://stackoverflow.com/questions/171480/regex-grabbing-values-between-quotation-marks
            new LexicalRule(TokenType.Comment, "^(#.*)"),
            new LexicalRule(TokenType.Keyword, "^(character|label|pass)"),
            new LexicalRule(TokenType.Equal, "^(=)"),
            new LexicalRule(TokenType.Tab, "^(\t)"),
            new LexicalRule(TokenType.Punctuation, "^([:|(|)])"),
            new LexicalRule(TokenType.Integer, @"^(\d+)")
        };
    }
}