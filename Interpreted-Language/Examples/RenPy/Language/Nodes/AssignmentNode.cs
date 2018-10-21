using System;
using System.Collections.Generic;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.Language.Parser.Syntax.Nodes;
using Interpreted_Language.Language.Parser.Syntax.Nodes.Traits;
using Interpreted_Language.RenPy.Language.Interpreter;

namespace Interpreted_Language.RenPy.Language.Nodes
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
        
        /// <inheritdoc />
        public int LineNumber { private get; set; }
        
        private static readonly IReadOnlyDictionary<string, Action<AssignmentNode, RenPyExecutionContext>> MethodNameToAction = 
            new Dictionary<string, Action<AssignmentNode, RenPyExecutionContext>>
            {
                { "character", CharacterMethod },
                { "room", RoomMethod }
            };

        /// <summary>
        /// Initialises a new instance of the <see cref="Interpreted_Language.RenPy.Language.Nodes.AssignmentNode"/> class.
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

        public BlockingType Execute(IExecutionContext context)
        {
            if (!MethodNameToAction.TryGetValue(_methodName, out var action)) throw new Exception($"There was an error on line {LineNumber}. There is no method called '{_methodName}'.");
            action(this, (RenPyExecutionContext)context);
            return BlockingType.NonBlocking;
        }
        
        private static void CharacterMethod(AssignmentNode node, RenPyExecutionContext context)
        {
            if (node._methodArguments.Length != 1) throw new Exception($"There was an error on line {node.LineNumber}. The character method got {node._methodArguments.Length} argument, expected 1 argument.");
            var characterId = node._methodArguments[0] as string;
            
            var (hasNullVariable, errorMessage) = IsAnyVariableNull(new[] {typeof(string)}, characterId);
            if(hasNullVariable) throw new Exception($"There was an error on line {node.LineNumber}. {errorMessage}");
            
            Console.WriteLine($"Created an instance for '{characterId}' using alias {node._variableName}.");

            if (!context.TryCreateCharacterAlias(node._variableName, characterId)) throw new Exception($"There was an error on {node.LineNumber}. There is already an alias called '{node._variableName}'.");
        }
        
        private static void RoomMethod(AssignmentNode node, RenPyExecutionContext context)
        {
            if (node._methodArguments.Length != 1) throw new Exception($"There was an error on line {node.LineNumber}. The room method got {node._methodArguments.Length} argument, expected 1 argument.");
            var roomId = node._methodArguments[0] as string;

            var (hasNullVariable, errorMessage) = IsAnyVariableNull(new[] {typeof(string)}, roomId);
            if(hasNullVariable) throw new Exception($"There was an error on line {node.LineNumber}. {errorMessage}");
            
            Console.WriteLine($"Created a reference to '{roomId}' using alias {node._variableName}.");
            
            if (!context.TryCreateRoomAlias(node._variableName, roomId)) throw new Exception($"There was an error on {node.LineNumber}. There is already an alias called '{node._variableName}'.");
        }

        private static (bool, string) IsAnyVariableNull(IReadOnlyList<Type> expectedTypes, params object[] variables)
        {
            if(expectedTypes.Count != variables.Length) throw new Exception("Invalid number of expected types or variables specified.");

            for (var i = 0; i < variables.Length; i++)
            {
                if (variables[i] == null) return (true, $"Invalid type given for argument {i + 1}, expected a {expectedTypes[i].Name.ToLower()}.");
            }

            return (false, string.Empty);
        }
    }
}