using System;

using octalforty.Brushie.Text.Authoring.Textile.Dom;
using octalforty.Brushie.Text.Authoring.Textile.Internal;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing Textile markup into DOM objects.
    /// <seealso cref="IDomElementVisitor"/>
    /// </summary>
    public sealed class TextileParser
    {
        #region Private Member Variables
        private IBlockElementParser rootBlockElementParser = new NullBlockElementParser();
        private IInlineElementParser rootInlineElementParser = new NullInlineElementParser();
        private object syncRoot = new object();
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="TextileParser"/> class.
        /// </summary>
        public TextileParser()
        {
        }

        /// <summary>
        /// Adds <paramref name="blockElementParser"/> to the end of the block elements parsing chain.
        /// </summary>
        /// <param name="blockElementParser"></param>
        public void AddBlockElementParser(IBlockElementParser blockElementParser)
        {
            lock(syncRoot)
            {
                AddElementParser(rootBlockElementParser, blockElementParser);
            } // lock
        }

        /// <summary>
        /// Adds <paramref name="inlineElementParser"/> to the end of the inline elements parsing chain.
        /// </summary>
        /// <param name="inlineElementParser"></param>
        public void AddInlineElementParser(IInlineElementParser inlineElementParser)
        {
            lock(syncRoot)
            {
                AddElementParser(rootInlineElementParser, inlineElementParser);
            } // lock
        }

        /// <summary>
        /// Parses <paramref name="text"/> and produces a <see cref="Document"/>.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Document Parse(String text)
        {
            Document document = new Document();
            Parse(document, text);

            return document;
        }

        /// <summary>
        /// Parses <paramref name="text"/>.
        /// </summary>
        /// <param name="domElement"></param>
        /// <param name="text"></param>
        public void ParseBlockElement(DomElement domElement, String text)
        {
            blocke
            rootElementParser.Parse(domElement, AuthoringScope.All, text);
        }

        /// <summary>
        /// Recursively adds <paramref name="elementParser"/> to the end of the parsing chain.
        /// </summary>
        /// <param name="parentElementParser"></param>
        /// <param name="elementParser"></param>
        private static void AddElementParser(IElementParser parentElementParser, IElementParser elementParser)
        {
            if(parentElementParser.NextElementParser == null)
                parentElementParser.NextElementParser = elementParser;
            else
                AddElementParser(parentElementParser.NextElementParser, elementParser);
        }
    }
}
