namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides a base class for regex-based Textile block element parsers.
    /// </summary>
    public abstract class TextileRegexBasedBlockElementParserBase : TextileRegexBasedElementParserBase, IBlockElementParser
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TextileRegexBasedBlockElementParserBase"/>.
        /// </summary>
        protected TextileRegexBasedBlockElementParserBase()
        {
        }
    }
}
