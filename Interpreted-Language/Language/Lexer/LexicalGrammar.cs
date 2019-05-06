using System.Collections;
using System.Collections.Generic;

namespace InterpretedLanguage.Language.Lexer
{
    /// <summary>
    /// Represents the grammar for the lexer.
    /// </summary>
    internal class LexicalGrammar : IEnumerable<LexicalRule>
    {
        /// <summary>
        /// The lexical rules.
        /// </summary>
        private readonly Dictionary<int, LexicalRule> _rules = new Dictionary<int, LexicalRule>();
        
        /// <summary>
        /// Adds a rule using the for the specified type using the specified regex.
        /// </summary>
        /// <param name="type">The type of token.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>This instance.</returns>
        public LexicalGrammar AddRule(int type, string pattern)
        {
            _rules.Add(type, new LexicalRule(type, pattern));
            return this;
        }
        
        /// <summary>
        /// Gets the rule associated with the specified type.
        /// </summary>
        /// <param name="type">The type of token.</param>
        /// <returns></returns>
        public LexicalRule GetRule(int type)
        {
            return _rules[type];
        }
        
        public IEnumerator<LexicalRule> GetEnumerator()
        {
            return _rules.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}