using System;
using InterpretedLanguage.Lexer;
using InterpretedLanguage.Parser;
using InterpretedLanguage.Parser.Groups.Statements;
using InterpretedLanguage.Parser.SyntaxTree;
using InterpretedLanguage.Parser.SyntaxTree.Traits;
using InterpretedLanguage.Tokens;

namespace InterpretedLanguage
{
    internal class DummyNode : INode
    {
        private readonly string _name;
        
        public DummyNode(string name)
        {
            _name = name;
        }
        
        public AdvancementType Execute(SyntaxTree tree)
        {
            Console.WriteLine(_name);
            return AdvancementType.Continue;
        }
    }
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            var lexer = new Lexer.Lexer();
            lexer.CreateGrammar(string.Empty)
                .AddRule(TokenType.Identifier, "(.+)");
            var tokens = lexer.Tokenise("hey");
            
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
            
            var parser = new Parser.Parser();
            
            parser.CreateGroup("New Lines")
                .Add(new ExpectAndIgnore(TokenType.NewLine));
            
            parser.CreateGroup(string.Empty)
                .Add(new Capture(TokenType.Identifier, "name"))
                .CreateNode((variables, lineNumber) => new DummyNode((string)variables["name"]));
            
            var tree = new SyntaxTree();
            parser.Parse(tree, tokens);
        }
    }
}