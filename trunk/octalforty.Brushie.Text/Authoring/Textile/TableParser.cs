using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile tables.
    /// </summary>
    public sealed class TableParser : TextileRegexBasedBlockElementParserBase
    {
        #region Private Constants
        // (?<Expression>^(table(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?.\r\n^((\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\].\s)?\|(.+?\|\r\n)+)))
        // (?<Expression>^(table(((\(((\#(?<ID>[^()]+?))|((?<CssClass>[^()]+?)\#(?<ID>[^()]+?))|(?<CssClass>[^()]+?))\))|(?<Indentation>((?<LeftIndent>\(*)(?<RightIndent>\)*)))|(\{(?<Style>.+?)\})|(\[(?<Language>.+?)\])|(?<Alignment>(=)|(\<\>)|(\<)|(\>)))*)?\.\r\n^((((\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))|(\{(?<Style>.+?)\})|(\[(?<Language>.+?)\]))*)\|(.+?\|\r\n)+)))
        private static readonly Regex TableRegex = new Regex(
            @"(?<Expression>^(table(" + BlockElementAttributesRegex + 
            @")?\.\r\n^((((\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))|(\{(?<Style>.+?)\})|(\[(?<Language>.+?)\]))*)\|(.+?\|\r\n)+)))",
                RegexOptions.Multiline | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="TableParser"/> class.
        /// </summary>
        public TableParser()
        {
        }

        #region ElementParserBase Members
        /// <summary>
        /// Returns a reference to the <see cref="System.Text.RegularExpressions.Regex"/>
        /// used in <see cref="ElementParserBase.Parse"/>.
        /// </summary>
        protected override Regex Regex
        {
            get { return TableRegex; }
        }

        /// <summary>
        /// Template method which is invoked from <see cref="ElementParserBase.Parse"/> when
        /// a match is encountered.
        /// </summary>
        /// <param name="authoringEngine"></param>
        /// <param name="parentElement"></param>
        /// <param name="match"></param>
        protected override void ProcessMatch(IAuthoringEngine authoringEngine, DomElement parentElement, Match match)
        {
            //
            // Processing the table.
            Table table = new Table(parentElement, CreateBlockElementAttributes(match));
            parentElement.AppendChild(table);
        }
        #endregion
    }
}
