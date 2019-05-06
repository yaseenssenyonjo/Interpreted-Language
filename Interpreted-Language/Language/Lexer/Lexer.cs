using System;
using System.IO;
using System.Text;
using InterpretedLanguage.Language.Lexer.Exceptions;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Language.Lexer
{
    /// <summary>
    /// Represents a lexer.
    /// </summary>
    internal class Lexer
    {
        /// <summary>
        /// The grammar.
        /// </summary>
        private LexicalGrammar _grammar;

        /// <summary>
        /// Creates the grammar.
        /// </summary>
        /// <returns>The lexical grammar.</returns>
        public LexicalGrammar CreateGrammar()
        {
            _grammar = new LexicalGrammar();
            return _grammar;
        }
        
        /// <summary>
        /// Tokenise the input.
        /// </summary>
        /// <param name="input">The input to tokenise.</param>
        /// <returns>The tokens.</returns>
        /// <exception cref="ArgumentNullException">The input is null.</exception>
        public TokenList Tokenise(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentNullException(nameof(input));

            using (var memoryStream = new MemoryStream())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                memoryStream.Write(inputBytes, 0, inputBytes.Length);
                memoryStream.Flush();

                memoryStream.Position = 0;

                return Tokenise(memoryStream);
            }
        }
        
        /// <summary>
        /// Tokenise the stream of input.
        /// </summary>
        /// <param name="inputStream">The stream of input.</param>
        /// <returns>The tokens.</returns>
        /// <exception cref="LexicalException">There are no grammar rules.</exception>
        private TokenList Tokenise(Stream inputStream)
        {
            if (_grammar == null) throw new LexicalException("There are no grammar rules created for the lexer.");

            using (var streamReader = new StreamReader(inputStream))
            {
                var tokens = new TokenList();
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

                    tokens.Add(new Token(ReservedTokens.NewLine, string.Empty, lineNumber++));
                }

                return tokens;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public LexicalRule GetRule(int type)
        {
            return _grammar.GetRule(type);
        }
    }
}