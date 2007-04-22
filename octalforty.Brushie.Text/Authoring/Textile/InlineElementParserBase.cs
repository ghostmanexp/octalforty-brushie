using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides a base class for inline element parsers used in <see cref="IInlineElementParser"/>.
    /// <seealso cref="InlineElement"/>
    /// </summary>
    public abstract class InlineElementParserBase : ElementParserBase, IInlineElementParser
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InlineElementParserBase"/> class.
        /// </summary>
        protected InlineElementParserBase()
        {
        }
    }
}
