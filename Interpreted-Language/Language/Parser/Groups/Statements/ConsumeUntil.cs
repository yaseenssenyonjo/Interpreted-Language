using System.Collections.Generic;
using Interpreted_Language.Language.Lexer.Tokens;
using Interpreted_Language.Language.Parser.Groups.Statements.Traits;

namespace Interpreted_Language.Language.Parser.Groups.Statements
{
    /// <summary>
    /// Consumes until a specific token is found.
    /// </summary>
    internal class ConsumeUntil : IStatement
    {
        /// <summary>
        /// The type of token to end the consumption of tokens.
        /// </summary>
        private readonly TokenType _tokenType;
        /// <summary>
        /// The expected value of the token.
        /// </summary>
        private readonly object _expectedValue;
        /// <summary>
        /// The name of associate the consumed tokens with.
        /// </summary>
        private readonly string _name;
        
        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.Language.Parser.Groups.Statements.ConsumeUntil"/> class.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expectedValue"></param>
        /// <param name="name"></param>
        public ConsumeUntil(TokenType type, object expectedValue, string name)
        {
            _tokenType = type;
            _expectedValue = expectedValue;
            _name = name;
        }
        
        /// <summary>
        /// Determines whether the tokens can be consumed until a specific token is found.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns><c>true</c> if it encounters the expected token before the end of a line; otherwise, <c>false</c></returns>
        public bool Evaluate(Group group, TokenList tokens)
        {
            var consumedTokens = new List<Token>();
            
            for (; tokens.Index < tokens.Count; tokens.Index++)
            {
                var token = tokens[tokens.Index];
                // If we start a new line and still haven't encountered the expected token, return false.
                if (token.Type == TokenType.NewLine) return false;
                // If we encounter the expected token, consume it and break out of the loop.
                if (token.Type == _tokenType && token.Value.Equals(_expectedValue))
                {
                    // Skip over the expected token thus consuming it.
                    tokens.Next();
                    break;
                }
                
                // Otherwise, add the token to the list of consumed tokens.
                consumedTokens.Add(token);
            }
            
            group.AddVariable(_name, consumedTokens.ToArray());
            return true;
        }
    }
}