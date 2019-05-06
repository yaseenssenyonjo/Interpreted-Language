using System;
using InterpretedLanguage.Language.Parser.SyntaxTree;
using InterpretedLanguage.Language.Parser.SyntaxTree.Traits;
using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Examples.Javascript.Nodes
{
    internal class ConsoleNode : INode
    {
        private readonly TokenList _methodArguments;
        private readonly string _methodName;

        public ConsoleNode(string methodName, TokenList methodArguments)
        {
            _methodName = methodName;
            _methodArguments = methodArguments;
        }

        public AdvancementType Execute(SyntaxTree tree)
        {
            switch (_methodName)
            {
                case "log":
                    Console.WriteLine(_methodArguments[0].Value);
                    break;

                case "clear":
                    Console.Clear();
                    break;
            }

            return AdvancementType.Continue;
        }
    }
}