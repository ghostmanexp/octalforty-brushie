using System.Diagnostics;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring
{
    /// <summary>
    /// Provides a base class for element parsers used in <see cref="IAuthoringEngine"/>.
    /// </summary>
    public abstract class ElementParserBase : IElementParser
    {
        #region Private Member Variables
        private IElementParser nextElementParser;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ElementParserBase"/> class.
        /// </summary>
        protected ElementParserBase()
        {
        }

        #region IElementParser Members
        /// <summary>
        /// Gets or sets a reference to the next <see cref="IElementParser"/> in the
        /// parsing chain.
        /// </summary>
        public virtual IElementParser NextElementParser
        {
            [DebuggerStepThrough()]
            get { return nextElementParser; }
            set { nextElementParser = value; }
        }

        /// <summary>
        /// Parses <paramref name="text"/> which is the child of <paramref name="parentElement"/>.
        /// </summary>
        /// <param name="authoringEngine">
        /// The <see cref="IAuthoringEngine"/> which initiated the parsing process.
        /// </param>
        /// <param name="parentElement">Parent DOM element.</param>
        /// <param name="text">The text to be parsed.</param>
        public abstract void Parse(IAuthoringEngine authoringEngine, DomElement parentElement, string text);
        #endregion

        /// <summary>
        /// Parses <paramref name="text"/> which is the child of <paramref name="parentElement"/> 
        /// with <see cref="NextElementParser"/>, if any.
        /// </summary>
        /// <param name="authoringEngine">
        /// The <see cref="IAuthoringEngine"/> which initiated the parsing process.
        /// </param>
        /// <param name="parentElement">Parent DOM element.</param>
        /// <param name="text">The text to be parsed.</param>
        protected void ParseWithNextElementParser(IAuthoringEngine authoringEngine, 
            DomElement parentElement, string text)
        {
            if(NextElementParser != null)
                NextElementParser.Parse(authoringEngine, parentElement, text);
        }
    }
}