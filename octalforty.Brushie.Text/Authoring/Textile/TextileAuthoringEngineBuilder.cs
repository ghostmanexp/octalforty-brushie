namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Creates <see cref="TextileAuthoringEngine"/> classes.
    /// </summary>
    public class TextileAuthoringEngineBuilder : IAuthoringEngineBuilder
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TextileAuthoringEngineBuilder"/>.
        /// </summary>
        public TextileAuthoringEngineBuilder()
        {
        }

        #region IAuthoringEngineBuilder Members
        /// <summary>
        /// Creates an instance of <see cref="IAuthoringEngine"/>.
        /// </summary>
        /// <returns></returns>
        public IAuthoringEngine CreateAuthoringEngine()
        {
            IAuthoringEngine authoringEngine = new TextileAuthoringEngine();

            //
            // Adding block element parsers.
            AddBlockElementParser(authoringEngine, new NoTextileParser());
            AddBlockElementParser(authoringEngine, new ParagraphParser());
            AddBlockElementParser(authoringEngine, new BlockquoteParser());
            //AddBlockElementParser(new TableParser());
            AddBlockElementParser(authoringEngine, new HeadingParser());
            AddBlockElementParser(authoringEngine, new ImageParser());
            AddBlockElementParser(authoringEngine, new ListParser());
            AddBlockElementParser(authoringEngine, new FootnoteParser());

            AddBlockElementParser(authoringEngine, new BlockElementFallbackParser());

            //
            // Adding inline element parsers.
            AddInlineElementParser(authoringEngine, new AcronymParser());
            AddInlineElementParser(authoringEngine, new HyperlinkParser());
            AddInlineElementParser(authoringEngine, new FootnoteReferenceParser());
            AddInlineElementParser(authoringEngine, new FormattedTextBlockParser());
            AddInlineElementParser(authoringEngine, new UnformattedTextBlockParser());

            return authoringEngine;
        }
        #endregion

        /// <summary>
        /// Adds <paramref name="blockElementParser"/> to the <paramref name="authoringEngine"/>.
        /// </summary>
        /// <param name="authoringEngine"></param>
        /// <param name="blockElementParser"></param>
        protected virtual void AddBlockElementParser(IAuthoringEngine authoringEngine, 
            IBlockElementParser blockElementParser)
        {
            authoringEngine.AddBlockElementParser(blockElementParser);
        }

        /// <summary>
        /// Adds <paramref name="inlineElementParser"/> to the <paramref name="authoringEngine"/>.
        /// </summary>
        /// <param name="authoringEngine"></param>
        /// <param name="inlineElementParser"></param>
        protected virtual void AddInlineElementParser(IAuthoringEngine authoringEngine,
            IInlineElementParser inlineElementParser)
        {
            authoringEngine.AddInlineElementParser(inlineElementParser);
        }
    }
}
