using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Internal
{
    /// <summary>
    /// Special-case block element parser.
    /// </summary>
    internal sealed class NullBlockElementParser : BlockElementParserBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NullBlockElementParser"/> class.
        /// </summary>
        public NullBlockElementParser()
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