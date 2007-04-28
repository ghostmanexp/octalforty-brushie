using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile acronyms.
    /// <code>
    /// HTML(Hypertext Markup Language)
    /// </code>
    /// <seealso cref="Acronym"/>
    /// </summary>
    public sealed class AcronymParser : TextileRegexBasedInlineElementParserBase
    {
        #region Private Constants
        private static readonly Regex AcronymRegex = new Regex(
            @"(?<Expression>((?<Acronym>\p{Lu}{2,}))\((?<Text>.+?)\))",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="AcronymParser"/> class.
        /// </summary>
        public AcronymParser()
        {
        }

        #region ElementParserBase Members
        /// <summary>
        /// Returns a reference to the <see cref="System.Text.RegularExpressions.Regex"/>
        /// used in <see cref="ElementParserBase.Parse"/>.
        /// </summary>
        protected override Regex Regex
        {
            get { return AcronymRegex; }
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
            Acronym acronym = new Acronym(parentElement, match.Groups["Text"].Value,
                match.Groups["Acronym"].Value);
            parentElement.AppendChild(acronym);
        }
        #endregion
    }
}
