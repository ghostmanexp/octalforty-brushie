using System;
using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile headings.
    /// </summary>
    public sealed class HeadingParser : BlockElementParserBase
    {
        #region Private Constants
        private static readonly Regex HeadingRegex =
            new Regex(
                @"(?<Expression>^h(?<Level>[1-6])(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Alignment>(=)|(\<\>)|(\<)|(\>))?(?<Indentation>((?<LeftIndent>\(*)(?<RightIndent>\)*)))?\.\s(?<Text>.*))\r\n\r\n",
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="HeadingParser"/> class.
        /// </summary>
        public HeadingParser()
        {
        }

        #region ElementParserBase Members
        /// <summary>
        /// Returns a reference to the <see cref="System.Text.RegularExpressions.Regex"/>
        /// used in <see cref="ElementParserBase.Parse"/>.
        /// </summary>
        protected override Regex Regex
        {
            get { return HeadingRegex; }
        }

        /// <summary>
        /// Template method which is invoked from <see cref="ElementParserBase.Parse"/> when
        /// a match is encountered.
        /// </summary>
        /// <param name="authoringEngine"></param>
        /// <param name="parentElement"></param>
        /// <param name="match"></param>
        protected override void ProcessMatch(IAuthoringEngine authoringEngine, 
            DomElement parentElement, Match match)
        {
            Heading heading = new Heading(parentElement, CreateBlockElementAttributes(match),
                    Convert.ToInt32(match.Groups["Level"].Value), match.Groups["Text"].Value);
            parentElement.AppendChild(heading);
        }
        #endregion
    }
}
