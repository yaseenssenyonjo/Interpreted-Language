using System.Collections.Generic;

namespace InterpretedLanguage.Language.Parser.Groups.Traits
{
    /// <summary>
    /// Represents statements in a certain order.
    /// </summary>
    internal interface IGroupPattern
    {
        /// <summary>
        /// Gets a list of statements.
        /// </summary>
        /// <returns>A list of statements</returns>
        List<IGroupStatement> GetStatements();
    }
}