using System.Text.RegularExpressions;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile tables.
    /// </summary>
    public sealed class TableParser : BlockElementParserBase
    {
        #region Private Constants
        private static readonly Regex ExplicitTableRegex = new Regex(
            @"(?<Expression>^(table(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?.\r\n))",
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex TableRegex = new Regex(
            @"(?<Expression>^((\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\].\s)?\|(.+?\|\r\n)+))",
                RegexOptions.Multiline | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="TableParser"/> class.
        /// </summary>
        public TableParser()
        {
        }
    }
}
