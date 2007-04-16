using System;
using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TextileParser
    {
        #region Private Constants
        private static readonly Regex HeadingRegex = new Regex(
            @"(?<Expression>^h(?<Level>[1-6])(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Alignment>(=)|(\<\>)|(\<)|(\>))?(?<Indentation>((?<LeftIndent>\(*)(?<RightIndent>\)*)))?\.\s(?<Text>.*))\r\n\r\n",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex ImplicitParagraphRegex = new Regex(
            @"^(?!h[1-6]|fn|bq|p|\#|\*|\|)(?<Text>(.(\r\n)?)+)\r\n\r\n",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex ParagraphRegex = new Regex(
            @"(?<Expression>^p(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?(?<Alignment>(=)|(\<\>)|(\<)|(\>))?(?<Indentation>((?<LeftIndent>\(*)(?<RightIndent>\)*)))?\.\s(?<Text>(.(\r\n)?)*))\r\n\r\n",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

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

        private static void Parse(DomElement domElement, string text)
        {
            //
            // Adding "p. " modifiers
            Match implicitParagraphMatch = ImplicitParagraphRegex.Match(text);
            while(implicitParagraphMatch.Success)
            {
                text = text.Replace(implicitParagraphMatch.Value, "p. " + implicitParagraphMatch.Value);
                implicitParagraphMatch = ImplicitParagraphRegex.Match(text);
            } // while

            //
            // Parsing paragraphs
            Match paragraphMatch = ParagraphRegex.Match(text);
            if(paragraphMatch.Success)
            {
                if(paragraphMatch.Index != 0)
                {
                    string before = text.Substring(0, paragraphMatch.Index);
                    Parse(domElement, before);
                } // if

                Paragraph paragraph = new Paragraph(domElement, null);
                domElement.AppendChild(paragraph);

                Parse(paragraph, paragraphMatch.Groups["Text"].Value);

                if(paragraphMatch.Index + paragraphMatch.Length < text.Length)
                {
                    string after = text.Substring(paragraphMatch.Index + paragraphMatch.Length);
                    Parse(domElement, after);
                } // if

                return;
            } // if

            Match match = HeadingRegex.Match(text);
            if(match.Success)
            {
                if(match.Index != 0)
                {
                    string before = text.Substring(0, match.Index);
                    Parse(domElement, before);
                } // if

                domElement.AppendChild(new Heading(domElement, null, 1, match.Groups["Text"].Value));

                if(match.Index + match.Length < text.Length)
                {
                    string after = text.Substring(match.Index + match.Length);
                    Parse(domElement, after);
                } // if

                return;
            } // if

            domElement.AppendChild(new Dom.Text(domElement, text));
        }
    }
}
