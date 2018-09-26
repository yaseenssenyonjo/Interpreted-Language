using System.Linq;
using Interpreted_Language.JS.Language.Grammar;
using Interpreted_Language.JS.Language.Interpreter;
using Interpreted_Language.JS.Language.Nodes;
using Interpreted_Language.Language.Interpreter;
using Interpreted_Language.Language.Lexer;
using Interpreted_Language.Language.Lexer.Grammar.Traits;
using Interpreted_Language.Language.Lexer.Tokens;
using Interpreted_Language.Language.Parser;
using Interpreted_Language.Language.Parser.Groups.Statements;
using Interpreted_Language.Language.Parser.Syntax;

namespace Interpreted_Language
{
    internal static class JSProgram
    {
        private const string Script = @"
console.log('this line will never be seen!')
console.clear()
console.log('hello, this an example!')
console.log('multiple lines!')
console.log('...')";

        public static void Usage()
        {
            var tokens = Tokenise(new JSGrammar());
            var syntaxTree = Parse(tokens);
            Execute(syntaxTree);
        }
        
        private static TokenList Tokenise(IGrammar grammar)
        {           
            var lexer = new Lexer(grammar);
            var tokens = lexer.Tokenise(Script);
            
            return tokens;
        }

        private static SyntaxTree Parse(TokenList tokens)
        {
            var parser = new Parser();
            
            // This group matches any line that just has a new line.
            parser.CreateGroup("New Lines", doesCreateNode: false)
                .Add(new ExpectAndIgnore(TokenType.NewLine));
            
            parser.CreateGroup("console.method_name(arguments)")
                .Add(new Capture(TokenType.Keyword, "console"))
                .Add(new ExpectAndIgnore(TokenType.Punctuation, "."))
                .Add(new Capture(TokenType.Identifier, "methodName"))
                .Add(new ExpectAndIgnore(TokenType.Punctuation, "("))
                .Add(new ConsumeUntil(TokenType.Punctuation, ")", "arguments"))
                .Add(new ExpectAndIgnore(TokenType.NewLine))
                .CreateNode(variables => new ConsoleNode((string)variables["methodName"], ((Token[])variables["arguments"]).Select(t => t.Value).ToArray()));

            
            var syntaxTree = parser.Parse(tokens);
            return syntaxTree;
        }
        
        private static void Execute(SyntaxTree syntaxTree)
        {
            var executionContext = new JSExecutionContext();
            var interpreter = new Interpreter(executionContext);
            interpreter.Execute(syntaxTree);
        }
    }
}