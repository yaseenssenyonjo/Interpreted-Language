using System.Text;

namespace Interpreted_Language.Language.Lexer.Grammar
{
    internal static class GrammarHelper
    {
        private static readonly StringBuilder StringBuilder = new StringBuilder();
        
        /// <summary>
        /// Creates the regex pattern for keywords.
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static string CreateKeywordPattern(params string[] keywords)
        {
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