using System;
using System.Collections.Generic;
using InterpretedLanguage.Parser.SyntaxTree;

namespace InterpretedLanguage.Interpreter
{
    internal class Interpreter
    {
        private readonly Stack<SyntaxTree> _executionStack = new Stack<SyntaxTree>();
        
        public void PushRoot(SyntaxTree tree)
        {
            if(_executionStack.Count > 0) _executionStack.Clear();
            _executionStack.Push(tree);
        }
        
        public void Push(SyntaxTree tree)
        {
            _executionStack.Push(tree);
        }
        
        public bool Advance()
        {
            while (true)
            {
                var tree = _executionStack.Peek();
                var state = tree.Advance();
                
                switch (state)
                {
                    case SyntaxTreeState.Incomplete:
                        return true;
                    
                    case SyntaxTreeState.Completed:
                        if (_executionStack.Count > 1)
                        {
                            _executionStack.Pop();
                            if (_executionStack.Count > 0) continue;
                        }
                        return false;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}