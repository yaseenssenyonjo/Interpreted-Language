using System;
using System.Collections.Generic;
using InterpretedLanguage.Parser.Groups.Traits;
using InterpretedLanguage.Tokens;

namespace InterpretedLanguage.Parser.Groups.Statements
{
    internal class Evaluate : IGroupStatement
    {
        private readonly Dictionary<string, object> _variables = new Dictionary<string, object>();
        private readonly Action<Dictionary<string, object>, TokenList> _action;
        
        public Evaluate(Action<Dictionary<string, object>, TokenList> action)
        {
            _action = action;
        }

        public bool Match(Group group, TokenList tokens)
        {
            _variables.Clear();
            _action.Invoke(_variables, tokens);
            _variables.ForEach(variable => group.AddVariable(variable.Key, variable.Value));
            
            return true;
        }
    }
}