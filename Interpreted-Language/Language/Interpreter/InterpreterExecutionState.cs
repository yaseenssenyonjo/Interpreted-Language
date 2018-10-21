namespace Interpreted_Language.Language.Interpreter
{
    /// <summary>
    /// Represents the state of the interpreter.
    /// </summary>
    internal enum InterpreterExecutionState
    {
        /// <summary>
        /// The execution of the interpreter has completed.
        /// </summary>
        Completed,
        
        /// <summary>
        /// The execution of the interpreter has halted.
        /// </summary>
        /// <remarks>This only occurs when a blocking statement is encountered.</remarks>
        Halted
    }
}