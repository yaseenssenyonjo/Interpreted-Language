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
        
        public void Add(Token token)
        {
            _tokens.Add(token);
        }

        public bool Remove(Token token)
        {
            return _tokens.Remove(token);
        }
        
        public bool Contains(Token token)
        {
            return _tokens.Contains(token);
        }
        
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

        public Token Next()
        {
            return _tokens[Index++];
        }

        public IEnumerator<Token> GetEnumerator()
        {
            for (Index = 0; Index < Count; Index++) yield return _tokens[Index];
            Index = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}