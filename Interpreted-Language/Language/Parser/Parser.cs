using System.Collections.Generic;
using InterpretedLanguage.Language.Parser.Exceptions;
using InterpretedLanguage.Language.Parser.Groups;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Language.Parser
{
    internal class Parser : Parser<SyntaxTree.SyntaxTree>
    {
    }

    internal class Parser<T> where T : SyntaxTree.SyntaxTree
    {
        private readonly List<Group> _groups = new List<Group>();

        public Group CreateGroup(string name = null)
        {
            var group = new Group();
            _groups.Add(group);
            return group;
        }

        public void Parse(T tree, TokenList tokens)
        {
            while (!tokens.EndOfStream())
            {
                var i = 0;

                while (i < _groups.Count)
                {
                    tokens.CreateScreenshot();

                    if (!_groups[i++].Matches(tree, tokens))
                    {
                        tokens.RollbackScreenshot();
                        continue;
                    }

                    if (!tokens.EndOfStream())
                    {
                        i = 0;
                        continue;
                    }

                    break;
                }

                if (!tokens.EndOfStream())
                    throw new ParserException(
                        $"There is no group able to match the tokens on line {tokens.Peek().LineNumber}.");
            }
        }
    }
}