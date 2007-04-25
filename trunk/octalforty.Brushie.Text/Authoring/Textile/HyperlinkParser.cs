using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile hyperlinks.
    /// <code>
    /// "This is a hyperlink(with optional title)":uri
    /// </code>
    /// </summary>
    /// <remarks>
    /// The <c>uri</c> part of the hyperlink (as of current implementation) is parsed as as at least one
    /// non-whitespace character.
    /// </remarks>
    public sealed class HyperlinkParser : InlineElementParserBase
    {
        #region Private Constants
        private static readonly Regex HyperlinkRegex = new Regex(
            "(?<!\\\\)(?<Expression>\"(\\(((\\#(?<ID>.+?))|((?<CssClass>.+?)\\#(?<ID>.+?))|(?<CssClass>.+?))\\))?(\\{(?<Style>.+?)\\})?(\\[(?<Language>.+?)\\])?(?<Text>[^\"(]*)(\\((?<Title>.+?)\\))?\":(?<Url>\\S+))",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="HyperlinkParser"/> class.
        /// </summary>
        public HyperlinkParser()
        {
        }

        #region ElementParserBase Members
        /// <summary>
        /// Returns a reference to the <see cref="System.Text.RegularExpressions.Regex"/>
        /// used in <see cref="ElementParserBase.Parse"/>.
        /// </summary>
        protected override Regex Regex
        {
            get { return HyperlinkRegex; }
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
            Hyperlink hyperlink = new Hyperlink(parentElement, match.Groups["Text"].Value,
                match.Groups["Title"].Value, match.Groups["Url"].Value);
            parentElement.AppendChild(hyperlink);
        }
        #endregion
    }
}
