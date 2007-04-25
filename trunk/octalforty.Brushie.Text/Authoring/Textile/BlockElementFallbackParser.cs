﻿using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Fallback block element parses, which invokes <see cref="IAuthoringEngine.ParseInlineElements"/>
    /// on the text being parsed.
    /// </summary>
    public sealed class BlockElementFallbackParser : BlockElementParserBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BlockElementFallbackParser"/>.
        /// </summary>
        public BlockElementFallbackParser()
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
            authoringEngine.ParseInlineElements(parentElement, text);
        }
        #endregion
    }
}
