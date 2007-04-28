namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides a base class for regex-based Textile inline element parsers.
    /// </summary>
    public abstract class TextileRegexBasedInlineElementParserBase : 
        TextileRegexBasedElementParserBase, IInlineElementParser
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TextileRegexBasedInlineElementParserBase"/>.
        /// </summary>
        protected TextileRegexBasedInlineElementParserBase()
        {
        }
    }
}
