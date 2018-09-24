using System;
using System.Collections.Generic;
using System.Linq;
using Interpreted_Language.Language.Lexer.Tokens;
using Interpreted_Language.Language.Parser.Groups.Statements.Traits;
using Interpreted_Language.Language.Parser.Syntax;
using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;

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
        /// Does this group create a node.
        /// </summary>
        private bool _doesCreateNode;

        public Group(bool doesCreateNode)
        {
            _doesCreateNode = doesCreateNode;
        }

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
        /// <param name="syntaxTree">The syntax tree to add the nodes to.</param>
        /// <param name="tokens"></param>
        /// <returns><c>true</c> if all the statements evaluate to true, otherwise <c>false</c>.</returns>
        public bool Evaluate(SyntaxTree syntaxTree, TokenList tokens)
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
            
            // Create a copy of the variables dictionary.
            var variablesCopy = _variables.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            
            // Clear all the variables before creating nodes.
            // This allows for recursive parsing without throwing exceptions about keys
            // being in use as the variables dictionary has already been cleared.
            _variables.Clear();
            
            // If this group doesn't create a node return.
            if (!_doesCreateNode) return true;
            
            // If this group doesn't have a create node function throw an exception.
            if (_createNodeFunc == null) throw new Exception(); // todo: warn programmer that a group doesn't create a node. if this is intended behaviour tell them they to explicitly state that.
            
            // Construct the node.
            var node = _createNodeFunc(variablesCopy);
            // Set the line number.
            node.LineNumber = tokens[tokens.Index - 1].LineNumber; // gets the last token which will always be the new line. hacky implementation. TODO: try improve it.
            // Add the node to the syntax tree.
            syntaxTree.Add(node);

            return true;
        }
    }
}