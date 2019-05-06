using InterpretedLanguage.Language.Tokens;

namespace InterpretedLanguage.Language.Parser.Groups.Traits
{
    /// <summary>
    /// Represents a statement.
    /// </summary>
    internal interface IGroupStatement
    {
        /// <summary>
        /// Matches the specified tokens.
        /// </summary>
        /// <param name="group">The group for this statement.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>true, if this statements matches the tokens; otherwise, false</returns>
        bool Match(Group group, TokenList tokens);
    }
}