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
        /// <summary>
        /// Initializes a new instance of <see cref="TextileRegexBasedElementParserBase"/> class.
        /// </summary>
        protected TextileRegexBasedElementParserBase()
        {
        }

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
