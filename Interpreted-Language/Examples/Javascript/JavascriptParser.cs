using InterpretedLanguage.Examples.Javascript.Nodes;
using InterpretedLanguage.Examples.Javascript.Patterns;
using InterpretedLanguage.Language.Lexer;
using InterpretedLanguage.Language.Parser;
using InterpretedLanguage.Language.Parser.Groups.Statements;
using InterpretedLanguage.Language.Parser.SyntaxTree;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Examples.Javascript
{
    internal static class JavascriptParser
    {
        private static readonly Lexer Lexer = new Lexer();
        private static readonly Parser Parser = new Parser();

        static JavascriptParser()
        {
            ConstructLexer();
            ConstructParser();
        }

        public static SyntaxTree Parse(string input)
        {
            var tree = new SyntaxTree();
            Parser.Parse(tree, Lexer.Tokenise(input));
            return tree;
        }

        private static void ConstructLexer()
        {
            Lexer.CreateGrammar()
                .AddRule(JavascriptTokens.Identifier, "^([_A-Za-z][_A-Za-z0-9]*)")
                .AddRule(JavascriptTokens.String, @"^([""'`])(?:(?=(\\?))\2.)*?\1")
                .AddRule(JavascriptTokens.Punctuation, "^([.|(|)|;])");

            Lexer.GetRule(JavascriptTokens.String)
                .ProcessValue(value => value.Substring(1, value.Length - 2));
        }

        private static void ConstructParser()
        {
            Parser.CreateGroup("New Lines")
                .Add(new ExpectAndIgnore(ReservedTokens.NewLine));

            Parser.CreateGroup("Console")
                .Add(new MethodPrefixPattern("console"))
                .Add(new MethodCallPattern())
                .Add(new ConsumeIfExists(JavascriptTokens.Punctuation, ";"))
                .Add(new ConsumeIfExists(ReservedTokens.NewLine))
                .CreateNode((variables, lineNumber) =>
                {
                    var methodName = (string) variables["methodName"];
                    var methodArguments = (TokenList) variables["arguments"];

                    return new ConsoleNode(methodName, methodArguments);
                });
        }
    }
}