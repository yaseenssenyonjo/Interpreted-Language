namespace InterpretedLanguage.Language.Parser.SyntaxTree.Traits
{
    internal interface INode
    {
        AdvancementType Execute(SyntaxTree tree);
    }
}