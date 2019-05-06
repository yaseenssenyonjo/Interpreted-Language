using System;
using System.Collections.Generic;
using InterpretedLanguage.Language.Parser.SyntaxTree;

namespace InterpretedLanguage.Language.Interpreter
{
    /// <summary>
    /// Represents an interpreter.
    /// </summary>
    public class Interpreter
    {
        /// <summary>
        /// The execution stack.
        /// </summary>
        private readonly Stack<SyntaxTree> _executionStack = new Stack<SyntaxTree>();

        /// <summary>
        /// Pushes the specified tree to the bottom of the execution stack.
        /// </summary>
        /// <param name="tree">The abstract syntax tree.</param>
        public void PushRoot(SyntaxTree tree)
        {
            if (_executionStack.Count > 0) _executionStack.Clear();
            _executionStack.Push(tree);
        }

        /// <summary>
        /// Pushes the specified tree to the top of the execution stack.
        /// </summary>
        /// <param name="tree">The abstract syntax tree.</param>
        public void Push(SyntaxTree tree)
        {
            _executionStack.Push(tree);
        }

        /// <summary>
        /// Advances through the tree.
        /// </summary>
        /// <returns>true, if execution is not completed; otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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
                        // Preserve the root tree in the stack.
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