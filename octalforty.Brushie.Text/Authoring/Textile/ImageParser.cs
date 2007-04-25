using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile images.
    /// <code>
    /// !image uri(alternate text)!
    /// </code>
    /// </summary>
    public sealed class ImageParser : BlockElementParserBase
    {
        #region Private Constants
        private static readonly Regex ImageRegex =
            new Regex(
                @"(?<!\\)(?<Expression>!(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Url>.+?)(\((?<AlternateText>.+)\))?(?<!\\)\!)",
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
        /// Returns a reference to the <see cref="System.Text.RegularExpressions.Regex"/>
        /// used in <see cref="ElementParserBase.Parse"/>.
        /// </summary>
        protected override Regex Regex
        {
            get { return ImageRegex; }
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
            Image image = new Image(parentElement, CreateBlockElementAttributes(match),
                match.Groups["Url"].Value, match.Groups["AlternateText"].Value);
            parentElement.AppendChild(image);
        }
        #endregion
    }
}
