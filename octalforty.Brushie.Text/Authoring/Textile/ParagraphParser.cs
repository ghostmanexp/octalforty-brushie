using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile paragraphs.
    /// </summary>
    public sealed class ParagraphParser : BlockElementParserBase
    {
        #region Private Constants
        private static readonly Regex ImplicitParagraphRegex = new Regex(
            @"^(?!h[1-6]|fn|bq|p|\#|\*|\|)(?<Text>(.(\r\n)?)+)\r\n\r\n",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex ParagraphRegex = new Regex(
            @"(?<Expression>^p(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Alignment>(=)|(\<\>)|(\<)|(\>))?(?<Indentation>((?<LeftIndent>\(*)(?<RightIndent>\)*)))?\.\s(?<Text>(.(\r\n)?)*))\r\n\r\n",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ParagraphParser"/> class.
        /// </summary>
        public ParagraphParser()
        {
        }

        #region ElementParserBase Members
        /// <summary>
        /// Returns a reference to the <see cref="System.Text.RegularExpressions.Regex"/>
        /// used in <see cref="ElementParserBase.Parse"/>.
        /// </summary>
        protected override Regex Regex
        {
            get { return ParagraphRegex; }
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
            //
            // ...paragraph itself...
            Paragraph paragraph = new Paragraph(parentElement, CreateBlockElementAttributes(match));
            parentElement.AppendChild(paragraph);

            ParseWithNextElementParser(authoringEngine, paragraph, match.Groups["Text"].Value);
        }

        /// <summary>
        /// Parses <paramref name="text"/> which is the child of <paramref name="parentElement"/>.
        /// </summary>
        /// <param name="authoringEngine">
        /// The <see cref="IAuthoringEngine"/> which initiated the parsing process.
        /// </param>
        /// <param name="parentElement">Parent DOM element.</param>
        /// <param name="text">The text to be parsed.</param>
        public override void Parse(IAuthoringEngine authoringEngine, DomElement parentElement, string text)
        {
            //
            // Preparse implicit paragraphs.
            text = PreparseImplicitParagraphs(text);

            base.Parse(authoringEngine, parentElement, text);
        }
        #endregion

        private static string PreparseImplicitParagraphs(string text)
        {
            //
            // First, try to match an implicit paragraph.  If we found one,
            // prepend a "p. " modifier so that it gets matched by the regex below.
            Match implicitParagraphMatch = ImplicitParagraphRegex.Match(text);
            while(implicitParagraphMatch.Success)
            {
                text = text.Replace(implicitParagraphMatch.Value, "p. " + implicitParagraphMatch.Value);
                implicitParagraphMatch = ImplicitParagraphRegex.Match(text);
            } // while

            return text;
        }
    }
}
