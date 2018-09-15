using System;
using System.Collections.Generic;
using Interpreted_Language.Language.Lexer.Tokens;
using Interpreted_Language.Language.Parser.Groups.Statements.Traits;
using Interpreted_Language.Language.Parser.SyntaxTree.Nodes.Traits;

namespace Interpreted_Language.Language.Parser.Groups
{
    /// <summary>
    /// 
    /// </summary>
    internal class Group
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly List<IStatement> _statements = new List<IStatement>();
        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<string, object> _variables = new Dictionary<string, object>();
        /// <summary>
        /// 
        /// </summary>
        private Func<Dictionary<string, object>, INode> _createNodeFunc;

        /// <summary>
        /// Adds the statement to the group.
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        public Group Add(IStatement statement)
        {
            _statements.Add(statement);
            return this;
        }

        /// <summary>
        /// Adds the variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        public void AddVariable(string name, object value)
        {
            _variables.Add(name, value);
        }
        
        /// <summary>
        /// Creates the node.
        /// </summary>
        /// <param name="func">The function that creates the node.</param>
        public void CreateNode(Func<Dictionary<string, object>, INode> func)
        {
            _createNodeFunc = func;
        }
        
        /// <summary>
        /// Evaluates this group of statements.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns><c>true</c> if all the statements evaluate to true, otherwise <c>false</c>.</returns>
        public bool Evaluate(TokenList tokens)
        {
            var index = tokens.Index;
            foreach (var statement in _statements)
            {
                // If the statement evaluated successfully, continue to the next statement in the loop.
                if (statement.Evaluate(this, tokens)) continue;
                
                // Otherwise, revert back to initial index otherwise we end up skipping over tokens.
                tokens.Index = index;
                    
                // There is a possibility that the tokens partially match the group and without
                // reverting those tokens get skipped.
                    
                // Clear all the created variables as this group failed to match the tokens
                // and to ensure that this group can be reused.
                _variables.Clear();
                    
                return false;
            }

            if (_createNodeFunc != null)
            {
                // Construct the node.
                var node = _createNodeFunc.Invoke(_variables);
                Console.WriteLine(node);
                // TODO: Tell the syntax tree to add the node.
            }
            
            // Clear all the variables so this group can be reused without issue.
            _variables.Clear();
            
            return true;
        }
    }
}