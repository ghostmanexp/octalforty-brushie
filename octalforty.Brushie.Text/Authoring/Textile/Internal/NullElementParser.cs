using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile.Internal
{
    /// <summary>
    /// Special-case element parser.
    /// </summary>
    internal sealed class NullElementParser : ElementParserBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NullElementParser"/> class.
        /// </summary>
        public NullElementParser()
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
            if(NextElementParser != null)
                NextElementParser.Parse(parentElement, authoringScope, text);
        }
        #endregion
    }
}
