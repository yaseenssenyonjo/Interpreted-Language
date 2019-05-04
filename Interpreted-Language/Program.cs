using System;
using InterpretedLanguage.Lexer;
using InterpretedLanguage.Parser;
using InterpretedLanguage.Parser.Groups.Statements;
using InterpretedLanguage.Parser.SyntaxTree;
using InterpretedLanguage.Parser.SyntaxTree.Traits;
using InterpretedLanguage.Tokens;

namespace InterpretedLanguage
{    
    internal class MyEnvironment : SyntaxTreeEnvironment
    {
        public const string SecretValue = "!";
    }
    
    internal class DummyNode : INode
    {
        private readonly string _name;
        
        public DummyNode(string name)
        {
            _name = name;
        }
        
        public AdvancementType Execute(SyntaxTree tree)
        {
            var environment = (MyEnvironment) tree.Environment;
            
            Console.WriteLine(_name);
            Console.WriteLine(MyEnvironment.SecretValue);
            
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
            
            var tree = new SyntaxTree(new MyEnvironment());
            parser.Parse(tree, tokens);
            
            var interpreter = new Interpreter.Interpreter();
            interpreter.PushRoot(tree);
            while(interpreter.Advance()) {}
        }
    }
}