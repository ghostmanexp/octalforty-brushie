using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile.Internal
{
    /// <summary>
    /// Special-case inline element parser.
    /// </summary>
    internal class NullInlineElementParser : InlineElementParserBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NullInlineElementParser"/> class.
        /// </summary>
        public NullInlineElementParser()
        {
        }

        #region ElementParserBase Members
        /// <summary>
        /// Parses <paramref name="text"/> which is the child of <paramref name="parentElement"/>.
        /// </summary>
        /// <param name="authoringEngine">
        /// The <see cref="IAuthoringEngine"/> which initiated the parsing process.
        /// </param>
        /// <param name="parentElement">Parent DOM element.</param>
        /// <param name="text">The text to be parsed.</param>
        public override void Parse(IAuthoringEngine authoringEngine, DomElement parentElement, string text)
        {
            if(NextElementParser != null)
                NextElementParser.Parse(authoringEngine, parentElement, text);
        }
        #endregion
    }
}
