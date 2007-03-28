﻿using System;
using System.Text;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Textile authoring formatter, which produces HTML output.
    /// </summary>
    public class HtmlTextileAuthoringFormatter : ITextileAuthoringFormatter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ITextileAuthoringFormatter"/> class.
        /// </summary>
        public HtmlTextileAuthoringFormatter()
        {
        }

        #region ITextileAuthoringFormatter Members
        /// <summary>
        /// Formats a heading of a given level with provided attributes.
        /// </summary>
        /// <param name="level">Heading level.</param>
        /// <param name="text">Heading text.</param>
        /// <param name="attributes">Attributes of the heading block element.</param>
        /// <returns></returns>
        public String FormatHeading(Int32 level, String text, BlockElementAttributes attributes)
        {
            String tag = String.Format("h{0}", level);
            return String.Format("{0}{1}</{2}>", GetFullBlockStartTag(tag, attributes), text.Trim(), tag);
        }

        /// <summary>
        /// Formats a blockquote with provided attribute.
        /// </summary>
        /// <param name="text">Blockquote text.</param>
        /// <param name="attributes">Attributes of the blockquote block element.</param>
        /// <returns></returns>
        public String FormatBlockquote(string text, BlockElementAttributes attributes)
        {
            const String tag = "blockquote";
            return String.Format("{0}{1}</{2}>", GetFullBlockStartTag(tag, attributes), text.Trim(), tag);
        }

        /// <summary>
        /// Formats a hyperlink with given text, title, URL and attributes.
        /// </summary>
        /// <param name="text">
        /// Hyperlink text (the text that appears between <c>a</c> tags.
        /// </param>
        /// <param name="title">
        /// Hyperlink title (the text that appears in <c>title</c> attribute).
        /// </param>
        /// <param name="url">
        /// Hyperlink URL (the text that appears in <c>href</c> attribute).
        /// </param>
        /// <param name="attributes">Attributes of the hyperlink.</param>
        /// <returns></returns>
        public String FormatHyperlink(string text, string title, string url, PhraseElementAttributes attributes)
        {
            const String tag = "a";
            return String.Format("{0} title=\"{1}\" href=\"{2}\">{3}</{4}>", 
                GetPartialPhraseStartTag(tag, attributes),
                title.Trim(), url.Trim(), text.Trim(), tag);
        }

        /// <summary>
        /// Formats a text formatting block, as defined by <paramref name="formatting"/>.
        /// </summary>
        /// <param name="formatting">Text formatting.</param>
        /// <param name="text">The text.</param>
        /// <param name="attributes">Attributes of the phrase element.</param>
        /// <returns></returns>
        public string FormatTextFormatting(TextFormatting formatting, string text, PhraseElementAttributes attributes)
        {
            //
            // Determine tag
            String tag = String.Empty;
            switch(formatting)
            {
                case TextFormatting.StrongEmphasis:
                    tag = "strong";
                    break;
                case TextFormatting.Bold:
                    tag = "b";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("formatting");
            } // switch

            return String.Format("{0}>{1}</{2}>", GetPartialPhraseStartTag(tag, attributes), text.Trim(), tag);
        }
        #endregion

        /// <summary>
        /// Creates a full start tag of a form "&lt;<paramref name="tag"/> id="<see cref="BlockElementAttributes.ID"/>"
        /// class="<see cref="BlockElementAttributes.CssClass"/>" style="<see cref="BlockElementAttributes.Style"/>"
        /// lang="<see cref="BlockElementAttributes.Language"/>">".
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        protected static String GetFullBlockStartTag(String tag, BlockElementAttributes attributes)
        {
            StringBuilder tagBuilder = new StringBuilder(GetPartialPhraseStartTagWithoutStyle(tag, attributes));

            //
            // Now we need to construct a new "style" attribute since values
            // in Alignment, LeftIndent and RightIndent may add something new.
            // On the other hand, they may not be specified at all, so
            // this should be taken into consideration too.
            if(attributes.Alignment != BlockElementAlignment.Unknown ||
                attributes.LeftIndent != 0 || attributes.RightIndent != 0 ||
                !String.IsNullOrEmpty(attributes.Style))
            {
                String style = String.Empty;
                if(!String.IsNullOrEmpty(attributes.Style))
                {
                    style += attributes.Style;
                    if(!style.EndsWith(";"))
                        style += ";";
                } // if

                if(attributes.Alignment != BlockElementAlignment.Unknown)
                {
                    style += String.Format("text-align: {0};",
                        attributes.Alignment.ToString().ToLower());
                } // if

                if(attributes.LeftIndent != 0)
                {
                    style += String.Format("padding-left: {0}em;",
                        attributes.LeftIndent);
                } // if

                if(attributes.RightIndent != 0)
                {
                    style += String.Format("padding-right: {0}em;",
                        attributes.RightIndent);
                } // if
                
                tagBuilder.AppendFormat(" style=\"{0}\"", style);
            } // if

            tagBuilder.Append(">");
            return tagBuilder.ToString();
        }

        /// <summary>
        /// Creates a partial start tag of a form "&lt;<paramref name="tag"/> id="<see cref="PhraseElementAttributes.ID"/>"
        /// class="<see cref="PhraseElementAttributes.CssClass"/>" lang="<see cref="PhraseElementAttributes.Language"/>"
        /// style="<see cref="PhraseElementAttributes.Style"/>"".
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private static String GetPartialPhraseStartTag(String tag, PhraseElementAttributes attributes)
        {
            StringBuilder tagBuilder = new StringBuilder(GetPartialPhraseStartTagWithoutStyle(tag, attributes));

            if(!String.IsNullOrEmpty(attributes.Style))
            {
                tagBuilder.AppendFormat(" style=\"{0}", attributes.Style);

                if(!attributes.Style.EndsWith(";"))
                    tagBuilder.Append(";\"");
                else
                    tagBuilder.Append("\"");
            } // if

            return tagBuilder.ToString();
        }

        /// <summary>
        /// Creates a partial start tag of a form "&lt;<paramref name="tag"/> id="<see cref="BlockElementAttributes.ID"/>"
        /// class="<see cref="BlockElementAttributes.CssClass"/>" lang="<see cref="BlockElementAttributes.Language"/>"".
        /// <para />
        /// Note that <c>style</c> attribute is missing.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private static String GetPartialPhraseStartTagWithoutStyle(String tag, PhraseElementAttributes attributes)
        {
            StringBuilder tagBuilder = new StringBuilder();
            tagBuilder.AppendFormat("<{0}", tag);

            if(!String.IsNullOrEmpty(attributes.CssClass))
                tagBuilder.AppendFormat(" class=\"{0}\"", attributes.CssClass);

            if(!String.IsNullOrEmpty(attributes.ID))
                tagBuilder.AppendFormat(" id=\"{0}\"", attributes.ID);

            if(!String.IsNullOrEmpty(attributes.Language))
                tagBuilder.AppendFormat(" lang=\"{0}\"", attributes.Language);
            
            return tagBuilder.ToString();
        }
    }
}
