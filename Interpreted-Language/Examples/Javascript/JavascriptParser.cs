using InterpretedLanguage.Examples.Javascript.Nodes;
using InterpretedLanguage.Examples.Javascript.Patterns;
using InterpretedLanguage.Language.Lexer;
using InterpretedLanguage.Language.Parser;
using InterpretedLanguage.Language.Parser.Groups.Statements;
using InterpretedLanguage.Language.Parser.SyntaxTree;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Examples.Javascript
{
    internal class JavascriptParser
    {
        private readonly Lexer _lexer = new Lexer();
        private readonly Parser _parser = new Parser();

        public JavascriptParser()
        {
            ConstructLexer();
            ConstructParser();
        }

        public SyntaxTree Parse(string input)
        {
            var tree = new SyntaxTree();
            _parser.Parse(tree, _lexer.Tokenise(input));
            return tree;
        }

        private void ConstructLexer()
        {
            _lexer.CreateGrammar()
                .AddRule(JavascriptTokens.Identifier, "^([_A-Za-z][_A-Za-z0-9]*)")
                .AddRule(JavascriptTokens.String, @"^([""'`])(?:(?=(\\?))\2.)*?\1")
                .AddRule(JavascriptTokens.Punctuation, "^([.|(|)|;])");

            _lexer.GetRule(JavascriptTokens.String)
                .ProcessValue(value => value.Substring(1, value.Length - 2));
        }

        private void ConstructParser()
        {
            _parser.CreateGroup("New Lines")
                .Add(new ExpectAndIgnore(ReservedTokens.NewLine));

            _parser.CreateGroup("Console")
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