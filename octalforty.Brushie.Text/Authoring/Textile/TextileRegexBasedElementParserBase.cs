using System;
using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides a base class for Textile regex-based element parsers.
    /// </summary>
    public abstract class TextileRegexBasedElementParserBase : RegexBasedElementParserBase
    {
        #region Protected Constants
        /// <summary>
        /// Defines the part of a regex which matches attributes of an inline element.
        /// </summary>
        protected const string InlineElementAttributesRegex = @"((\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))|(\{(?<Style>.+?)\})|(\[(?<Language>.+?)\]))*";

        /// <summary>
        /// Defines the part of a regex which matches attributes of a block element.
        /// </summary>
        protected const string BlockElementAttributesRegex = @"((\(((\#(?<ID>[^()]+?))|((?<CssClass>[^()]+?)\#(?<ID>[^()]+?))|(?<CssClass>[^()]+?))\))|(?<Indentation>((?<LeftIndent>\(*)(?<RightIndent>\)*)))|(\{(?<Style>.+?)\})|(\[(?<Language>.+?)\])|(?<Alignment>(=)|(\<\>)|(\<)|(\>)))*";
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="TextileRegexBasedElementParserBase"/> class.
        /// </summary>
        protected TextileRegexBasedElementParserBase()
        {
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
            String leftIndent = match.Groups["LeftIndent"].Captures.Count > 0 ? 
                match.Groups["LeftIndent"].Captures[0].Value :
                match.Groups["LeftIndent"].Value;
            String rightIndent = match.Groups["RightIndent"].Captures.Count > 0 ?
                match.Groups["RightIndent"].Captures[0].Value :
                match.Groups["RightIndent"].Value;

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

        /// <summary>
        /// Tests whether <paramref name="value"/> is <c>null</c> or equals to <see cref="String.Empty"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static bool IsNullOrEmpty(String value)
        {
            return value == null || value == string.Empty;
        }
    }
}
