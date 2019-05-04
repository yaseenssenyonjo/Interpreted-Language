namespace InterpretedLanguage.Parser.SyntaxTree.Traits
{
    internal interface INode
    {
        AdvancementType Execute(SyntaxTree tree);
    }
}