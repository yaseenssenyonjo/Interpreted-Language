using System.Collections;
using System.Collections.Generic;

namespace Interpreted_Language.Language.Lexer.Tokens
{
    // TODO: Document this class.
    internal class TokenList : IList<Token>
    {
        private readonly List<Token> _tokens = new List<Token>();

        public int Index;
        public int Count => _tokens.Count;
        public bool IsReadOnly => false;

        public Token this[int index]
        {
            get => _tokens[index];
            set => _tokens[index] = value;
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
        /// Removes the token.
        /// </summary>
        /// <param name="token">The token to be removed.</param>
        /// <returns></returns>
        public bool Remove(Token token)
        {
            return _tokens.Remove(token);
        }
        
        /// <summary>
        /// Determines whether the list contains the specified token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Contains(Token token)
        {
            return _tokens.Contains(token);
        }
        
        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear()
        {
            _tokens.Clear();
        }
        
        public void Insert(int index, Token token)
        {
            _tokens.Insert(index, token);
        }
        
        public int IndexOf(Token token)
        {
            return _tokens.IndexOf(token);
        }

        public void RemoveAt(int index)
        {
            _tokens.RemoveAt(index);
        }
        
        public void CopyTo(Token[] array, int arrayIndex)
        {
            _tokens.CopyTo(array, arrayIndex);
        }
        
        /// <summary>
        /// Gets the next token.
        /// </summary>
        /// <returns></returns>
        public Token Next()
        {
            return _tokens[Index++];
        }

        public IEnumerator<Token> GetEnumerator()
        {
            foreach (var token in _tokens) yield return token;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}