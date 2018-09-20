using System;
using System.IO;
using System.Linq;
using Interpreted_Language.Language.Interpreter;
using Interpreted_Language.Language.Lexer;
using Interpreted_Language.Language.Lexer.Tokens;
using Interpreted_Language.Language.Lexer.Grammar.Traits;
using Interpreted_Language.Language.Parser;
using Interpreted_Language.Language.Parser.Groups.Statements;
using Interpreted_Language.Language.Parser.Syntax;
using Interpreted_Language.RenPy.Grammar;
using Interpreted_Language.RenPy.Interpreter;
using Interpreted_Language.RenPy.Nodes;

namespace Interpreted_Language
{
    internal static class Program
    {
        public static void Main()
        {
            var tokens = Tokenise(new RenPyGrammar(), "./Tests/RenPy/Data/script.rpy");
            
            foreach (var token in tokens)
            {
                // Console.WriteLine($"{token.Type} | {token.Value} | {token.LineNumber}");
            }

            var syntaxTree = Parse(tokens);

            Execute(syntaxTree);
        }
        
        private static TokenList Tokenise(IGrammar grammar, string path)
        {           
            var lexer = new Lexer(grammar);
            var tokens = lexer.Tokenise(File.ReadAllText(path));

            return tokens;
        }
        
        private static SyntaxTree Parse(TokenList tokens)
        {
            var parser = new Parser();
            
            // This group matches any line that just has a new line.
            parser.CreateGroup("New Lines")
                .Add(new ExpectAndIgnore(TokenType.NewLine));
            
            // This group matches variableName = methodName(arguments).
            parser.CreateGroup("Assignment")
                .Add(new Capture(TokenType.Identifier, "variableName"))
                .Add(new ExpectAndIgnore(TokenType.Equal))
                .Add(new Capture(TokenType.Keyword, "methodName"))
                .Add(new ExpectAndIgnore(TokenType.Punctuation, "("))
                .Add(new ConsumeUntil(TokenType.Punctuation, ")", "arguments"))
                .Add(new ExpectAndIgnore(TokenType.NewLine))
                .CreateNode(variables => new AssignmentNode((string) variables["variableName"], (string) variables["methodName"], ((Token[])variables["arguments"]).Select(t => t.Value).ToArray()));
            
            // This group matches label name: and anything indented within it.
            parser.CreateGroup("Label")
                .Add(new ExpectAndIgnore(TokenType.Keyword, "label"))
                .Add(new Capture(TokenType.Identifier, "labelName"))
                .Add(new ExpectAndIgnore(TokenType.Punctuation, ":"))
                .Add(new ExpectAndIgnore(TokenType.NewLine))
                // This conditional loop keeps consuming tokens as long as it begins with a tab.
                .Add(new ConditionalLoop(t => t.Index < t.Count && t.Next().Type == TokenType.Tab, conditionalLoop =>
                {
                    // Gets the existing instance, otherwise creates it if it doesn't exist.
                    var consumedTokens = (TokenList)conditionalLoop.Get("consumedTokens", new TokenList());
                    
                    for (; tokens.Index < tokens.Count; tokens.Index++)
                    {
                        var token = tokens[tokens.Index];
                        
                        // Add the token to the list of consumed tokens.
                        consumedTokens.Add(token);
                        
                        // If we aren't starting a new line, keep consuming tokens.
                        if (token.Type != TokenType.NewLine) continue;
                        
                        // Otherwise, break out of the loop so that the condition can be checked.
                        // Go to the next token before we break out of the loop thus consuming the current token (the new line token).
                        tokens.Next();
                        break;
                    }
                }))
                // Recursively parses the consumed tokens into a syntax tree.
                .CreateNode(variables => new LabelNode((string)variables["labelName"], parser.Parse((TokenList)variables["consumedTokens"])));
            
            // This group matches characterName "dialogue".
            parser.CreateGroup("Say")
                .Add(new Capture(TokenType.Identifier, "characterName"))
                .Add(new Capture(TokenType.String, "dialogue"))
                .CreateNode(variables => new SayNode((string)variables["characterName"], (string)variables["dialogue"]));
            
            // This group matches characterName sprite 00
            parser.CreateGroup("Sprite")
                .Add(new Capture(TokenType.Identifier, "characterName"))
                .Add(new ExpectAndIgnore(TokenType.Keyword, "sprite"))
                .Add(new Capture(TokenType.Integer, "spriteId"))
                .Add(new ExpectAndIgnore(TokenType.NewLine))
                .CreateNode(variables => new SpriteNode((string)variables["characterName"], (int)variables["spriteId"]));
            
            var syntaxTree = parser.Parse(tokens);

            return syntaxTree;
        }

        private static void Execute(SyntaxTree syntaxTree)
        {
            var executionContext = new RenPyExecutionContext();
            var interpreter = new Interpreter(executionContext);
            interpreter.Execute(syntaxTree);
        }
    }
}

// TODO: Unit Tests.
    // - Lexer creates expected tokens.
    // - Syntax Tree parses tokens correctly (count and label)
    // - Throws an exception for unexpected variable types