using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using InterpretedLanguage.Lexer.Exceptions;
using InterpretedLanguage.Tokens;

namespace InterpretedLanguage.Lexer
{
    internal class Lexer
    {
        private LexicalGrammar _grammar;

        public LexicalGrammar CreateGrammar(string name)
        {
            _grammar = new LexicalGrammar(name);
            return _grammar;
        }

        public List<Token> Tokenise(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            using (var memoryStream = new MemoryStream())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                memoryStream.Write(inputBytes, 0, inputBytes.Length);
                memoryStream.Flush();

                memoryStream.Position = 0;
                
                return Tokenise(memoryStream);
            }
        }

        private List<Token> Tokenise(Stream inputStream)
        {
            if (_grammar == null)
            {
                throw new LexicalException("There are no grammar rules created for the lexer.");
            }
            
            using (var streamReader = new StreamReader(inputStream))
            {
                var tokens = new List<Token>();
                var lineNumber = 1;
                
                while (!streamReader.EndOfStream)
                {
                    var line = new LexicalLine(streamReader.ReadLine(), lineNumber);
                    
                    while (line.Length > 0)
                    {
                        Token token = null;
                        
                        foreach (var rule in _grammar)
                        {
                            if (!rule.TryCreateToken(ref line, out token)) continue;
                            tokens.Add(token);
                            break;
                        }

                        if (token == null)
                        {
                            throw new LexicalException($"There are no rules that match line {lineNumber}.");
                        }
                    }
                    
                    tokens.Add(new Token(TokenType.NewLine, string.Empty, lineNumber++));
                }

                return tokens;
            }
        }
    }
}