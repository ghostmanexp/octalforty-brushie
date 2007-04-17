using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing text blocks.
    /// </summary>
    public class TextParser : ElementParserBase
    {
        #region Private Constants
        private static readonly Regex StrongEmphasisRegex = new Regex(
            @"(?<!\\)(?<Expression>\*(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)(?<!\\)\*)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex BoldRegex = new Regex(
            @"(?<!\\)(?<Expression>\*\*(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)(?<!\\)\*\*)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex EmphasisRegex = new Regex(
            @"(?<!\\)(?<Expression>_(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)(?<!\\)_)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex ItalicsRegex = new Regex(
            @"(?<!\\)(?<Expression>__(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)(?<!\\)__)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex CitationRegex = new Regex(
            @"(?<!\\)(?<Expression>\?\?(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)(?<!\\)\?\?)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex DeletedRegex = new Regex(
            @"(?<!\\)(?<Expression>-(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)(?<!\\)-)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex InsertedRegex = new Regex(
            @"(?<!\\)(?<Expression>\+(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)(?<!\\)\+)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex SuperscriptRegex = new Regex(
            @"(?<!\\)(?<Expression>\^(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)(?<!\\)\^)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex SubscriptRegex = new Regex(
            @"(?<!\\)(?<Expression>~(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)(?<!\\)~)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex SpanRegex = new Regex(
            @"(?<!\\)(?<Expression>%(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)(?<!\\)%)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="TextParser"/> class.
        /// </summary>
        public TextParser()
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
            if(Parse(parentElement, authoringScope, text, StrongEmphasisRegex, TextBlockModifier.StrongEmphasis))
                return;

            if(Parse(parentElement, authoringScope, text, ItalicsRegex, TextBlockModifier.Italics))
                return;

            if(Parse(parentElement, authoringScope, text, EmphasisRegex, TextBlockModifier.Emphasis))
                return;

            if(Parse(parentElement, authoringScope, text, BoldRegex, TextBlockModifier.Bold))
                return;

            if(Parse(parentElement, authoringScope, text, CitationRegex, TextBlockModifier.Citation))
                return;

            if(Parse(parentElement, authoringScope, text, DeletedRegex, TextBlockModifier.Deleted))
                return;

            if(Parse(parentElement, authoringScope, text, InsertedRegex, TextBlockModifier.Inserted))
                return;

            if(Parse(parentElement, authoringScope, text, SuperscriptRegex, TextBlockModifier.Superscript))
                return;

            if(Parse(parentElement, authoringScope, text, SubscriptRegex, TextBlockModifier.Subscript))
                return;

            if(Parse(parentElement, authoringScope, text, SpanRegex, TextBlockModifier.Unknown))
                return;

            parentElement.AppendChild(new TextBlock(parentElement, text));
        }
        #endregion

        private static bool Parse(DomElement parentElement, AuthoringScope authoringScope, string text, 
            Regex regex, TextBlockModifier expectedModifier)
        {
            Match match = regex.Match(text);
            if(match.Success)
            {
                //
                // Parsing prefix...
                ParsePrefix(parentElement, authoringScope, text, match);

                //
                // ...text block itself...
                TextBlock textBlock = new TextBlock(parentElement, match.Groups["Text"].Value, expectedModifier);
                parentElement.AppendChild(textBlock);

                //
                // ...and finally parse suffux
                ParseSuffix(parentElement, authoringScope, text, match);
                return true;
            } // if

            return false;
        }
    }
}
