using System.Collections;
using System.Collections.Generic;

namespace InterpretedLanguage.Language.Tokens
{
    /// <summary>
    /// Represents a list of tokens.
    /// </summary>
    internal class TokenList : IEnumerable<Token>
    {
        /// <summary>
        /// The underlying list of tokens.
        /// </summary>
        private readonly List<Token> _tokens = new List<Token>();
        /// <summary>
        /// The current index.
        /// </summary>
        private int _index;
        /// <summary>
        /// The screenshot index.
        /// </summary>
        private int _screenshotIndex;
        
        /// <summary>
        /// Gets the token at the specified index.
        /// </summary>
        /// <param name="i">The index of the token.</param>
        public Token this[int i] => _tokens[i];
        
        /// <summary>
        /// Initialises a new instance of the <see cref="TokenList"/> class.
        /// </summary>
        public TokenList()
        {
        }
        
        /// <summary>
        /// Initialises a new instance of the <see cref="TokenList"/> class that contains elements copied form the specified collection.
        /// </summary>
        /// <param name="collection"></param>
        public TokenList(IEnumerable<Token> collection)
        {
            collection.ForEach(token => _tokens.Add(token));
        }

        /// <summary>
        /// Adds the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        public void Add(Token token)
        {
            _tokens.Add(token);
        }

        /// <summary>
        /// Advances to the next token.
        /// </summary>
        /// <returns>The current token.</returns>
        public Token Advance()
        {
            return _tokens[_index++];
        }

        /// <summary>
        /// Returns the next token.
        /// </summary>
        /// <returns>The next token</returns>
        public Token Peek()
        {
            return _tokens[_index];
        }

        /// <summary>
        /// Returns the previous token.
        /// </summary>
        /// <returns>The previous token.</returns>
        public Token Previous()
        {
            return _tokens[_index - 1];
        }

        /// <summary>
        /// Takes a screenshot.
        /// </summary>
        public void CreateScreenshot()
        {
            _screenshotIndex = _index;
        }
        
        /// <summary>
        /// Rollbacks to the screenshot.
        /// </summary>
        public void RollbackScreenshot()
        {
            _index = _screenshotIndex;
        }
        
        /// <summary>
        /// Gets a value that indicates whether the current stream position is at the end of the stream.
        /// </summary>
        /// <returns>true, if the current stream position is at the end of the stream; otherwise, false.</returns>
        public bool EndOfStream()
        {
            return _index >= _tokens.Count;
        }
        
        public IEnumerator<Token> GetEnumerator()
        {
            return _tokens.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}