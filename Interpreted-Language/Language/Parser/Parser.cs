using System.Collections.Generic;
using Interpreted_Language.Language.Lexer.Tokens;
using Interpreted_Language.Language.Parser.Exceptions;
using Interpreted_Language.Language.Parser.Groups;
using Interpreted_Language.Language.Parser.Syntax;

namespace Interpreted_Language.Language.Parser
{
    /// <summary>
    /// 
    /// </summary>
    internal class Parser
    {
        /// <summary>
        /// The filter groups.
        /// </summary>
        private readonly List<Group> _groups = new List<Group>();
        
        /// <summary>
        /// Creates a new filter group.
        /// </summary>
        /// <param name="groupName">(Optional) The name of the group.</param>
        /// <remarks>The group name is syntax sugar to help remember what the group does.</remarks>
        public Group CreateGroup(string groupName = null, bool doesCreateNode = true)
        {
            var group = new Group(doesCreateNode);
            _groups.Add(group);
            return group;
        }
        
        /// <summary>
        /// Parses the tokens into an abstract syntax tree.
        /// </summary>
        /// <param name="tokens">The tokens to parse.</param>
        public SyntaxTree Parse(TokenList tokens)
        {
            var syntaxTree = new SyntaxTree();
            
            // Note: The tokens count has one subtracted from it to account for indexes starting at zero.
            // Keep evaluating the tokens until every token has been evaluated.
            while (tokens.Index < tokens.Count)
            {
                for (var i = 0; i < _groups.Count; i++)
                {
                    // If the group evaluation fails, continue to the next group.
                    if (!_groups[i].Evaluate(syntaxTree, tokens)) continue;
                    
                    // If the group evaluates successfully and we still have more tokens set the index to -1.
                    // The index is set to -1 because the index is incremented after every loop thus in
                    // this case it will be incremented to zero.
                    // This is done to begin from the first group rather than the remaining groups left.
                    if (tokens.Index < tokens.Count)
                    {
                        i = -1;
                        continue;
                    }

                    break;
                }
                // If the token index does not match the token count, this means that certain tokens
                // have not been evaluated meaning that there is no group able to match the remaining
                // tokens.
                if (tokens.Index < tokens.Count) throw new ParserException($"There is no group that is able to match remaining tokens on line {tokens[tokens.Index].LineNumber} and possible future lines."); 
            }
            
            return syntaxTree;
        }
    }
}