using System;
using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.Text.Authoring.Textile
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
        public virtual void Parse(IAuthoringEngine authoringEngine, DomElement parentElement, string text)
        {
            Match match = Regex.Match(text);
            if(match.Success)
            {
                int startIndex = 0;
                while(match.Success)
                {
                    //
                    // Parsing prefix...
                    if(startIndex < match.Index)
                    {
                        ParseWithNextElementParser(authoringEngine, parentElement,
                            text.Substring(startIndex, match.Index - startIndex));
                    } // if

                    ProcessMatch(authoringEngine, parentElement, match);

                    startIndex = match.Index + match.Length;
                    match = Regex.Match(text, startIndex);
                } // while

                ParseWithNextElementParser(authoringEngine, parentElement, text.Substring(startIndex));
            } // if
            else
            {
                //
                // Proceed.
                ParseWithNextElementParser(authoringEngine, parentElement, text);
            } // else
        }
        #endregion

        #region Overridables
        /// <summary>
        /// Returns a reference to the <see cref="System.Text.RegularExpressions.Regex"/>
        /// used in <see cref="Parse"/>.
        /// </summary>
        protected virtual Regex Regex
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Template method which is invoked from <see cref="Parse"/> when
        /// a match is encountered.
        /// </summary>
        /// <param name="authoringEngine"></param>
        /// <param name="parentElement"></param>
        /// <param name="match"></param>
        protected virtual void ProcessMatch(IAuthoringEngine authoringEngine,
            DomElement parentElement, Match match)
        {
        }
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
            if(!IsNullOrEmpty(alignment))
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
            if(!IsNullOrEmpty(leftIndent))
                leftIndentValue = leftIndent.Length;

            //
            // Right indent.
            if(!IsNullOrEmpty(rightIndent))
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

		protected static bool IsNullOrEmpty(String value)
		{
			return value == null || value == string.Empty;
		}
    } 
}
