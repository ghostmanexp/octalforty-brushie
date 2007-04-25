using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Defines a contract for an element parser.
    /// </summary>
    public interface IElementParser
    {
        /// <summary>
        /// Gets or sets a reference to the next <see cref="IElementParser"/> in the
        /// parsing chain.
        /// </summary>
        IElementParser NextElementParser
        { get; set; }

        /// <summary>
        /// Parses <paramref name="text"/> which is the child of <paramref name="parentElement"/>.
        /// </summary>
        /// <param name="authoringEngine">
        /// The <see cref="IAuthoringEngine"/> which initiated the parsing process.
        /// </param>
        /// <param name="parentElement">Parent DOM element.</param>
        /// <param name="text">The text to be parsed.</param>
        void Parse(IAuthoringEngine authoringEngine, DomElement parentElement, string text);
    }
}
