using System;
using System.Text.RegularExpressions;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Represents an authoring engine capable of transforming Textile-formatted text
    /// into arbitrary markup with the help of <see cref="ITextileAuthoringFormatter"/>.
    /// </summary>
    public sealed class TextileAuthoringEngine
    {
        #region Private Constants
        /// <summary>
        /// (?<Heading>
        /// ^h
        ///  (?<Level>[1-6]) # Heading level
        ///  (
        ///    \( 
        ///      ( 
        ///        (\#(?<ID>.+?)) | 
        ///        ((?<CssClass>.+?)\#(?<ID>.+?)) | 
        ///        (?<CssClass>.+?) 
        ///       ) 
        ///     \)
        ///  )? # ID, CSS class and ID or simply CSS class
        ///  (\{(?<Style>.+?)\})? # Style
        ///  (\[(?<Language>.+?)\])? # Language
        ///  (?<Alignment>
        ///    (=) |
        ///    (\<\>) |
        ///    (\<) |
        ///    (\>)
        ///  )?
        ///  (?<Indentation>((?<LeftIndent>\(*)(?<RightIndent>\)*)))?
        /// \.\s
        /// (?<Text>.*)\n\n)
        /// </summary>
        public static readonly Regex HeadingRegex = new Regex(
            @"(?<Expression>^h(?<Level>[1-6])(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Alignment>(=)|(\<\>)|(\<)|(\>))?(?<Indentation>((?<LeftIndent>\(*)(?<RightIndent>\)*)))?\.\s(?<Text>.*)\n)\n",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | 
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        #region Private Member Variables
        private ITextileAuthoringFormatter authoringFormatter;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="TextileAuthoringEngine"/> with
        /// a reference to the <see cref="ITextileAuthoringFormatter"/>.
        /// </summary>
        /// <param name="authoringFormatter">Authoring formatter.</param>
        public TextileAuthoringEngine(ITextileAuthoringFormatter authoringFormatter)
        {
            this.authoringFormatter = authoringFormatter;
        }

        /// <summary>
        /// Authors <paramref name="text"/> according to the <paramref name="authoringScope"/>.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="authoringScope"></param>
        public String Author(String text, AuthoringScope authoringScope)
        {
            if((authoringScope & AuthoringScope.Headings) == AuthoringScope.Headings)
                text = AuthorHeadings(text);

            return text;
        }

        /// <summary>
        /// Authors headings.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private String AuthorHeadings(String text)
        {
            Match match = HeadingRegex.Match(text);
            while(match.Success)
            {
                String expression = match.Groups["Expression"].Value;
                String headingText = match.Groups["Text"].Value;
                Int32 level = Convert.ToInt32(match.Groups["Level"].Value);

                BlockElementAttributes attributes = CreateBlockElementAttributes(match);

                text = text.Replace(expression, authoringFormatter.FormatHeading(level, 
                    headingText, attributes));

                match = HeadingRegex.Match(text);
            } // while

            return text;
        }

        /// <summary>
        /// Creates an instance of <see cref="BlockElementAttributes"/> class from the
        /// given <paramref name="match"/>, provided that <paramref name="match"/>
        /// has required groups (<c>CssClass</c>, <c>ID</c>, <c>Style</c>, <c>Language</c>, 
        /// <c>Alignment</c>, <c>LeftIndent</c> and <c>RightIndent</c>).
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private BlockElementAttributes CreateBlockElementAttributes(Match match)
        {
            PhraseElementAttributes phraseElementAttributes =
                CreatePhraseElementAttributes(match);

            //
            // Fetching values
            String alignment = match.Groups["Alignment"].Value;
            String leftIndent = match.Groups["LeftIndent"].Value;
            String rightIndent = match.Groups["RightIndent"].Value;

            //
            // Determining block element alignment
            BlockElementAlignment elementAlignment = BlockElementAlignment.Unknown;
            if(!String.IsNullOrEmpty(alignment))
            {
                switch(alignment.ToUpper())
                {
                    case "<":
                        elementAlignment = BlockElementAlignment.Left;
                        break;
                    case ">":
                        elementAlignment = BlockElementAlignment.Right;
                        break;
                    case "=":
                        elementAlignment = BlockElementAlignment.Center;
                        break;
                    case "<>":
                        elementAlignment = BlockElementAlignment.Justify;
                        break;
                } // switch
            } // if

            //
            // Determining indentation values
            Int32 leftIndentValue = 0;
            Int32 rightIndentValue = 0;

            //
            // Left indent.
            if(!String.IsNullOrEmpty(leftIndent))
                leftIndentValue = leftIndent.Length;

            //
            // Right indent.
            if(!String.IsNullOrEmpty(rightIndent))
                rightIndentValue = rightIndent.Length;

            return new BlockElementAttributes(phraseElementAttributes.CssClass,
                phraseElementAttributes.ID, phraseElementAttributes.Style, phraseElementAttributes.Language,
                elementAlignment, leftIndentValue, rightIndentValue);
        }

        /// <summary>
        /// Creates an instance of <see cref="PhraseElementAttributes"/> class from the
        /// given <paramref name="match"/>, provided that <paramref name="match"/>
        /// has required groups (<c>CssClass</c>, <c>ID</c>, <c>Style</c> and <c>Language</c> ).
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private PhraseElementAttributes CreatePhraseElementAttributes(Match match)
        {
            //
            // Fetching values.
            String cssClass = match.Groups["CssClass"].Value;
            String id = match.Groups["ID"].Value;
            String style = match.Groups["Style"].Value;
            String language = match.Groups["Language"].Value;

            return new PhraseElementAttributes(cssClass, id, style, language);
        }
    }
}
