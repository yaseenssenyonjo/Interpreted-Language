using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;

namespace Interpreted_Language.RenPy.Nodes
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
        /// The line number for this node.
        /// </summary>
        public int LineNumber { private get; set; }
        
        private static readonly IReadOnlyDictionary<string, Action<AssignmentNode>> MethodNameToAction = new Dictionary<string, Action<AssignmentNode>>
        {
            { "character", CharacterMethod },
            { "room", RoomMethod }
        };

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
        
        // TODO: Implement exceptions here.


        public void Execute(IExecutionContext context)
        {
            if (!MethodNameToAction.TryGetValue(_methodName, out var action)) throw new Exception("..."); // the given method "..." does not exist.
            action(this);
        }
        
        private static void CharacterMethod(AssignmentNode node)
        {
            if (node._methodArguments.Length != 1) throw new Exception($"There was an error on line {node.LineNumber}. The character method got {node._methodArguments.Length} argument, expected 1 argument.");
            var characterName = node._methodArguments[0] as string;
            
            var (hasNullVariable, errorMessage) = IsAnyVariableNull(new[] {typeof(string)}, characterName);
            if(hasNullVariable) throw new Exception($"There was an error on line {node.LineNumber}. {errorMessage}");
            
            // TODO: This would use the execution context (as variables created will be stored there.)
            Console.WriteLine($"{characterName}: Create an instance if needed, then an alias as {node._variableName}.");
            // todo if it already exists throw exception "there is already a variable called '...'"
        }
        
        private static void RoomMethod(AssignmentNode node)
        {
            if (node._methodArguments.Length != 1) throw new Exception($"There was an error on line {node.LineNumber}. The room method got {node._methodArguments.Length} argument, expected 1 argument.");
            var roomName = node._methodArguments[0] as string;

            var (hasNullVariable, errorMessage) = IsAnyVariableNull(new[] {typeof(string)}, roomName);
            if(hasNullVariable) throw new Exception($"There was an error on line {node.LineNumber}. {errorMessage}");
            
            // TODO: This would use the execution context (as variables created will be stored there.)
            Console.WriteLine($"Create a reference to '{roomName}' room if needed an alias as {node._variableName}.");
        }

        private static (bool, string) IsAnyVariableNull(IReadOnlyList<Type> expectedTypes, params object[] variables)
        {
            if(expectedTypes.Count != variables.Length) throw new Exception(); // this is a developer mistake they need to match the expected types.

            for (var i = 0; i < variables.Length; i++)
            {
                if (variables[i] == null) return (true, $"Invalid type given for argument {i + 1}, expected a {expectedTypes[i].Name.ToLower()}.");
            }

            return (false, string.Empty);
        }
    }
}