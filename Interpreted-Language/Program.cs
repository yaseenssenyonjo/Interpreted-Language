using System;
using System.IO;
using Interpreted_Language.Language.Lexer;
using Interpreted_Language.Language.Lexer.Grammar;
using Interpreted_Language.Language.Lexer.Grammar.Traits;

namespace Interpreted_Language
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Tokenise(new RenPyGrammar(), "./Data/script.rpy");
        }

        private static void Tokenise(IGrammar grammar, string path)
        {
            Console.WriteLine($"{grammar.Name} Lexer\n");
            
            var lexer = new Lexer(grammar);
            var tokens = lexer.Tokenise(File.ReadAllText(path));

            foreach (var token in tokens)
            {
                Console.WriteLine($"{token.Type} | {token.Value}");
            }
        }
    }
}