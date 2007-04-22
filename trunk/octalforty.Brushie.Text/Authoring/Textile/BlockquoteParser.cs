using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile blockquotes.
    /// </summary>
    public sealed class BlockquoteParser : BlockElementParserBase
    {
        #region Private Constants
        private static readonly Regex BlockquoteRegex =
            new Regex(
                @"(?<Expression>^bq(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Alignment>(=)|(\<\>)|(\<)|(\>))?(?<Indentation>((?<LeftIndent>\(*)(?<RightIndent>\)*)))?\.\s(?<Text>.*))\r\n\r\n",
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="BlockquoteParser"/> class.
        /// </summary>
        public BlockquoteParser()
        {
        }

        #region ElementParserBase Members
        /// <summary>
        /// Returns a reference to the <see cref="System.Text.RegularExpressions.Regex"/>
        /// used in <see cref="ElementParserBase.Parse"/>.
        /// </summary>
        protected override Regex Regex
        {
            get { return BlockquoteRegex; }
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
            Blockquote blockquote = new Blockquote(parentElement, CreateBlockElementAttributes(match));
            parentElement.AppendChild(blockquote);
            
            ParseWithNextElementParser(authoringEngine, parentElement, match.Groups["Text"].Value);
        }
        #endregion
    }
}
