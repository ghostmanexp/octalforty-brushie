using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides a base class for block element parsers used in <see cref="IAuthoringEngine"/>.
    /// <see cref="BlockElement"/>
    /// </summary>
    public abstract class BlockElementParserBase : ElementParserBase, IBlockElementParser
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BlockElementParserBase"/> class.
        /// </summary>
        protected BlockElementParserBase()
        {
        }
    }
}
