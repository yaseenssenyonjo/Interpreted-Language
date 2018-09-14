using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Interpreted_Language.Language.Lexer.Tokens;
using Interpreted_Language.Language.Lexer.Exceptions;
using Interpreted_Language.Language.Lexer.Grammar.Traits;

namespace Interpreted_Language.Language.Lexer
{
    internal class Lexer
    {
        /// <summary>
        /// The grammar.
        /// </summary>
        private readonly IGrammar _grammar;
        private static readonly Regex WhitespaceRegex = new Regex($@"((\r|\t|\v|\f| )*(?<NewLine>({Environment.NewLine}|\n)+)?)+", RegexOptions.Compiled);
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Interpreted_Language.Language.Lexer.Lexer"/> class.
        /// </summary>
        /// <param name="grammar">The grammar rules.</param>
        public Lexer(IGrammar grammar)
        {
            _grammar = grammar;
        }

        /// <summary>
        /// Tokenise the input.
        /// </summary>
        /// <param name="input">The string to tokenise. If input is null, an exception is thrown.</param>
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
                
                // After writing to the stream, the write/read head is positioned at the end of the stream.
                // This means that when attempting to read from the stream it will fail.
                // So we set the stream position to beginning.
                memoryStream.Position = 0;
                
                return Tokenise(memoryStream);
            }
        }
        
        /// <summary>
        /// Tokenise the input in the stream.
        /// </summary>
        /// <param name="inputStream">The stream to read the input from.</param>
        /// <returns>The tokens.</returns>
        /// <exception cref="ArgumentNullException">The line is null.</exception>
        /// <exception cref="CaptureGroupException">There is no capture group in a rule.</exception>
        private TokenList Tokenise(Stream inputStream)
        {
            var tokens = new TokenList();

            using (var streamReader = new StreamReader(inputStream))
            {
                var lineNumber = 0;

                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine(); lineNumber++;
                    if (line == null) throw new ArgumentNullException(nameof(line));
                    
                    var startIndex = 0;
                    
                    while (startIndex < line.Length)
                    {
                        LexicalRule lexicalRule = null;
                        var matchValue = string.Empty;
                        var matchLength = 0;
                        
                        foreach (var rule in _grammar.Rules)
                        {
                            var match = rule.RegularExpression.Match(line.Substring(startIndex));
                            // If the token doesn't match the grammar rule, skip to the next rule.
                            if (!match.Success) continue;
                            
                            // All regular expressions should have at least a single capture group, if they don't throw an exception.
                            if (match.Groups.Count == 1) throw new CaptureGroupException($"{_grammar.Name}: Lexical rule '{rule.TokenType.ToString().ToLower()}' is missing a capture group.");

                            lexicalRule = rule;
                            matchValue = match.Groups[0].Value;
                            matchLength = match.Length;
                            break;
                        }
                        
                        // TODO: Rider uses spaces when indenting the "script.rpy" file which has highlighted the inability for 4 spaces to be handled as tabs which needs to be resolved.
                        
                        // This means that it failed to find a rule for the remaining line, so throw an exception.
                        if (lexicalRule == null) throw new LexerException($"{_grammar.Name}: Failed to find a lexical rule that matches line {lineNumber}.");
                        // If the token isn't ignored, add it to the list.
                        if (!lexicalRule.IsIgnored) tokens.Add(new Token(lexicalRule.TokenType, matchValue, lineNumber));
                        
                        var whitespace = WhitespaceRegex.Match(line, startIndex + matchLength);
                        if (whitespace.Success && whitespace.Length > 0) matchLength += whitespace.Length;

                        startIndex += matchLength;
                    }
                    
                    tokens.Add(new Token(TokenType.NewLine, string.Empty, lineNumber));
                }
            }
            
            return tokens;
        }
    }
}