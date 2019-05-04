using System.Collections;
using System.Collections.Generic;

namespace InterpretedLanguage.Tokens
{
    internal class TokenList : IEnumerable<Token>
    {
        private readonly List<Token> _tokens = new List<Token>();
        private int _index;
        private int _screenshotIndex;
        
        public int Count => _tokens.Count;

        public Token this[int i] => _tokens[i];

        public TokenList()
        {
        }
        
        public TokenList(IEnumerable<Token> collection)
        {
            collection.ForEach(token => _tokens.Add(token));
        }
        
        public void Add(Token token)
        {
            _tokens.Add(token);
        }

        public Token Advance()
        {
            return _tokens[_index++];
        }
        
        public Token Peek()
        {
            return _tokens[_index];
        }

        public Token Previous()
        {
            return _tokens[_index - 1];
        }

        public void CreateScreenshot()
        {
            _screenshotIndex = _index;
        }

        public void RollbackScreenshot()
        {
            _index = _screenshotIndex;
        }

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