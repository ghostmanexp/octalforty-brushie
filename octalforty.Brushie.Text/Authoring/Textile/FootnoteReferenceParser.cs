using System;
using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile footnote references.
    /// <code>
    /// This[1] is a footnote reference, whereas [2] this and\[3] and[this] are not.
    /// </code>
    /// </summary>
    public sealed class FootnoteReferenceParser : TextileRegexBasedInlineElementParserBase
    {
        #region Private Constants
        private static readonly Regex FootnoteReferenceRegex = new Regex(
            @"(?<!\\|\s)(?<Expression>\[(?<FootnoteID>\d+)\])", 
            RegexOptions.Compiled);
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
