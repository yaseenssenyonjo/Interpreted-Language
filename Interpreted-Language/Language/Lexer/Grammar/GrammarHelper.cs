using System.Text;

namespace Interpreted_Language.Language.Lexer.Grammar
{
    /// <summary>
    /// Provides helper methods for interfacing with grammar rules.
    /// </summary>
    internal static class GrammarHelper
    {
        /// <summary>
        /// The underlying string builder.
        /// </summary>
        private static readonly StringBuilder StringBuilder = new StringBuilder();
        
        /// <summary>
        /// Creates the regex pattern for keywords.
        /// </summary>
        /// <param name="keywords">A string array of keywords.</param>
        /// <returns>A regular expression which will match any one of the keywords.</returns>
        public static string CreateKeywordPattern(params string[] keywords)
        {
            // Clears the builder to ensure that it is empty.
            StringBuilder.Clear();
            StringBuilder.Append("^(");

            for (var i = 0; i < keywords.Length; i++)
            {
                // Start of word boundary
                StringBuilder.Append("\\b");
                // The keyword.
                StringBuilder.Append(keywords[i]);
                // End of word boundary.
                StringBuilder.Append("\\b");
                
                // If this is not the last keyword dd a separator character.
                if(i != keywords.Length - 1) StringBuilder.Append("|");
            }

            StringBuilder.Append(")");

            return StringBuilder.ToString();
        }
    }
}