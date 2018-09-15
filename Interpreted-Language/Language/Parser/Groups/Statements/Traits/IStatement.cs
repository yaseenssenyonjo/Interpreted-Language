using Interpreted_Language.Language.Lexer.Tokens;

namespace Interpreted_Language.Language.Parser.Groups.Statements.Traits
{
    /// <summary>
    /// Represents a statement.
    /// </summary>
    internal interface IStatement
    {
        /// <summary>
        /// Determines whether this statement condition is met.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="tokens"></param>
        /// <returns><c>true</c> if this statement condition is met, otherwise <c>false</c>.</returns>
        bool Evaluate(Group group, TokenList tokens);
    }
}