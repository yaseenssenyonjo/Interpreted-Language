using System;
using System.Collections.Generic;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax;
using Interpreted_Language.Language.Parser.Syntax.Nodes;

namespace Interpreted_Language.Language.Interpreter
{
    /// <summary>
    /// Represents an interpreter.
    /// </summary>
    internal class Interpreter
    {
        /// <summary>
        /// The execution context for this interpreter.
        /// </summary>
        private readonly IExecutionContext _context;

        /// <summary>
        /// The current syntax tree.
        /// </summary>
        private SyntaxTree _syntaxTree;
        /// <summary>
        /// The current index.
        /// </summary>
        private int _currentIndex;
        
        /// <summary>
        /// The halted syntax trees.
        /// </summary>
        private readonly Stack<HaltedSyntaxTree> _haltedSyntaxTrees = new Stack<HaltedSyntaxTree>();
        
        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.Language.Interpreter.Interpreter"/> class.
        /// </summary>
        /// <param name="context">The execution context for this interpreter.</param>
        public Interpreter(IExecutionContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Pushes the syntax tree to the execution stack and pauses the active syntax tree.
        /// </summary>
        /// <param name="syntaxTree">The syntax tree to execute.</param>
        public void Push(SyntaxTree syntaxTree)
        {
            if (_syntaxTree != null)
            {
                // Push the executing syntax tree.
                // Increment the current index to prevent it from executing the same node when
                // resumed.
                _haltedSyntaxTrees.Push(new HaltedSyntaxTree(_syntaxTree, _currentIndex + 1));
                // This will be pre-incremented thus this will be zero.
                _currentIndex = -1; 
            }
            
            _syntaxTree = syntaxTree;
        }
        
        /// <summary>
        /// Executes the specified syntax tree.
        /// </summary>
        /// <returns>The state of the interpreter.</returns>
        public InterpreterExecutionState Execute()
        {
            if(_syntaxTree == null) throw new Exception("Failed to execute. There is no syntax tree.");
            
            while (true)
            {
                for (; _currentIndex < _syntaxTree.Count; _currentIndex++)
                {
                    var node = _syntaxTree[_currentIndex];
                
                    if (node.Execute(_context) == BlockingType.Blocking)
                    {
                        // Increment the index so it executes the next node when next called.
                        _currentIndex++;
                        return InterpreterExecutionState.Halted;
                    }
                }
                
                if (_haltedSyntaxTrees.Count > 0)
                {
                    var haltedSyntaxTree = _haltedSyntaxTrees.Pop();
                    _syntaxTree = haltedSyntaxTree.Tree;
                    _currentIndex = haltedSyntaxTree.Index;
                    continue;
                }

                break;
            }
           
            // Reset the index.
            _currentIndex = 0;
            return InterpreterExecutionState.Completed;
        }
    }
}