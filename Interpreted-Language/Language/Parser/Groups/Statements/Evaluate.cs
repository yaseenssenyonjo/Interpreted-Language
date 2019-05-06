using System;
using System.Collections.Generic;
using InterpretedLanguage.Language.Parser.Groups.Traits;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Language.Parser.Groups.Statements
{
    public class Evaluate : IGroupStatement
    {
        private readonly Action<Dictionary<string, object>, TokenList> _action;
        private readonly Dictionary<string, object> _variables = new Dictionary<string, object>();

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