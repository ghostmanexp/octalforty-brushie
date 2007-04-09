using System;
using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Internal;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Represents an authoring engine capable of transforming Textile-formatted text
    /// into arbitrary markup with the help of <see cref="ITextileAuthoringFormatter"/>.
    /// </summary>
    /// <remarks>
    /// Textile Quick Reference is available at http://hobix.com/textile/quick.html. More extensive one
    /// can be found at http://www.textism.com/tools/textile/
    /// </remarks>
    public sealed class TextileAuthoringEngine
    {
        #region Private Constants
#pragma warning disable 1570
        /// <summary>
        /// (?<Expression>
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
        /// (?<Text>.*)\n)\n
        /// </summary>
        private static readonly Regex HeadingRegex = new Regex(
            @"(?<Expression>^h(?<Level>[1-6])(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Alignment>(=)|(\<\>)|(\<)|(\>))?(?<Indentation>((?<LeftIndent>\(*)(?<RightIndent>\)*)))?\.\s(?<Text>.*))\r\n\r\n",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | 
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// (?<Expression>
        /// ^bq
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
        /// (?<Text>.*)\n)\n
        /// </summary>
        private static readonly Regex BlockquoteRegex = new Regex(
            @"(?<Expression>^bq(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Alignment>(=)|(\<\>)|(\<)|(\>))?(?<Indentation>((?<LeftIndent>\(*)(?<RightIndent>\)*)))?\.\s(?<Text>.*))\r\n\r\n",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// [^\\](?<Expression>"(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>[^"(]*)(\((?<Title>.+?)\))?":(?<Url>\S*))
        /// </summary>
        private static readonly Regex LinkRegex = new Regex(
            "[^\\\\](?<Expression>\"(\\(((\\#(?<ID>.+?))|((?<CssClass>.+?)\\#(?<ID>.+?))|(?<CssClass>.+?))\\))?(\\{(?<Style>.+?)\\})?(\\[(?<Language>.+?)\\])?(?<Text>[^\"(]*)(\\((?<Title>.+?)\\))?\":(?<Url>\\S*))",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// [^\\](?<Expression>\*(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)\*)
        /// </summary>
        private static readonly Regex StrongEmphasisRegex = new Regex(
            @"[^\\](?<Expression>\*(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)\*)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// [^\\](?<Expression>\*\*(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)\*\*)
        /// </summary>
        private static readonly Regex BoldRegex = new Regex(
            @"[^\\](?<Expression>\*\*(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)\*\*)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// [^\\](?<Expression>_(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)_)
        /// </summary>
        private static readonly Regex EmphasisRegex = new Regex(
            @"[^\\](?<Expression>_(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)_)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// [^\\](?<Expression>__(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)__)
        /// </summary>
        private static readonly Regex ItalicsRegex = new Regex(
            @"[^\\](?<Expression>__(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)__)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// [^\\](?<Expression>\?\?(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)\?\?)
        /// </summary>
        private static readonly Regex CitationRegex = new Regex(
            @"[^\\](?<Expression>\?\?(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)\?\?)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// [^\\](?<Expression>-(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)-)
        /// </summary>
        private static readonly Regex DeletedRegex = new Regex(
            @"[^\\](?<Expression>-(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)-)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// [^\\](?<Expression>\+(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)\+)
        /// </summary>
        private static readonly Regex InsertedRegex = new Regex(
            @"[^\\](?<Expression>\+(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)\+)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// [^\\](?<Expression>\^(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)\^)
        /// </summary>
        private static readonly Regex SuperscriptRegex = new Regex(
            @"[^\\](?<Expression>\^(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)\^)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// [^\\](?<Expression>~(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)~)
        /// </summary>
        private static readonly Regex SubscriptRegex = new Regex(
            @"[^\\](?<Expression>~(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)~)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// [^\\](?<Expression>%(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)%)
        /// </summary>
        private static readonly Regex SpanRegex = new Regex(
            @"[^\\](?<Expression>%(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)%)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// (?<Expression>^((?<Qualifier>[*#]+)(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?\s(?<Title>.*)\r\n)+)
        /// </summary>
        private static readonly Regex ListRegex = new Regex(
            @"(?<Expression>^((?<Qualifier>[*#]+)(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?\s(?<Title>.*)\r\n)+)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// ^(?!h[1-6]|bq|p|\#|\*|\|)(?<Text>(.(\r\n)?)+)\r\n\r\n
        /// </summary>
        private static readonly Regex ImplicitParagraphRegex = new Regex(
            @"^(?!h[1-6]|bq|p|\#|\*|\|)(?<Text>(.(\r\n)?)+)\r\n\r\n",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// (?<Expression>^p(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Alignment>(=)|(\<\>)|(\<)|(\>))?(?<Indentation>((?<LeftIndent>\(*)(?<RightIndent>\)*)))?\.\s(?<Text>(.(\r\n)?)*))\r\n\r\n
        /// </summary>
        private static readonly Regex ParagraphRegex = new Regex(
            @"(?<Expression>^p(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Alignment>(=)|(\<\>)|(\<)|(\>))?(?<Indentation>((?<LeftIndent>\(*)(?<RightIndent>\)*)))?\.\s(?<Text>(.(\r\n)?)*))\r\n\r\n",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// [^\\](?<Expression>!(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Url>.+)(\s)?\((?<AlternateText>.+)\)\!)
        /// </summary>
        private static readonly Regex ImageRegex = new Regex(
            @"[^\\](?<Expression>!(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Url>.+)(\s)?\((?<AlternateText>.+)\)\!)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// [^\\](?<Expression>\[(?<FootnoteID>\d+)\])
        /// </summary>
        private static readonly Regex FootnoteReferenceRegex = new Regex(
            @"[^\\](?<Expression>\[(?<FootnoteID>\d+)\])",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex FootnoteRegex = new Regex(
            @"(?<Expression>^fn(?<FootnoteID>\d+)\.\s(?<Text>.*))\r\n\r\n",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
#pragma warning restore 1570
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
            //
            // Skipping markup inside <pre> tags.
            string uppercaseText = text.ToUpper();
            if(uppercaseText.Contains("<PRE") && uppercaseText.Contains("</PRE>"))
            {
                int preStartTagIndex = uppercaseText.IndexOf("<PRE");
                int preEndTagIndex = uppercaseText.IndexOf("</PRE>", preStartTagIndex);

                if(preStartTagIndex < preEndTagIndex)
                {
                    String beforePre = text.Substring(0, preStartTagIndex);
                    String afterPre = text.Substring(preEndTagIndex + 6);
                    String preMarkup = text.Substring(preStartTagIndex, preEndTagIndex - preStartTagIndex + 6);

                    return Author(beforePre, authoringScope) + AuthorPre(preMarkup) + Author(afterPre, authoringScope);
                } // if
            } // if

            /*if((authoringScope & AuthoringScope.Lists) == AuthoringScope.Lists)
                text = AuthorLists(text);*/
            // Lists, tables and possibly something else should be authored prior
            // to removing linebreaks.
            if((authoringScope & AuthoringScope.Links) == AuthoringScope.Links)
                text = AuthorHyperlinks(text);

            if((authoringScope & AuthoringScope.Images) == AuthoringScope.Images)
                text = AuthorImages(text);

            if((authoringScope & AuthoringScope.TextFormatting) == AuthoringScope.TextFormatting)
                text = AuthorTextFormatting(text);

            //
            // Prior to authoring block stuff, we need to perform some conversions of the source text.
            // Get rid of \t characters, since we'd need them later on.
            text = text.Replace("\t", "");

            //
            // Replacing triplets of linebreaks with two linebreaks.
            while(text.Contains("\r\n\r\n\r\n"))
                text = text.Replace("\r\n\r\n\r\n", "\r\n\r\n");

            //
            // Now we need to replace two consecutive linebreaks with one \t character,
            // then eliminate all single linebreaks and only then restore original
            // double linebreaks.
            text = text.Replace("\r\n\r\n", "\t");
            text = text.Replace("\r\n", "");
            text = text.Replace("\t", "\r\n\r\n");

            text = AuthorParagraphs(text);

            if((authoringScope & AuthoringScope.Headings) == AuthoringScope.Headings)
                text = AuthorHeadings(text);

            if((authoringScope & AuthoringScope.Blockquotes) == AuthoringScope.Blockquotes)
                text = AuthorBlockquotes(text);

            if((authoringScope & AuthoringScope.Footnotes) == AuthoringScope.Footnotes)
                text = AuthorFootnotes(text);

            return text;
        }

        /// <summary>
        /// Authors "pre" markup.
        /// </summary>
        /// <param name="preMarkup"></param>
        /// <returns></returns>
        private static String AuthorPre(string preMarkup)
        {
            preMarkup = preMarkup.Replace("<", "&lt;");
            preMarkup = preMarkup.Replace(">", "&gt;");

            return preMarkup;
        }

        /// <summary>
        /// Authors footnotes and footnote references.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private String AuthorFootnotes(String text)
        {
            return text;
        }

        /// <summary>
        /// Authors images.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private String AuthorImages(String text)
        {
            Match match = ImageRegex.Match(text);
            while(match.Success)
            {
                String expression = match.Groups["Expression"].Value;
                String url = match.Groups["Url"].Value;
                String alternateText = match.Groups["AlternateText"].Value;

                BlockElementAttributes attributes = CreateBlockElementAttributes(match);

                text = text.Replace(expression, 
                    authoringFormatter.FormatImage(alternateText, url, attributes));

                match = ImageRegex.Match(text);
            } // while

            return text;
        }

        /// <summary>
        /// Authors lists.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private String AuthorLists(String text)
        {
            Match match = ListRegex.Match(text);
            while(match.Success)
            {
                String expression = match.Groups["Expression"].Value;
               /* String textToFormat = match.Groups["Text"].Value;

                //
                // We need to ensure that current expression is not inside <pre> tags.
                if(!IsMatchBetweenTags(text, match, "pre"))
                {
                    PhraseElementAttributes attributes = CreatePhraseElementAttributes(match);

                    text = text.Replace(expression, authoringFormatter.FormatTextFormatting(formatting,
                        textToFormat, attributes));
                    match = regex.Match(text);
                } // if
                else
                {*/
                    match = match.NextMatch();
                //} // else
            } // while

            return text;
        }

        /// <summary>
        /// Authors text formatting.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private String AuthorTextFormatting(String text)
        {
            text = AuthorTextFormatting(TextFormatting.Bold, BoldRegex, text);
            text = AuthorTextFormatting(TextFormatting.StrongEmphasis, StrongEmphasisRegex, text);
            text = AuthorTextFormatting(TextFormatting.Italics, ItalicsRegex, text);
            text = AuthorTextFormatting(TextFormatting.Emphasis, EmphasisRegex, text);
            text = AuthorTextFormatting(TextFormatting.Citation, CitationRegex, text);
            text = AuthorTextFormatting(TextFormatting.Inserted, InsertedRegex, text);
            text = AuthorTextFormatting(TextFormatting.Deleted, DeletedRegex, text);
            text = AuthorTextFormatting(TextFormatting.Superscript, SuperscriptRegex, text);
            text = AuthorTextFormatting(TextFormatting.Subscript, SubscriptRegex, text);
            text = AuthorTextFormatting(TextFormatting.Span, SpanRegex, text);

            return text;
        }

        /// <summary>
        /// Authors paragraphs.
        /// </summary>
        /// <param name="text"></param>
        private String AuthorParagraphs(String text)
        {
            //
            // Preparing paragraphs not directly specified with the "p." modifier and
            // then authoring them all in one sweep.
            Match implicitParagraphMatch = ImplicitParagraphRegex.Match(text);
            while(implicitParagraphMatch.Success)
            {
                text = text.Replace(implicitParagraphMatch.Groups["Text"].Value, "p. " + 
                    implicitParagraphMatch.Groups["Text"].Value);
                implicitParagraphMatch = ImplicitParagraphRegex.Match(text);
            } // match

            //
            // Now do all the paragraphs remaining
            Match paragraphMatch = ParagraphRegex.Match(text);
            while(paragraphMatch.Success)
            {
                String expression = paragraphMatch.Groups["Expression"].Value;
                String paragraphText = paragraphMatch.Groups["Text"].Value;

                BlockElementAttributes attributes = CreateBlockElementAttributes(paragraphMatch);

                text = text.Replace(expression, authoringFormatter.FormatParagraph(paragraphText,
                    attributes));

                paragraphMatch = ParagraphRegex.Match(text);
            } // while

            return text;
        }

        /// <summary>
        /// Authors text formatting which is accessed via <paramref name="regex"/>. It is required
        /// that the <paramref name="regex"/> produces two groups - <c>Expression</c> and <c>Text</c>.
        /// </summary>
        /// <param name="formatting"></param>
        /// <param name="regex"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private String AuthorTextFormatting(TextFormatting formatting, Regex regex, String text)
        {
            Match match = regex.Match(text);
            while(match.Success)
            {
                String expression = match.Groups["Expression"].Value;
                String textToFormat = match.Groups["Text"].Value;

                //
                // We need to ensure that current expression is not inside <pre> tags.
                if(!IsMatchBetweenTags(text, match, "pre"))
                {
                    PhraseElementAttributes attributes = CreatePhraseElementAttributes(match);

                    text = text.Replace(expression, authoringFormatter.FormatTextFormatting(formatting,
                        textToFormat, attributes));
                    match = regex.Match(text);
                } // if
                else
                {
                    match = match.NextMatch();
                } // else
            } // while

            return text;
        }

        private static Boolean IsMatchBetweenTags(String text, Match match, String tag)
        {
            String startTag = String.Format("<{0}", tag.ToUpper());
            String endTag = String.Format("</{0}>", tag.ToUpper());

            text = text.ToUpper();

            String beforeMatch = text.Substring(0, match.Index).ToUpper();
            String afterMatch = text.Substring(match.Index + match.Length);

            Int32 startTagBeforeMatch = beforeMatch.LastIndexOf(startTag);
            Int32 endTagBeforeMatch = beforeMatch.LastIndexOf(endTag);
            Int32 startTagAfterMatch = afterMatch.IndexOf(startTag);
            Int32 endTagAfterMatch = afterMatch.IndexOf(endTag);

            //
            // Shortcuts
            if(startTagBeforeMatch == -1 || endTagAfterMatch == -1)
                return false;

            //
            // If the tag starts before match (which is in case when end tag before match is either missing
            // or is positioned even before the start tag) and ends after the match (that is, ends
            // before start tag in after match, if one exists), we're inside those tags.
            if(((endTagBeforeMatch == -1 && startTagBeforeMatch != -1) || (endTagBeforeMatch < startTagBeforeMatch)) &&
                (endTagAfterMatch != -1 && startTagAfterMatch == -1) || (endTagAfterMatch > startTagAfterMatch))
                return true;
            
            return false;
        }

        /// <summary>
        /// Authors hyperlinks.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private String AuthorHyperlinks(String text)
        {
            Match match = LinkRegex.Match(text);
            while(match.Success)
            {
                Hyperlink hyperlink = new Hyperlink(match);
                PhraseElementAttributes attributes = CreatePhraseElementAttributes(match);

                text = text.Replace(hyperlink.Expression, authoringFormatter.FormatHyperlink(hyperlink.Text, 
                    hyperlink.Title, hyperlink.Url, attributes));

                match = LinkRegex.Match(text);
            } // while

            return text;
        }

        /// <summary>
        /// Authors blockquotes.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private String AuthorBlockquotes(String text)
        {
            Match match = BlockquoteRegex.Match(text);
            while(match.Success)
            {
                String expression = match.Groups["Expression"].Value;
                String blockquoteText = match.Groups["Text"].Value;

                BlockElementAttributes attributes = CreateBlockElementAttributes(match);

                text = text.Replace(expression, authoringFormatter.FormatBlockquote(blockquoteText, attributes));

                match = BlockquoteRegex.Match(text);
            } // while

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
        private static BlockElementAttributes CreateBlockElementAttributes(Match match)
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
        private static PhraseElementAttributes CreatePhraseElementAttributes(Match match)
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
