using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing "notextile" blocks.
    /// <code>
    /// ==This text== is left intact
    /// </code>
    /// </summary>
    public sealed class NoTextileParser : TextileRegexBasedBlockElementParserBase
    {
        #region Private Constants
        private static readonly Regex NoTextileRegex = new Regex(
            @"(?<!\\)(?<Expression>==(?<Text>.+?)(?<!\\)==)", 
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="NoTextileParser"/> class.
        /// </summary>
        public NoTextileParser()
        {
        }

        #region ElementParserBase Members
        /// <summary>
        /// Returns a reference to the <see cref="System.Text.RegularExpressions.Regex"/>
        /// used in <see cref="ElementParserBase.Parse"/>.
        /// </summary>
        protected override Regex Regex
        {
            get { return NoTextileRegex; }
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
            TextBlock textBlock = new TextBlock(parentElement, InlineElementAttributes.Empty, 
                match.Groups["Text"].Value, TextBlockFormatting.Unknown);
            parentElement.AppendChild(textBlock);
        }
        #endregion
    }
}
