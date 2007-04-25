using System;
using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile footnotes.
    /// <code>
    /// fn132. Footnote text here
    /// </code>
    /// </summary>
    public sealed class FootnoteParser : BlockElementParserBase
    {
        #region Private Constants
        private static readonly Regex FootnoteRegex = new Regex(
            @"(?<Expression>^fn(?<FootnoteID>\d+)\.\s(?<Text>.*))\r\n\r\n",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="FootnoteParser"/> class.
        /// </summary>
        public FootnoteParser()
        {
        }

        #region ElementParserBase Members
        /// <summary>
        /// Returns a reference to the <see cref="System.Text.RegularExpressions.Regex"/>
        /// used in <see cref="ElementParserBase.Parse"/>.
        /// </summary>
        protected override Regex Regex
        {
            get { return FootnoteRegex; }
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
            Footnote footnote = new Footnote(parentElement, CreateBlockElementAttributes(match),
                Convert.ToInt32(match.Groups["FootnoteID"].Value));
            parentElement.AppendChild(footnote);

            ParseWithNextElementParser(authoringEngine, footnote, match.Groups["Text"].Value);
        }
        #endregion
    }
}
