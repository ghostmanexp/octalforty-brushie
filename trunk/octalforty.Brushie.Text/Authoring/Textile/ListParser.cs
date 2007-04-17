using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing lists.
    /// </summary>
    public sealed class ListParser : ElementParserBase
    {
        #region Private Constants
        private static readonly Regex ListRegex = new Regex(
            @"(?<Expression>^((?<Qualifier>[*#]+)(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?\s(?<Title>.*)\r\n)+)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ListParser"/> class.
        /// </summary>
        public ListParser()
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
            Match listMatch = ListRegex.Match(text);
            if(listMatch.Success)
            {
                //
                // Parsing prefix...
                ParsePrefix(parentElement, authoringScope, text, listMatch);

                //
                // ...paragraph itself...
                Paragraph paragraph = new Paragraph(parentElement, CreateBlockElementAttributes(listMatch));
                parentElement.AppendChild(paragraph);

                TextileParser.Parse(paragraph, authoringScope, listMatch.Groups["Text"].Value);

                //
                // ...and finally parse suffux
                ParseSuffix(parentElement, authoringScope, text, listMatch);
                return;
            } // if

            ParseWithNextElementParser(parentElement, authoringScope, text);
        }
        #endregion
    }
}
