using System.Text.RegularExpressions;

namespace octalforty.Brushie.Text.Authoring
{
    /// <summary>
    /// Authors text using Textile markup.
    /// </summary>
    public static class Textile
    {
        #region Private Static Constants
        private static readonly Regex headersRegex =
            new Regex(@"(?<Header>^h(?<Level>\d)\.\s(?<Text>.*)$)", RegexOptions.Multiline);
        private static readonly Regex boldRegex =
            new Regex(@"(?<Expression>(?<!\\)\*(?<InnerText>(.+?))(?<!\\)\*)", RegexOptions.Compiled);
        private static readonly Regex italicsRegex =
            new Regex(@"(?<Expression>(?<!\\)_(?<InnerText>(.+?))(?<!\\)_)", RegexOptions.Compiled);
        private static Regex linkRegex =
            new Regex(@"(?<!\\)(?<Expression>\[((?<Alias>(.+?))\|)?(?<Uri>(.+?))(\|(?<Tip>(.+?)))?(?<!\\)\])",
                RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Fully authors Textile markup.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Author(string content)
        {
            content = AuthorHeaders(content);
            content = AuthorTextBreaks(content);
            content = AuthorTextFormatting(content);
            content = AuthorLinks(content);

            return content;
        }

        /// <summary>
        /// Authors Textile headings and converts them to appropriate HTML
        /// <c>h1, h2</c> etc. tags.
        /// </summary>
        /// <param name="content">Content to be authored.</param>
        /// <returns></returns>
        public static string AuthorHeaders(string content)
        {
            Match match = headersRegex.Match(content);
            while(match.Success)
            {
                string expression = match.Groups["Header"].Value;
                string text = match.Groups["Text"].Value.Trim(' ', '\n', '\t');

                int level = int.Parse(match.Groups["Level"].Value);

                content = content.Replace(expression,
                    string.Format("<h{0}>{1}</h{0}>", level, text));

                match = headersRegex.Match(content);
            } // while

            return content;
        }

        /// <summary>
        /// Authors various text formatting tags, such as bold and italics.
        /// </summary>
        /// <param name="content">Content to be authored.</param>
        /// <returns></returns>
        public static string AuthorTextFormatting(string content)
        {
            content = AuthorBold(content);
            content = AuthorItalics(content);

            return content;
        }

        /// <summary>
        /// Authors bold text.
        /// </summary>
        /// <param name="content">Content to be authored.</param>
        /// <returns></returns>
        public static string AuthorBold(string content)
        {
            Match match = boldRegex.Match(content);
            while(match.Success)
            {
                string expression = match.Groups["Expression"].Value;
                string innerText = match.Groups["InnerText"].Value.Trim(' ', '\n', '\t');

                content = content.Replace(expression,
                    string.Format("<strong>{0}</strong>", innerText));

                match = boldRegex.Match(content);
            } // while

            content.Replace("\\*", "*");

            return content;
        }

        /// <summary>
        /// Authors text in italics.
        /// </summary>
        /// <param name="content">Content to be authored.</param>
        /// <returns></returns>
        public static string AuthorItalics(string content)
        {
            Match match = italicsRegex.Match(content);
            while(match.Success)
            {
                string expression = match.Groups["Expression"].Value;
                string innerText = match.Groups["InnerText"].Value.Trim(' ', '\n', '\t');

                content = content.Replace(expression,
                    string.Format("<em>{0}</em>", innerText));

                match = italicsRegex.Match(content);
            } // while

            content.Replace("\\_", "_");

            return content;
        }

        /// <summary>
        /// Authors text breaks.
        /// </summary>
        /// <param name="content">Content to be authored.</param>
        /// <returns></returns>
        public static string AuthorTextBreaks(string content)
        {
            content = content.Replace("\r\n\r\n", "<p />");
            content = content.Replace("\r\n", "<br />");

            return content;
        }

        /// <summary>
        /// Authors links.
        /// </summary>
        /// <param name="content">Content to be authored.</param>
        /// <returns></returns>
        public static string AuthorLinks(string content)
        {
            Match match = linkRegex.Match(content);
            while(match.Success)
            {
                string expression = match.Groups["Expression"].Value;

                string alias = match.Groups["Alias"].Value;
                string uri = match.Groups["Uri"].Value;
                string tip = match.Groups["Tip"].Value;

                content = content.Replace(expression,
                    string.Format("<a href=\"{0}\" title=\"{1}\">{2}</em>",
                        uri, tip, alias));

                match = linkRegex.Match(content);
            } // while

            content.Replace("\\[", "[");
            content.Replace("\\]", "]");

            return content;
        }
    }
}