using System;
using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile footnote references.
    /// </summary>
    public sealed class FootnoteReferenceParser : InlineElementParserBase
    {
        #region Private Constants
        private static readonly Regex FootnoteReferenceRegex = new Regex(
            @"(?<!\\)(?<Expression>\[(?<FootnoteID>\d+)\])",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="FootnoteReferenceParser"/> class.
        /// </summary>
        public FootnoteReferenceParser()
        {
        }

        #region ElementParserBase Members
        /// <summary>
        /// Returns a reference to the <see cref="System.Text.RegularExpressions.Regex"/>
        /// used in <see cref="ElementParserBase.Parse"/>.
        /// </summary>
        protected override Regex Regex
        {
            get { return FootnoteReferenceRegex; }
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
            FootnoteReference footnoteReference = new FootnoteReference(parentElement,
                Convert.ToInt32(match.Groups["FootnoteID"].Value));
            parentElement.AppendChild(footnoteReference);
        }
        #endregion
    }
}
