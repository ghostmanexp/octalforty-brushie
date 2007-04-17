using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile hyperlinks.
    /// </summary>
    public sealed class HyperlinkParser : ElementParserBase
    {
        #region Private Constants
        private static readonly Regex HyperlinkRegex = new Regex(
            "[^\\\\](?<Expression>\"(\\(((\\#(?<ID>.+?))|((?<CssClass>.+?)\\#(?<ID>.+?))|(?<CssClass>.+?))\\))?(\\{(?<Style>.+?)\\})?(\\[(?<Language>.+?)\\])?(?<Text>[^\"(]*)(\\((?<Title>.+?)\\))?\":(?<Url>\\S*))",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="HyperlinkParser"/> class.
        /// </summary>
        public HyperlinkParser()
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
            Match hyperlinkMatch = HyperlinkRegex.Match(text);
            if(hyperlinkMatch.Success)
            {
                //
                // Parsing prefix...
                ParsePrefix(parentElement, authoringScope, text, hyperlinkMatch);

                //
                // ...hyperlink itself...
                Hyperlink hyperlink = new Hyperlink(parentElement, hyperlinkMatch.Groups["Text"].Value,
                    hyperlinkMatch.Groups["Title"].Value, hyperlinkMatch.Groups["Url"].Value);
                parentElement.AppendChild(hyperlink);

                //
                // ...and finally parse suffux
                ParseSuffix(parentElement, authoringScope, text, hyperlinkMatch);
                return;
            } // if

            ParseWithNextElementParser(parentElement, authoringScope, text);
        }
        #endregion
    }
}
