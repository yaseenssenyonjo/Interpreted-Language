using System.Collections.Generic;
using Interpreted_Language.Language.Lexer.Tokens;
using Interpreted_Language.Language.Parser.Exceptions;
using Interpreted_Language.Language.Parser.Groups;

namespace Interpreted_Language.Language.Parser
{
    internal class Parser
    {
        private readonly List<Group> _groups = new List<Group>();
        
        /// <summary>
        /// Creates a new filter group.
        /// </summary>
        /// <param name="groupName">(Optional) The name of the group.</param>
        /// <remarks>The group name variable is there for naming groups and is .</remarks>
        public Group CreateGroup(string groupName = null)
        {
            var group = new Group();
            _groups.Add(group);
            return group;
        }
        
        /// <summary>
        /// Parses the tokens into an abstract syntax tree.
        /// </summary>
        /// <param name="tokens">The tokens to parse.</param>
        public void Parse(TokenList tokens)
        {
            // The tokens count has one subtracted from it to account for indexes starting at zero.
            
            // Keep evaluating the tokens until every token has been evaluated.
            while (tokens.Index < tokens.Count - 1)
            {
                for (var i = 0; i < _groups.Count; i++)
                {
                    // If this group evaluates successfully, set the index to -1.
                    // The index is set to -1 because the index is incremented after every loop thus in
                    // this case it will be incremented to zero.
                    
                    // This is done to begin from the first group rather than the remaining groups left.
                    if (_groups[i].Evaluate(tokens)) i = -1;
                    
                    // After every group evaluation check if all the tokens have been evaluated.
                    // If the token index matches the token count, return.
                    if(tokens.Index >= tokens.Count - 1) return;
                }

                // This point is only reached if no group is able to match the remaining tokens.
                throw new ParserException($"There is no group that is able to match remaining tokens on line {tokens[tokens.Index].LineNumber + 1} and possible future lines."); 
            }
        }
    }
}