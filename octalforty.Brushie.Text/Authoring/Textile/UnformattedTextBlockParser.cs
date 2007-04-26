using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for performing such tasks as converting unformatted text block
    /// into <see cref="TextBlock"/> instances and making the text look somewhat
    /// more pretty than it used to.
    /// </summary>
    public sealed class UnformattedTextBlockParser : InlineElementParserBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnformattedTextBlockParser"/>.
        /// </summary>
        public UnformattedTextBlockParser()
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
            parentElement.AppendChild(new TextBlock(parentElement, InlineElementAttributes.Empty, 
                text, TextBlockFormatting.Unknown));
        }
        #endregion
    }
}
