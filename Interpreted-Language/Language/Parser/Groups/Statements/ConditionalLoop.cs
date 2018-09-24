using System;
using System.Collections.Generic;
using Interpreted_Language.Language.Lexer.Tokens;
using Interpreted_Language.Language.Parser.Groups.Statements.Traits;

namespace Interpreted_Language.Language.Parser.Groups.Statements
{
    /// <summary>
    /// Represents a loop that executes until the condition is not satisfied.
    /// </summary>
    internal class ConditionalLoop : IStatement
    {
        private readonly Predicate<TokenList> _condition;
        private readonly Action<ConditionalLoop> _function;
        private readonly Dictionary<string, object> _variables = new Dictionary<string, object>();
        
        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.Language.Parser.Groups.Statements.ConditionalLoop"/> class.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="function">The function to execute if the condition is true.</param>
        public ConditionalLoop(Predicate<TokenList> condition, Action<ConditionalLoop> function)
        {
            _condition = condition;
            _function = function;
        }
        
        /// <summary>
        /// Determines whether the loop can execute.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns><c>true</c> if the loop has executed, otherwise <c>false</c></returns>
        public bool Evaluate(Group group, TokenList tokens)
        {
            var isConditionSatisfied = _condition.Invoke(tokens);
            // If the condition is not satisfied the first time it is checked, return false. 
            if (!isConditionSatisfied) return false;
            
            // Keep calling the function until the condition is no longer satisfied.
            while (isConditionSatisfied)
            {
                _function(this);
                isConditionSatisfied = _condition.Invoke(tokens);
            }
            
            // Transfer the "local" variables to the group.
            foreach (var variable in _variables) group.AddVariable(variable.Key, variable.Value);
            
            // Clear the variables.
            _variables.Clear();

            return true;
        }
        
        /// <summary>
        /// Gets the value associated with the specified name.
        /// </summary>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="variableValue">The value to default to if the variable doesn't exist.</param>
        /// <returns>The value associated with the specified name.</returns>
        public object Get(string variableName, object variableValue)
        {
            if (_variables.TryGetValue(variableName, out var value)) return value;
            _variables.Add(variableName, variableValue);
            return variableValue;
        }
    }
}