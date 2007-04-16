using System;
using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile headings.
    /// </summary>
    public sealed class HeadingParser : ElementParserBase
    {
        #region Private Constants
        private static readonly Regex HeadingRegex = new Regex(
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
        /// Parses <paramref name="text"/> which is the child of <paramref name="parentElement"/> in
        /// accordance with <paramref name="authoringScope"/>.
        /// </summary>
        /// <param name="parentElement">Parent DOM element.</param>
        /// <param name="authoringScope">Authoring scope.</param>
        /// <param name="text">The text to be parsed.</param>
        public override void Parse(DomElement parentElement, AuthoringScope authoringScope, string text)
        {
            if((authoringScope & AuthoringScope.Headings) == AuthoringScope.Headings)
            {
                Match headingMatch = HeadingRegex.Match(text);
                if(headingMatch.Success)
                {
                    //
                    // Parsing prefix...
                    ParsePrefix(parentElement, authoringScope, text, headingMatch);

                    //
                    // ...heading itself...
                    Heading heading = new Heading(parentElement, CreateBlockElementAttributes(headingMatch),
                        Convert.ToInt32(headingMatch.Groups["Level"].Value), headingMatch.Groups["Text"].Value);
                    parentElement.AppendChild(heading);

                    //
                    // ...and end with suffix.
                    ParseSuffix(parentElement, authoringScope, text, headingMatch);
                    return;
                } // if
            } // if

            ParseWithNextElementParser(parentElement, authoringScope, text);
        }
        #endregion
    }
}
