using System.Text;
using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for performing such tasks as converting unformatted text block
    /// into <see cref="TextBlock"/> instances and making the text look somewhat
    /// more pretty than it used to.
    /// </summary>
    public sealed class UnformattedTextBlockParser : InlineElementParserBase
    {
        #region Private Constants
        private static readonly Regex QuoteRegex = new Regex(
            "\\\"(?<Text>(\\S.+?\\S))\\\"", RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="UnformattedTextBlockParser"/>.
        /// </summary>
        public UnformattedTextBlockParser()
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
        public override void Parse(IAuthoringEngine authoringEngine, DomElement parentElement, string text)
        {
            //
            // Unsecaping previously escaped characters.
            StringBuilder textBuilder = new StringBuilder(text);
            textBuilder.Replace("\\__", "__");
            textBuilder.Replace("\\**", "**");
            textBuilder.Replace("\\_", "_");
            textBuilder.Replace("\\*", "*");
            textBuilder.Replace("\\??", "??");
            textBuilder.Replace("\\?", "?");
            textBuilder.Replace("\\-", "-");
            textBuilder.Replace("\\+", "+");
            textBuilder.Replace("\\^", "^");
            textBuilder.Replace("\\~", "~");
            textBuilder.Replace("\\@", "@");
            textBuilder.Replace("\\%", "%");

            //
            // Replace double hyphens -- with an em-dash entity.
            textBuilder.Replace("--", "&mdash;");

            //
            // Replace single hyphens surrounded by spaces with an en-dash entity.
            textBuilder.Replace(" - ", "&ndash;");

            //
            // Replace triplets of periods with an ellipsis entity.
            textBuilder.Replace("...", "&hellip;");

            //
            // Convert (TM), (R), and (C) to their respective HTML entities
            textBuilder.Replace("(TM)", "&trade;");
            textBuilder.Replace("(R)", "&reg;");
            textBuilder.Replace("(C)", "&copy;");

            //
            // Replacing simple quotes with angle quotes
            text = QuoteRegex.Replace(textBuilder.ToString(), "&laquo;${Text}&raquo;");

            parentElement.AppendChild(new TextBlock(parentElement, InlineElementAttributes.Empty, 
                text, TextBlockFormatting.Unknown));
        }
        #endregion
    }
}
