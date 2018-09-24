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
        private readonly Dictionary<string, object> _rooms = new Dictionary<string, object>(); // todo
        
        public bool HasLabel(string name)
        {
            return _labels.ContainsKey(name);
        }

        public void RegisterLabel(string name, LabelNode node)
        {
            if(_labels.ContainsKey(name)) throw new Exception($"Failed to register label. There is already a label called '{name}'.");
            _labels.Add(name, node);
        }
        
        public void TryExecuteLabel(string name)
        {
            if (!_labels.TryGetValue(name, out var node)) throw new Exception($"Failed to execute label. There is no label called '{name}'.");
            node.Execute(this);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="characterId"></param>
        /// <returns><c>true</c> if the alias was created, otherwise <c>false</c></returns>
        public bool TryCreateCharacterAlias(string alias, string characterId)
        {
            // todo: create character using characterId
            _characters.Add(alias, null);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="roomId"></param>
        /// <returns><c>true</c> if the alias was created, otherwise <c>false</c></returns>
        public bool TryCreateRoomAlias(string alias, string roomId)
        {
            // todo: find the room using roomId.
            _rooms.Add(alias, null);
            return true;
        }

        public Character GetCharacter(string alias)
        {
            if (!_characters.TryGetValue(alias, out var character)) throw new Exception($"Failed to get character. There is no character alias called '{alias}'.");
            return character;
        }
    }
}