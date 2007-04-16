using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile blockquotes.
    /// </summary>
    public sealed class BlockquoteParser : ElementParserBase
    {
        #region Private Constants
        private static readonly Regex BlockquoteRegex = new Regex(
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
        /// Parses <paramref name="text"/> which is the child of <paramref name="parentElement"/> in
        /// accordance with <paramref name="authoringScope"/>.
        /// </summary>
        /// <param name="parentElement">Parent DOM element.</param>
        /// <param name="authoringScope">Authoring scope.</param>
        /// <param name="text">The text to be parsed.</param>
        public override void Parse(DomElement parentElement, AuthoringScope authoringScope, string text)
        {
            Match blockquoteMatch = BlockquoteRegex.Match(text);
            if(blockquoteMatch.Success)
            {
                //
                // Parsing prefix...
                ParsePrefix(parentElement, authoringScope, text, blockquoteMatch);

                //
                // ...blockquote itself...
                Blockquote blockquote = new Blockquote(parentElement, CreateBlockElementAttributes(blockquoteMatch));
                parentElement.AppendChild(blockquote);

                TextileParser.Parse(blockquote, authoringScope, blockquoteMatch.Groups["Text"].Value);

                //
                // ...and finally parse suffux
                ParseSuffix(parentElement, authoringScope, text, blockquoteMatch);
                return;
            } // if

            //
            // Proceed.
            ParseWithNextElementParser(parentElement, authoringScope, text);
        }
        #endregion
    }
}
