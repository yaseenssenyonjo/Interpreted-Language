using Interpreted_Language.Language.Lexer.Tokens;
using Interpreted_Language.Language.Parser.Groups.Statements.Traits;

namespace Interpreted_Language.Language.Parser.Groups.Statements
{
    internal class Capture : IStatement
    {
        /// <summary>
        /// The type of token to be captured.
        /// </summary>
        private readonly TokenType _tokenType;
        /// <summary>
        /// The name to associate the captured value with.
        /// </summary>
        private readonly string _name;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        public Capture(TokenType type, string name)
        {
            _tokenType = type;
            _name = name;
        }

        public bool Evaluate(Group group, TokenList tokens)
        {
            // Get the next token.
            var token = tokens.Next();
            // If the token type is not the expected result, return false.
            if (token.Type != _tokenType) return false;
            
            group.AddVariable(_name, token.Value);
            return true;
        }
    }
}