using System;
using System.Text;
using System.Text.RegularExpressions;

namespace octalforty.Brushie.Text.Authoring
{
    /// <summary>
    /// Authors text using Textile markup.
    /// </summary>
    /// <remarks>
    /// This class allows for conversion of Textile-formatted text to (possibly) any other
    /// format, including HTML (which is the default mode).<para />
    /// In order to be able to convert Textile-formatted text to any other format, inherit
    /// from <see cref="Textile"/> and override the required <c>OnXXX</c> methods.
    /// </remarks>
    public class Textile
    {
        #region Private Static Constants
        private static readonly Regex headersRegex =
            new Regex(@"(?<Header>^h(?<Level>\d)\.\s(?<Text>.*)$)", RegexOptions.Multiline);

        private static readonly Regex boldRegex =
            new Regex(@"(?<Expression>(?<!\\)\*(?<InnerText>(.+?))(?<!\\)\*)",
                      RegexOptions.Compiled);

        private static readonly Regex italicsRegex =
            new Regex(@"(?<Expression>(?<!\\)_(?<InnerText>(.+?))(?<!\\)_)",
                      RegexOptions.Compiled);

        private static Regex linkRegex =
            new Regex(
                @"(?<!\\)(?<Expression>\[((?<Alias>(.+?))\|)?(?<Uri>(.+?))(\|(?<Tip>(.+?)))?(?<!\\)\])",
                RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Textile"/> class.
        /// </summary>
        public Textile()
        {
        }

        /// <summary>
        /// Authors Textile <paramref name="source"/> and returns fully formatted text.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public String Author(String source)
        {
            return Author(source, AuthoringScope.All);
        }

        /// <summary>
        /// Authors Textile <paramref name="source"/> and returns text which is
        /// formatted according to <paramref name="authoringScope"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="authoringScope"></param>
        /// <returns></returns>
        public String Author(String source, AuthoringScope authoringScope)
        {
            return source;
        }

        #region Overridables
        /// <summary>
        /// When overriden in a derived class, returns an <see cref="Enclosure"/> object
        /// which contains markup used to enclose emphasized text, optionally replacing
        /// the original <paramref name="text"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="style"></param>
        /// <param name="language"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual Enclosure OnEmphasis(String id, String cssClass, String style,
            String language, String text)
        {
            return CreateEnclosure("em", id, cssClass, style, language);
        }

        /// <summary>
        /// When overriden in a derived class, returns an <see cref="Enclosure"/> object
        /// which contains markup used to enclose strongly emphasized text.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="style"></param>
        /// <param name="language"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual Enclosure OnStrongEmphasis(String id, String cssClass, String style,
            String language, String text)
        {
            return CreateEnclosure("strong", id, cssClass, style, language);
        }

        /// <summary>
        /// When overriden in a derived class, returns an <see cref="Enclosure"/> object
        /// which contains markup used to enclose italicized text.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="style"></param>
        /// <param name="language"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual Enclosure OnItalics(String id, String cssClass, String style,
            String language, String text)
        {
            return CreateEnclosure("i", id, cssClass, style, language);
        }

        /// <summary>
        /// When overriden in a derived class, returns an <see cref="Enclosure"/> object
        /// which contains markup used to enclose emboldened text.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="style"></param>
        /// <param name="language"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual Enclosure OnBold(String id, String cssClass, String style,
            String language, String text)
        {
            return CreateEnclosure("b", id, cssClass, style, language);
        }

        /// <summary>
        /// When overriden in a derived class, returns an <see cref="Enclosure"/> object
        /// which contains markup used to enclose cited text.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="style"></param>
        /// <param name="language"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual Enclosure OnCitation(String id, String cssClass, String style,
            String language, String text)
        {
            return CreateEnclosure("cite", id, cssClass, style, language);
        }

        /// <summary>
        /// When overriden in a derived class, returns an <see cref="Enclosure"/> object
        /// which contains markup used to enclose deleted text.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="style"></param>
        /// <param name="language"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual Enclosure OnDeleted(String id, String cssClass, String style,
            String language, String text)
        {
            return CreateEnclosure("del", id, cssClass, style, language);
        }

        /// <summary>
        /// When overriden in a derived class, returns an <see cref="Enclosure"/> object
        /// which contains markup used to enclose inserted text.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="style"></param>
        /// <param name="language"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual Enclosure OnInserted(String id, String cssClass, String style,
            String language, String text)
        {
            return CreateEnclosure("ins", id, cssClass, style, language);
        }

        /// <summary>
        /// When overriden in a derived class, returns an <see cref="Enclosure"/> object
        /// which contains markup used to enclose text in superscript.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="style"></param>
        /// <param name="language"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual Enclosure OnSuperscript(String id, String cssClass, String style,
            String language, String text)
        {
            return CreateEnclosure("sup", id, cssClass, style, language);
        }

        /// <summary>
        /// When overriden in a derived class, returns an <see cref="Enclosure"/> object
        /// which contains markup used to enclose text in subscript.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="style"></param>
        /// <param name="language"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual Enclosure OnSubscript(String id, String cssClass, String style,
            String language, String text)
        {
            return CreateEnclosure("sub", id, cssClass, style, language);
        }

        /// <summary>
        /// When overriden in a derived class, returns an <see cref="Enclosure"/> object
        /// which contains markup used to enclose a span of text.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="style"></param>
        /// <param name="language"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual Enclosure OnSpan(String id, String cssClass, String style,
            String language, String text)
        {
            return CreateEnclosure("span", id, cssClass, style, language);
        }
        #endregion

        /// <summary>
        /// Creates an enlosure of a form "&lt;<paramref name="tag"/> id="<paramref name="id"/>"
        /// class="<paramref name="cssClass"/>" style="<paramref name="style"/>"
        /// lang="<paramref name="language"/>">".
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="id"></param>
        /// <param name="cssClass"></param>
        /// <param name="style"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        protected static Enclosure CreateEnclosure(String tag, String id, String cssClass, String style,
            String language)
        {
            StringBuilder tagBuilder = new StringBuilder();
            tagBuilder.AppendFormat("<{0}", tag);

            if(!String.IsNullOrEmpty(id))
                tagBuilder.AppendFormat(" id=\"{0}\"", id);

            if(!String.IsNullOrEmpty(cssClass))
                tagBuilder.AppendFormat(" class=\"{0}\"", cssClass);

            if(!String.IsNullOrEmpty(style))
                tagBuilder.AppendFormat(" style=\"{0}\"", style);

            if(!String.IsNullOrEmpty(language))
                tagBuilder.AppendFormat(" lang=\"{0}\"", language);

            tagBuilder.Append(">");

            return new Enclosure(tagBuilder.ToString(), String.Format(""));
        }

        /// <summary>
        /// Fully authors Textile markup.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        /*public static string Author(string content)
        {
            content = AuthorHeaders(content);
            content = AuthorTextBreaks(content);
            content = AuthorTextFormatting(content);
            content = AuthorLinks(content);

            return content;
        }*/

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