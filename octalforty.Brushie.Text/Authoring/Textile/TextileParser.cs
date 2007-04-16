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
        #region Private Static Member Variables
        private static IElementParser rootElementParser = new NullElementParser();
        private static object syncRoot = new object();
        #endregion

        /// <summary>
        /// Adds <paramref name="elementParser"/> to the end of the parsing chain.
        /// </summary>
        /// <param name="elementParser"></param>
        public static void AddElementParser(IElementParser elementParser)
        {
            lock(syncRoot)
            {
                AddElementParser(rootElementParser, elementParser);
            } // lock
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

        /// <summary>
        /// Parses <paramref name="text"/> and produces a <see cref="Document"/>.
        /// </summary>
        /// <param name="authoringScope"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Document Parse(AuthoringScope authoringScope, String text)
        {
            Document document = new Document();
            Parse(document, authoringScope, text);

            return document;
        }

        /// <summary>
        /// Parses <paramref name="text"/>.
        /// </summary>
        /// <param name="domElement"></param>
        /// <param name="authoringScope"></param>
        /// <param name="text"></param>
        public static void Parse(DomElement domElement, AuthoringScope authoringScope, string text)
        {
            rootElementParser.Parse(domElement, AuthoringScope.All, text);
        }
    }
}
