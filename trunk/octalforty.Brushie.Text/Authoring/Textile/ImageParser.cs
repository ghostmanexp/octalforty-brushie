using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile images.
    /// </summary>
    public sealed class ImageParser : ElementParserBase
    {
        #region Private Constants
        private static readonly Regex ImageRegex = new Regex(
            @"(?<!\\)(?<Expression>!(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Url>.+)(\s)?\((?<AlternateText>.+)\)(?<!\\)\!)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ImageParser"/> class.
        /// </summary>
        public ImageParser()
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
            Match imageMatch = ImageRegex.Match(text);
            if(imageMatch.Success)
            {
                //
                // Parsing prefix...
                ParsePrefix(parentElement, authoringScope, text, imageMatch);

                //
                // ...paragraph itself...
                Image image = new Image(parentElement, CreateInlineElementAttributes(imageMatch),
                    imageMatch.Groups["AlternateText"].Value, imageMatch.Groups["Url"].Value);
                parentElement.AppendChild(image);

                //
                // ...and finally parse suffux
                ParseSuffix(parentElement, authoringScope, text, imageMatch);
                return;
            } // if

            ParseWithNextElementParser(parentElement, authoringScope, text);
        }
        #endregion
    }
}
