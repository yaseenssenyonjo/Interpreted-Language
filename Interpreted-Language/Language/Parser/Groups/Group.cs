using System;
using System.Collections.Generic;
using InterpretedLanguage.Language.Parser.Groups.Traits;
using InterpretedLanguage.Language.Parser.SyntaxTree.Traits;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Language.Parser.Groups
{
    /// <summary>
    /// Represents a group.
    /// </summary>
    internal class Group
    {
        /// <summary>
        /// The statements.
        /// </summary>
        private readonly List<IGroupStatement> _statements = new List<IGroupStatement>();
        /// <summary>
        /// The variables.
        /// </summary>
        private readonly Dictionary<string, object> _variables = new Dictionary<string, object>();

        /// <summary>
        /// The node create function.
        /// </summary>
        private Func<Dictionary<string, object>, int, INode> _createNodeFunc;

        /// <summary>
        /// Adds the specified statement.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns>This instance.</returns>
        public Group Add(IGroupStatement statement)
        {
            _statements.Add(statement);
            return this;
        }

        /// <summary>
        /// Adds the specified pattern.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <returns>This instance.</returns>
        public Group Add(IGroupPattern pattern)
        {
            pattern.GetStatements().ForEach(_statements.Add);
            return this;
        }

        /// <summary>
        /// Adds a variable with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        public void AddVariable(string name, object value)
        {
            _variables.Add(name, value);
        }

        /// <summary>
        /// Sets the node create function.
        /// </summary>
        /// <param name="func"></param>
        public void CreateNode(Func<Dictionary<string, object>, int, INode> func)
        {
            _createNodeFunc = func;
        }

        /// <summary>
        /// Matches the specified tokens.
        /// </summary>
        /// <param name="tree">The abstract syntax tree.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>true, if this group matches the tokens; otherwise, false</returns>
        public bool Matches(SyntaxTree.SyntaxTree tree, TokenList tokens)
        {
            _variables.Clear();

            foreach (var statement in _statements)
            {
                if (statement.Match(this, tokens)) continue;
                return false;
            }

            if (_createNodeFunc != null)
            {
                var lineNumber = (!tokens.EndOfStream() ? tokens.Peek().LineNumber : tokens.Previous().LineNumber) - 1;
                var node = _createNodeFunc.Invoke(_variables, lineNumber);
                tree.Add(node);
            }

            return true;
        }
    }
}