using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;

namespace Interpreted_Language.Language.Parser.Syntax.Nodes
{
    internal class AssignmentNode : INode
    {
        /// <summary>
        /// The name of the variable.
        /// </summary>
        private readonly string _variableName;
        /// <summary>
        /// The name of the method.
        /// </summary>
        private readonly string _methodName;
        /// <summary>
        /// The arguments for the method.
        /// </summary>
        private readonly object[] _methodArguments;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="methodArguments">The arguments for the method.</param>
        public AssignmentNode(string variableName, string methodName, object[] methodArguments)
        {
            _variableName = variableName;
            _methodName = methodName;
            _methodArguments = methodArguments;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}