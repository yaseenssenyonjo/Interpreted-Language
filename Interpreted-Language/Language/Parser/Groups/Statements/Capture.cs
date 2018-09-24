using Interpreted_Language.Language.Lexer.Tokens;
using Interpreted_Language.Language.Parser.Groups.Statements.Traits;

namespace Interpreted_Language.Language.Parser.Groups.Statements
{
    /// <summary>
    /// Finds and captures a specific token.
    /// </summary>
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
        /// Initialises a new instance of the <see cref="Interpreted_Language.Language.Parser.Groups.Statements.Capture"/> class.
        /// </summary>
        /// <param name="type">The type of token to be captured.</param>
        /// <param name="name">The name to associate the captured value with.</param>
        public Capture(TokenType type, string name)
        {
            _tokenType = type;
            _name = name;
        }
        
        /// <summary>
        /// Determines whether a specific token can be found and captured.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns></returns>
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