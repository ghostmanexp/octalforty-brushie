using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile paragraphs.
    /// </summary>
    public sealed class ParagraphParser : ElementParserBase
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
        /// Parses <paramref name="text"/> which is the child of <paramref name="parentElement"/> in
        /// accordance with <paramref name="authoringScope"/>.
        /// </summary>
        /// <param name="parentElement">Parent DOM element.</param>
        /// <param name="authoringScope">Authoring scope.</param>
        /// <param name="text">The text to be parsed.</param>
        public override void Parse(DomElement parentElement, AuthoringScope authoringScope, string text)
        {
            //
            // First, try to match an implicit paragraph.  If we found one,
            // prepend a "p. " modifier so that it gets matched by the regex below.
            Match implicitParagraphMatch = ImplicitParagraphRegex.Match(text);
            if(implicitParagraphMatch.Success)
                text = text.Replace(implicitParagraphMatch.Value, "p. " + implicitParagraphMatch.Value);

            Match paragraphMatch = ParagraphRegex.Match(text);
            if(paragraphMatch.Success)
            {
                //
                // Parsing prefix...
                ParsePrefix(parentElement, authoringScope, text, paragraphMatch);

                //
                // ...paragraph itself...
                Paragraph paragraph = new Paragraph(parentElement, CreateBlockElementAttributes(paragraphMatch));
                parentElement.AppendChild(paragraph);

                TextileParser.Parse(paragraph, authoringScope, paragraphMatch.Groups["Text"].Value);

                //
                // ...and finally parse suffux
                ParseSuffix(parentElement, authoringScope, text, paragraphMatch);
                return;
            } // if

            //
            // Proceed.
            ParseWithNextElementParser(parentElement, authoringScope, text);
        }
        #endregion
    }
}
