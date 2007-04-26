using System;
using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing formatted text blocks.
    /// </summary>
    public sealed class FormattedTextBlockParser : InlineElementParserBase
    {
        #region Private Constants
        private static readonly Regex TextBlockRegex = new Regex(
            @"(?<!\\)(?<Tag>(\*\*|__|\*|_|\?\?|-|\+|\^|~|\%))(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Text>.+?)(?<!\\)\k<Tag>",
            RegexOptions.Compiled);
        #endregion

		/// <summary>
		/// Initializes a new instance of <see cref="FormattedTextBlockParser"/> class.
		/// </summary>
		public FormattedTextBlockParser()
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
		public override void Parse(IAuthoringEngine authoringEngine, DomElement parentElement, String text)
		{
            InternalParse(authoringEngine, parentElement, text);
		}
		#endregion

        private void InternalParse(IAuthoringEngine authoringEngine, DomElement parentElement, String text)
        {
            Match match = TextBlockRegex.Match(text);
            if(match.Success)
            {
                int startIndex = 0;
                while(match.Success)
                {
                    //
                    // What we have here is the text without any formatting.
                    if(startIndex < match.Index)
                    {
                        ParseWithNextElementParser(authoringEngine, parentElement,
                            text.Substring(startIndex, match.Index - startIndex));
                    }
                    /*TextBlock prefixTextBlock = new TextBlock(parentElement,
                        InlineElementAttributes.Empty, startIndex < match.Index ?
                            text.Substring(startIndex, match.Index - startIndex) : string.Empty,
                        TextBlockFormatting.Unknown);
                    parentElement.AppendChild(prefixTextBlock);*/

                    //
                    // Parsing match
                    TextBlock textBlock = new TextBlock(parentElement, CreateInlineElementAttributes(match),
                        string.Empty, GetTextBlockFormatting(match.Groups["Tag"].Value));
                    parentElement.AppendChild(textBlock);
                    InternalParse(authoringEngine, textBlock, match.Groups["Text"].Value);

                    startIndex = match.Index + match.Length;
                    match = TextBlockRegex.Match(text, startIndex);
                } // while
                
                ParseWithNextElementParser(authoringEngine, parentElement,
                    text.Substring(startIndex));
                /*
                parentElement.AppendChild(new TextBlock(parentElement, InlineElementAttributes.Empty,
                    text.Substring(startIndex), TextBlockFormatting.Unknown));*/
            } // if
            else
            {
                ParseWithNextElementParser(authoringEngine, parentElement, text);
                /*parentElement.AppendChild(new TextBlock(parentElement, InlineElementAttributes.Empty, 
                    text, TextBlockFormatting.Unknown));*/
            } // else
        }

        /// <summary>
        /// Returns <see cref="TextBlockFormatting"/> based on the value of <paramref name="tag"/>.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static TextBlockFormatting GetTextBlockFormatting(string tag)
        {
            switch(tag)
            {
                case "**":
                    return TextBlockFormatting.Bold;
                case "??":
                    return TextBlockFormatting.Citation;
                case "@":
                    return TextBlockFormatting.Code;
                case "-":
                    return TextBlockFormatting.Deleted;
                case "_":
                    return TextBlockFormatting.Emphasis;
                case "+":
                    return TextBlockFormatting.Inserted;
                case "__":
                    return TextBlockFormatting.Italics;
                case "%":
                    return TextBlockFormatting.Span;
                case "*":
                    return TextBlockFormatting.StrongEmphasis;
                case "~":
                    return TextBlockFormatting.Subscript;
                case "^":
                    return TextBlockFormatting.Superscript;
                default:
                    throw new ArgumentOutOfRangeException("tag");
            } // switch
        }
    }
}
