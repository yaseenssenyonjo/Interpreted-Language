using System;
using System.IO;
using System.Linq;
using Interpreted_Language.Language.Lexer;
using Interpreted_Language.Language.Lexer.Tokens;
using Interpreted_Language.Language.Lexer.Grammar;
using Interpreted_Language.Language.Lexer.Grammar.Traits;
using Interpreted_Language.Language.Parser;
using Interpreted_Language.Language.Parser.Groups.Statements;
using Interpreted_Language.Language.Parser.SyntaxTree.Nodes;

namespace Interpreted_Language
{
    internal static class Program
    {
        public static void Main()
        {
            var tokens = Tokenise(new RenPyGrammar(), "./Data/script.rpy");
            
            foreach (var token in tokens)
            {
                Console.WriteLine($"{token.Type} | {token.Value} | {token.LineNumber}");
            }

            Parse(tokens);
        }
        
        
        private static TokenList Tokenise(IGrammar grammar, string path)
        {
            Console.WriteLine($"{grammar.Name} Lexer\n");
            
            var lexer = new Lexer(grammar);
            var tokens = lexer.Tokenise(File.ReadAllText(path));

            return tokens;
        }
        
        private static void Parse(TokenList tokens)
        {
            var parser = new Parser();
            
            // This group matches any line that just has
            parser.CreateGroup("New Lines")
                .Add(new ExpectAndIgnore(TokenType.NewLine));
            
            // This group matches variableName = methodName(arguments).
            parser.CreateGroup("Assignment")
                .Add(new Capture(TokenType.Identifier, "variableName"))
                .Add(new ExpectAndIgnore(TokenType.Equal))
                .Add(new Capture(TokenType.Identifier, "methodName"))
                .Add(new ExpectAndIgnore(TokenType.Punctuation, "("))
                .Add(new ConsumeUntil(TokenType.Punctuation, ")", "arguments"))
                .Add(new ExpectAndIgnore(TokenType.NewLine))
                .CreateNode(variables =>
                    new AssignmentNode((string)variables["variableName"], (string)variables["methodName"], ((Token[])variables["arguments"]).Select(t => t.Value).ToArray())
                );
            
            // This group matches characterName "dialogue".
            parser.CreateGroup("Say")
                .Add(new Capture(TokenType.Identifier, "characterName"))
                .Add(new Capture(TokenType.String, "dialogue"))
                .CreateNode(variables => 
                    new SayNode((string)variables["characterName"], (string)variables["dialogue"])    
                );

            parser.CreateGroup("Sprite")
                .Add(new Capture(TokenType.Identifier, "characterName"))
                .Add(new ExpectAndIgnore(TokenType.Keyword, "sprite"))
                .Add(new Capture(TokenType.Integer, "spriteId"))
                .Add(new ExpectAndIgnore(TokenType.NewLine))
                .CreateNode(variables => null);


            
            parser.Parse(tokens);
        }
    }
}