using System.Collections;
using System.Collections.Generic;

namespace Interpreted_Language.Language.Lexer.Tokens
{
    /// <summary>
    /// Represents a list of tokens.
    /// </summary>
    internal class TokenList : IList<Token>
    {
        /// <summary>
        /// The underlying token list.
        /// </summary>
        private readonly List<Token> _tokens = new List<Token>();
        
        /// <summary>
        /// The current index.
        /// </summary>
        public int Index;
        /// <summary>
        /// Gets the number of elements contained in this list.
        /// </summary>
        public int Count => _tokens.Count;
        /// <summary>
        /// Gets a value indicating whether the IList is read-only.
        /// </summary>
        /// <remarks>This will always return true.</remarks>
        public bool IsReadOnly => true;
        
        /// <summary>
        /// Gets or sets the token at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the token to get or set.</param>
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
        /// <returns><c>true</c> if token is found in the list; otherwise, <c>false</c></returns>
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
        
        /// <summary>
        /// Inserts an element into the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which token should be inserted.</param>
        /// <param name="token">The token to insert. </param>
        public void Insert(int index, Token token)
        {
            _tokens.Insert(index, token);
        }
        
        /// <summary>
        /// Searches for the specified token and returns the zero-based index of the first occurrence within the list.
        /// </summary>
        /// <param name="token">The token to locate in the list.</param>
        /// <returns>The zero-based index of the first occurrence of item within the entire list, if found; otherwise, –1.</returns>
        public int IndexOf(Token token)
        {
            return _tokens.IndexOf(token);
        }
        
        /// <summary>
        /// Removes the element at the specified index of the list.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public void RemoveAt(int index)
        {
            _tokens.RemoveAt(index);
        }
        
        /// <summary>
        /// Copies the entire list to a compatible one-dimensional array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from this list.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(Token[] array, int arrayIndex)
        {
            _tokens.CopyTo(array, arrayIndex);
        }
        
        /// <summary>
        /// Gets the next token.
        /// </summary>
        /// <returns>The next token.</returns>
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