using System;

using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile.Internal;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides a base class for authoring engines and implements core methods of 
    /// <see cref="IAuthoringEngine"/>.
    /// </summary>
    public abstract class AuthoringEngineBase : IAuthoringEngine
    {
        #region Private Member Variables
        private IBlockElementParser rootBlockElementParser = new NullBlockElementParser();
        private IInlineElementParser rootInlineElementParser = new NullInlineElementParser();
        private object syncRoot = new object();
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets a reference to the root <see cref="IBlockElementParser"/>.
        /// </summary>
        protected IBlockElementParser RootBlockElementParser
        {
            get { return rootBlockElementParser; }
        }

        /// <summary>
        /// Gets a reference to the root <see cref="IInlineElementParser"/>.
        /// </summary>
        protected IInlineElementParser RootInlineElementParser
        {
            get { return rootInlineElementParser; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="AuthoringEngineBase"/> class.
        /// </summary>
        protected AuthoringEngineBase()
        {
        }

        #region IAuthoringEngine Members
        /// <summary>
        /// Adds <paramref name="blockElementParser"/> to the end of the block
        /// elements parsing chain.
        /// </summary>
        /// <param name="blockElementParser">Block element parser to be added.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="blockElementParser"/> is a <c>null</c> reference.
        /// </exception>
        public virtual void AddBlockElementParser(IBlockElementParser blockElementParser)
        {
            lock(syncRoot)
            {
                AddElementParser(RootBlockElementParser, blockElementParser);
            } // lock
        }

        /// <summary>
        /// Adds <paramref name="inlineElementParser"/> to the end of the inline
        /// elements parsing chain.
        /// </summary>
        /// <param name="inlineElementParser">Inline element parser to be added.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="inlineElementParser"/> is a <c>null</c> reference.
        /// </exception>
        public virtual void AddInlineElementParser(IInlineElementParser inlineElementParser)
        {
            lock(syncRoot)
            {
                AddElementParser(RootInlineElementParser, inlineElementParser);
            } // lock
        }

        /// <summary>
        /// Parses <paramref name="text"/>, extracts block elements and append them
        /// to <paramref name="parentElement"/>.
        /// <seealso cref="BlockElement"/>
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="text"></param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="parentElement"/> is a <c>null</c> reference.
        /// </exception>
        public virtual void ParseBlockElements(DomElement parentElement, String text)
        {
            lock(syncRoot)
            {
                RootBlockElementParser.Parse(this, parentElement, text);
            } // lock
        }

        /// <summary>
        /// Parses <paramref name="text"/>, extracts inline elements and append them
        /// to <paramref name="parentElement"/>.
        /// <seealso cref="InlineElement"/>
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="text"></param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="parentElement"/> is a <c>null</c> reference.
        /// </exception>
        public virtual void ParseInlineElements(DomElement parentElement, String text)
        {
            lock(syncRoot)
            {
                RootInlineElementParser.Parse(this, parentElement, text);
            } // lock
        }

        /// <summary>
        /// Parses <paramref name="text"/> and produces <see cref="DomDocument"/>.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public virtual DomDocument Parse(string text)
        {
            DomDocument document = new DomDocument();
            ParseBlockElements(document, text);

            return document;
        }
        #endregion

        /// <summary>
        /// Recursively adds <paramref name="elementParser"/> to the end of the parsing chain.
        /// </summary>
        /// <param name="parentElementParser"></param>
        /// <param name="elementParser"></param>
        protected static void AddElementParser(IElementParser parentElementParser, IElementParser elementParser)
        {
            if(parentElementParser.NextElementParser == null)
                parentElementParser.NextElementParser = elementParser;
            else
                AddElementParser(parentElementParser.NextElementParser, elementParser);
        }
    }
}
