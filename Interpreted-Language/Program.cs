using System;
using InterpretedLanguage.Tokens;

namespace InterpretedLanguage
{
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
        }
    }
}