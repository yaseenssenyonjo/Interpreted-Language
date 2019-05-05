using System.Collections.Generic;
using InterpretedLanguage.Language.Parser.Exceptions;
using InterpretedLanguage.Language.Parser.Groups;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Language.Parser
{
    /// <summary>
    /// Represents a parser.
    /// </summary>
    internal class Parser
    {
        /// <summary>
        /// The groups.
        /// </summary>
        private readonly List<Group> _groups = new List<Group>();

        /// <summary>
        /// Creates a group.
        /// </summary>
        /// <param name="name">The name of the group.</param>
        /// <returns>The group.</returns>
        /// <remarks>The name is syntax sugar.</remarks>
        public Group CreateGroup(string name = null)
        {
            var group = new Group();
            _groups.Add(group);
            return group;
        }
        
        /// <summary>
        /// Parses the specified tokens into an abstract syntax tree.
        /// </summary>
        /// <param name="tree">The abstract syntax tree.</param>
        /// <param name="tokens">The tokens.</param>
        /// <typeparam name="T">The type of abstract syntax tree.</typeparam>
        public void Parse<T>(T tree, TokenList tokens) where T : SyntaxTree.SyntaxTree
        {
            while (!tokens.EndOfStream())
            {
                var i = 0;

                while (i < _groups.Count)
                {
                    // Take a screenshot of the tokens list.
                    tokens.CreateScreenshot();
                    
                    // If the group doesn't match the tokens, rollback to the screenshot
                    // and continue.
                    if (!_groups[i++].Matches(tree, tokens))
                    {
                        tokens.RollbackScreenshot();
                        continue;
                    }
                    
                    // If we still have tokens reset the index, and
                    // starts from the first group.
                    if (!tokens.EndOfStream())
                    {
                        i = 0;
                        continue;
                    }
                    
                    // Otherwise, break out of the loop.
                    break;
                }

                if (!tokens.EndOfStream())
                    throw new ParserException($"There is no group able to match the tokens on line {tokens.Peek().LineNumber}.");
            }
        }
    }
}