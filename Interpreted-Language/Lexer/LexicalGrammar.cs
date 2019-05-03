using System.Collections;
using System.Collections.Generic;
using InterpretedLanguage.Tokens;

namespace InterpretedLanguage.Lexer
{
    internal class LexicalGrammar : IEnumerable<LexicalRule>
    {
        private readonly string _name;
        private readonly List<LexicalRule> _rules = new List<LexicalRule>();

        public LexicalGrammar(string name)
        {
            _name = name;
        }
        
        public LexicalGrammar AddRule(TokenType type, string regex)
        {
            _rules.Add(new LexicalRule(type, regex));
            return this;
        }

        public IEnumerator<LexicalRule> GetEnumerator()
        {
            return _rules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}