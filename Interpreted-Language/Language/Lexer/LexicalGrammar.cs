using System.Collections;
using System.Collections.Generic;

namespace InterpretedLanguage.Language.Lexer
{
    internal class LexicalGrammar : IEnumerable<LexicalRule>
    {
        private readonly Dictionary<int, LexicalRule> _rules = new Dictionary<int, LexicalRule>();

        public IEnumerator<LexicalRule> GetEnumerator()
        {
            return _rules.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public LexicalGrammar AddRule(int type, string regex)
        {
            _rules.Add(type, new LexicalRule(type, regex));
            return this;
        }

        public LexicalRule GetRule(int type)
        {
            return _rules[type];
        }
    }
}