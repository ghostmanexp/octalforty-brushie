using System;
using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile;
using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides a base class for element parsers used in <see cref="TextileParser"/>.
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
            get { return nextElementParser; }
            set { nextElementParser = value; }
        }

        /// <summary>
        /// Parses <paramref name="text"/> which is the child of <paramref name="parentElement"/> in
        /// accordance with <paramref name="authoringScope"/>.
        /// </summary>
        /// <param name="parentElement">Parent DOM element.</param>
        /// <param name="authoringScope">Authoring scope.</param>
        /// <param name="text">The text to be parsed.</param>
        public abstract void Parse(DomElement parentElement, AuthoringScope authoringScope, string text);
        #endregion

        /// <summary>
        /// Parses <paramref name="text"/> which is the child of <paramref name="parentElement"/> in
        /// accordance with <paramref name="authoringScope"/> with <see cref="NextElementParser"/>, if any.
        /// </summary>
        /// <param name="parentElement">Parent DOM element.</param>
        /// <param name="authoringScope">Authoring scope.</param>
        /// <param name="text">The text to be parsed.</param>
        protected void ParseWithNextElementParser(DomElement parentElement, AuthoringScope authoringScope, string text)
        {
            if(NextElementParser != null)
                NextElementParser.Parse(parentElement, authoringScope, text);
        }

        /// <summary>
        /// Creates an instance of <see cref="BlockElementAttributes"/> class from the
        /// given <paramref name="match"/>, provided that <paramref name="match"/>
        /// has required groups (<c>CssClass</c>, <c>ID</c>, <c>Style</c>, <c>Language</c>, 
        /// <c>Alignment</c>, <c>LeftIndent</c> and <c>RightIndent</c>).
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        protected static BlockElementAttributes CreateBlockElementAttributes(Match match)
        {
            InlineElementAttributes inlineElementAttributes =
                CreateInlineElementAttributes(match);

            //
            // Fetching values
            String alignment = match.Groups["Alignment"].Value;
            String leftIndent = match.Groups["LeftIndent"].Value;
            String rightIndent = match.Groups["RightIndent"].Value;

            //
            // Determining block element alignment
            BlockElementAlignment elementAlignment = BlockElementAlignment.Unknown;
            if(!String.IsNullOrEmpty(alignment))
            {
                switch(alignment.ToUpper())
                {
                    case "<":
                        elementAlignment = BlockElementAlignment.Left;
                        break;
                    case ">":
                        elementAlignment = BlockElementAlignment.Right;
                        break;
                    case "=":
                        elementAlignment = BlockElementAlignment.Center;
                        break;
                    case "<>":
                        elementAlignment = BlockElementAlignment.Justify;
                        break;
                } // switch
            } // if

            //
            // Determining indentation values
            Int32 leftIndentValue = 0;
            Int32 rightIndentValue = 0;

            //
            // Left indent.
            if(!String.IsNullOrEmpty(leftIndent))
                leftIndentValue = leftIndent.Length;

            //
            // Right indent.
            if(!String.IsNullOrEmpty(rightIndent))
                rightIndentValue = rightIndent.Length;

            return new BlockElementAttributes(inlineElementAttributes.CssClass,
                inlineElementAttributes.ID, inlineElementAttributes.Style, inlineElementAttributes.Language,
                elementAlignment, leftIndentValue, rightIndentValue);
        }

        /// <summary>
        /// Creates an instance of <see cref="InlineElementAttributes"/> class from the
        /// given <paramref name="match"/>, provided that <paramref name="match"/>
        /// has required groups (<c>CssClass</c>, <c>ID</c>, <c>Style</c> and <c>Language</c> ).
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        protected static InlineElementAttributes CreateInlineElementAttributes(Match match)
        {
            //
            // Fetching values.
            String cssClass = match.Groups["CssClass"].Value;
            String id = match.Groups["ID"].Value;
            String style = match.Groups["Style"].Value;
            String language = match.Groups["Language"].Value;

            return new InlineElementAttributes(cssClass, id, style, language);
        }

        /// <summary>
        /// Parses text right before <see cref="Match.Value"/>, if there is any.
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="authoringScope"></param>
        /// <param name="text"></param>
        /// <param name="match"></param>
        protected static void ParsePrefix(DomElement parentElement, AuthoringScope authoringScope, string text, Match match)
        {
            if(match.Index != 0)
            {
                TextileParser.Parse(parentElement, authoringScope, text.Substring(0, match.Index));
            } // if
        }

        /// <summary>
        /// Parses text immediately after <see cref="Match.Value"/>, if there is any.
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="authoringScope"></param>
        /// <param name="text"></param>
        /// <param name="match"></param>
        protected static void ParseSuffix(DomElement parentElement, AuthoringScope authoringScope, string text, Match match)
        {
            if(match.Index + match.Length < text.Length)
            {
                TextileParser.Parse(parentElement, authoringScope, text.Substring(match.Index + match.Length));
            } // if
        }
    } 
}
