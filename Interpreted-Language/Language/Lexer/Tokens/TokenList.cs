using System.Collections;
using System.Collections.Generic;

namespace Interpreted_Language.Language.Lexer.Tokens
{
    // TODO: Consider fully implementing IList to make it a "true" list.
    internal class TokenList : IEnumerable<Token>
    {
        /// <summary>
        /// The tokens.
        /// </summary>
        private readonly List<Token> _tokens = new List<Token>();

        /// <summary>
        /// The current index.
        /// </summary>
        /// <remarks>
        /// This is initialized to -1 because we use pre-incrementation
        /// thus when Next is called the index will be zero.
        /// </remarks>
        public int Index = -1;
        /// <summary>
        /// Gets the number of tokens in list.
        /// </summary>
        public int Count => _tokens.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public Token this[int index] => _tokens[index];
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Token> GetEnumerator()
        {
            for (Index = 0; Index < _tokens.Count; Index++) yield return _tokens[Index];

            // Reset the index after iterating through.
            Index = -1;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds the token.
        /// </summary>
        /// <param name="token">The token to be added.</param>
        public void Add(Token token)
        {
            _tokens.Add(token);
        }

        /// <summary>
        /// Gets the next token.
        /// </summary>
        /// <returns></returns>
        public Token Next()
        {
            return _tokens[++Index];
        }
    }
}