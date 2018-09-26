using System;
using System.Collections.Generic;
using Interpreted_Language.Language.Interpreter.Traits;
using Interpreted_Language.RenPy.Language.Nodes;

namespace Interpreted_Language.RenPy.Language.Interpreter
{
    internal class RenPyExecutionContext : IExecutionContext
    {
        private readonly Dictionary<string, LabelNode> _labels = new Dictionary<string, LabelNode>();
        private readonly Dictionary<string, Character> _characters = new Dictionary<string, Character>();
        private readonly Dictionary<string, object> _rooms = new Dictionary<string, object>();
        
        /// <summary>
        /// Determines whether the specified label name is registered.
        /// </summary>
        /// <param name="name">The name of the label.</param>
        /// <returns><c>true</c> if the label is registered; otherwise, <c>false</c></returns>
        public bool IsLabelRegistered(string name)
        {
            return _labels.ContainsKey(name);
        }
        
        /// <summary>
        /// Registers the specified label.
        /// </summary>
        /// <param name="name">The name of the label.</param>
        /// <param name="node">The label node to register.</param>
        /// <exception cref="Exception">The label name is already in use.</exception>
        public void RegisterLabel(string name, LabelNode node)
        {
            if(_labels.ContainsKey(name)) throw new Exception($"Failed to register label. There is already a label called '{name}'.");
            _labels.Add(name, node);
        }
        
        /// <summary>
        /// Executes the specified label.
        /// </summary>
        /// <param name="name">The name of the label.</param>
        /// <exception cref="Exception">The label name does not exist.</exception>
        public void TryExecuteLabel(string name)
        {
            if (!_labels.TryGetValue(name, out var node)) throw new Exception($"Failed to execute label. There is no label called '{name}'.");
            node.Execute(this);
        }
        
        /// <summary>
        /// Create the character associated with the specified alias.
        /// </summary>
        /// <param name="alias">The alias of the character.</param>
        /// <param name="characterId">The id of the character.</param>
        /// <returns><c>true</c> if the alias was created, otherwise <c>false</c></returns>
        public bool TryCreateCharacterAlias(string alias, string characterId)
        {
            if (_characters.ContainsKey(alias)) return false;
            _characters.Add(alias, new Character(characterId));
            return true;
        }

        /// <summary>
        /// Create a reference to the room associated with the specified alias.
        /// </summary>
        /// <param name="alias">The alias of the room.</param>
        /// <param name="roomId">The id of the room.</param>
        /// <returns><c>true</c> if the alias was created, otherwise <c>false</c></returns>
        public bool TryCreateRoomAlias(string alias, string roomId)
        {
            _rooms.Add(alias, null);
            return true;
        }
        
        /// <summary>
        /// Gets the character associated with the specified alias.
        /// </summary>
        /// <param name="alias">The alias of the character.</param>
        /// <param name="lineNumber">The current line number.</param>
        /// <exception cref="Exception">There is no character associated with the specified alias.</exception>
        /// <returns>The character.</returns>
        public Character GetCharacter(string alias, int lineNumber)
        {
            if (!_characters.TryGetValue(alias, out var character)) throw new Exception($"There was an error on line {lineNumber}. Failed to get character. There is no character alias called '{alias}'.");
            return character;
        }
    }
}