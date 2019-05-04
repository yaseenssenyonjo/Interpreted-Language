using System;
using System.Collections.Generic;
using InterpretedLanguage.Parser.Groups.Traits;
using InterpretedLanguage.Parser.SyntaxTree.Traits;
using InterpretedLanguage.Tokens;

namespace InterpretedLanguage.Parser.Groups
{
    internal class Group
    {
        private readonly List<IGroupStatement> _statements = new List<IGroupStatement>();
        private readonly Dictionary<string, object> _variables = new Dictionary<string, object>();
        
        private Func<Dictionary<string, object>, int, INode> _createNodeFunc;
        
        public Group Add(IGroupStatement statement)
        {
            _statements.Add(statement);
            return this;
        }

        public Group Add(IGroupPattern pattern)
        {
            pattern.CreateStatements().ForEach(_statements.Add);
            return this;
        }
        
        public void AddVariable(string name, object value)
        {
            _variables.Add(name, value);
        }
        
        public void CreateNode(Func<Dictionary<string, object>, int, INode> createNodeFunc)
        {
            _createNodeFunc = createNodeFunc;
        }
        
        public bool Matches(SyntaxTree.SyntaxTree tree, TokenList tokens)
        {
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