using System;
using System.Text;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Defines a <see cref="IDomElementVisitor"/> which is capable of authoring HTML
    /// markup from Textile DOM.
    /// </summary>
    public class HtmlAuthoringDomElementVisitor : IDomElementVisitor
    {
        #region Private Member Variables
        private StringBuilder htmlBuilder = new StringBuilder();
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a <see cref="String"/> which contains the HTML markup.
        /// </summary>
        public String Html
        {
            get { return htmlBuilder.ToString(); }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="HtmlAuthoringDomElementVisitor"/> class.
        /// </summary>
        public HtmlAuthoringDomElementVisitor()
        {
        }

        #region IDomElementVisitor Members
        /// <summary>
        /// Visits the <paramref name="document"/> element.
        /// </summary>
        /// <param name="document"></param>
        public void Visit(Document document)
        {
            VisitChildElements(document);
        }

        /// <summary>
        /// Visits the <paramref name="heading"/> element.
        /// </summary>
        /// <param name="heading"></param>
        public void Visit(Heading heading)
        {
            String tag = String.Format("h{0}", heading.Level);
            htmlBuilder.AppendFormat("{0}{1}</{2}>", GetFullBlockStartTag(tag, heading.Attributes), heading.Text.Trim(),
                                     tag);
        }

        /// <summary>
        /// Visits the <paramref name="blockquote"/> element.
        /// </summary>
        /// <param name="blockquote"></param>
        public void Visit(Blockquote blockquote)
        {
            htmlBuilder.AppendFormat(GetFullBlockStartTag("blockquote", blockquote.Attributes));
            VisitChildElements(blockquote);
            htmlBuilder.Append("</blockquote>");
        }

        /// <summary>
        /// Visits the <paramref name="paragraph"/> element.
        /// <seealso cref="Paragraph"/>
        /// </summary>
        /// <param name="paragraph"></param>
        public void Visit(Paragraph paragraph)
        {
            htmlBuilder.AppendFormat(GetFullBlockStartTag("p", paragraph.Attributes));
            VisitChildElements(paragraph);
            htmlBuilder.Append("</p>");
        }

        /// <summary>
        /// Visits the <paramref name="hyperlink"/> element.
        /// </summary>
        /// <param name="hyperlink"></param>
        public void Visit(Hyperlink hyperlink)
        {
            htmlBuilder.AppendFormat("{0} title=\"{1}\" href=\"{2}\">{3}</{4}>",
                                     GetPartialPhraseStartTag("a", hyperlink.Attributes), hyperlink.Title.Trim(),
                                     hyperlink.Url.Trim(), hyperlink.InnerText.Trim(), "a");
        }

        /// <summary>
        /// Visits the <paramref name="image"/> elements.
        /// </summary>
        /// <param name="image"></param>
        public void Visit(Image image)
        {
            htmlBuilder.AppendFormat("{0} src=\"{1}\" alt=\"{2}\" />", GetPartialPhraseStartTag("img", image.Attributes),
                                     image.Url, image.AlternateText);
        }

        /// <summary>
        /// Visits the <paramref name="textBlock"/> element.
        /// </summary>
        /// <param name="textBlock"></param>
        public void Visit(TextBlock textBlock)
        {
            //
            // Determine tag
            String tag;
            switch(textBlock.Modifier)
            {
            case TextBlockModifier.StrongEmphasis:
                tag = "strong";
                break;
            case TextBlockModifier.Bold:
                tag = "b";
                break;
            case TextBlockModifier.Emphasis:
                tag = "em";
                break;
            case TextBlockModifier.Italics:
                tag = "i";
                break;
            case TextBlockModifier.Citation:
                tag = "cite";
                break;
            case TextBlockModifier.Deleted:
                tag = "del";
                break;
            case TextBlockModifier.Inserted:
                tag = "ins";
                break;
            case TextBlockModifier.Superscript:
                tag = "sup";
                break;
            case TextBlockModifier.Subscript:
                tag = "sub";
                break;
            case TextBlockModifier.Code:
                tag = "code";
                break;
            default:
                tag = "span";
                break;
            } // switch

            htmlBuilder.AppendFormat("{0}>{1}</{2}>", GetPartialPhraseStartTag(tag, textBlock.Attributes),
                textBlock.InnerText, tag);
        }

        /// <summary>
        /// Visits the <paramref name="acronym"/> element.
        /// </summary>
        /// <param name="acronym"></param>
        public void Visit(Acronym acronym)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Visits the <paramref name="footnote"/> element.
        /// </summary>
        /// <param name="footnote"></param>
        public void Visit(Footnote footnote)
        {
            htmlBuilder.AppendFormat(GetFullBlockStartTag("p", footnote.Attributes));
            
            htmlBuilder.AppendFormat("<sup>[<a href=\"#{0}\">{1}</a>]</sup>",
                GetFoonoteAnchorName(footnote.Number), footnote.Number);
            VisitChildElements(footnote);

            htmlBuilder.Append("</p>");
        }

        /// <summary>
        /// Visits the <paramref name="footnoteReference"/> element.
        /// </summary>
        /// <param name="footnoteReference"></param>
        public void Visit(FootnoteReference footnoteReference)
        {
            htmlBuilder.AppendFormat("<sup>[<a href=\"#{0}\">{1}</a>]</sup>", 
                GetFoonoteAnchorName(footnoteReference.Number), footnoteReference.Number);
        }

        /// <summary>
        /// Visits the <paramref name="orderedList"/> element.
        /// </summary>
        /// <param name="orderedList"></param>
        public void Visit(OrderedList orderedList)
        {
            htmlBuilder.AppendFormat(GetFullBlockStartTag("ol", orderedList.Attributes));
            VisitChildElements(orderedList);
            htmlBuilder.Append("</ol>");
        }

        /// <summary>
        /// Visits the <paramref name="unorderedList"/> element.
        /// </summary>
        /// <param name="unorderedList"></param>
        public void Visit(UnorderedList unorderedList)
        {
            htmlBuilder.AppendFormat(GetFullBlockStartTag("ul", unorderedList.Attributes));
            VisitChildElements(unorderedList);
            htmlBuilder.Append("</ul>");
        }

        /// <summary>
        /// Visits the <paramref name="listItem"/> element.
        /// </summary>
        /// <param name="listItem"></param>
        public void Visit(ListItem listItem)
        {
            htmlBuilder.AppendFormat(GetFullBlockStartTag("li", listItem.Attributes));
            VisitChildElements(listItem);
            htmlBuilder.Append("</li>");
        }
        #endregion

        #region Overridables
        /// <summary>
        /// Gets a string with the name of the HTML anchor (without leading #) which corresponds to the
        /// footnote with identifer <paramref name="footnoteID"/>
        /// </summary>
        /// <param name="footnoteID"></param>
        /// <returns></returns>
        protected virtual String GetFoonoteAnchorName(Int32 footnoteID)
        {
            return String.Format("__footnote{0}", footnoteID);
        }
        #endregion

        /// <summary>
        /// Visits all child elements of <paramref name="domElement"/>.
        /// </summary>
        /// <param name="domElement"></param>
        protected void VisitChildElements(DomElement domElement)
        {
            foreach(DomElement childElement in domElement.ChildElements)
                childElement.Accept(this);
        }

        /// <summary>
        /// Creates partial start tag of a form "&lt;<paramref name="tag"/> id="<see cref="InlineElementAttributes.ID"/>"
        /// class="<see cref="InlineElementAttributes.CssClass"/>" style="<see cref="InlineElementAttributes.Style"/>"
        /// lang="<see cref="InlineElementAttributes.Language"/>".
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        protected static String GetPartialBlockStartTag(String tag, BlockElementAttributes attributes)
        {
            StringBuilder tagBuilder = new StringBuilder(GetPartialPhraseStartTagWithoutStyle(tag, attributes));

            //
            // Now we need to construct a new "style" attribute since values
            // in Alignment, LeftIndent and RightIndent may add something new.
            // On the other hand, they may not be specified at all, so
            // this should be taken into consideration too.
            if(attributes.Alignment != BlockElementAlignment.Unknown ||
                attributes.LeftIndent != 0 || attributes.RightIndent != 0 ||
                !IsNullOrEmpty(attributes.Style))
            {
                String style = String.Empty;
                if(!IsNullOrEmpty(attributes.Style))
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

            return tagBuilder.ToString();
        }

        /// <summary>
        /// Creates a full start tag of a form "&lt;<paramref name="tag"/> id="<see cref="InlineElementAttributes.ID"/>"
        /// class="<see cref="InlineElementAttributes.CssClass"/>" style="<see cref="InlineElementAttributes.Style"/>"
        /// lang="<see cref="InlineElementAttributes.Language"/>">".
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        protected static String GetFullBlockStartTag(String tag, BlockElementAttributes attributes)
        {
            return GetPartialBlockStartTag(tag, attributes) + ">";
        }

        /// <summary>
        /// Creates a partial start tag of a form "&lt;<paramref name="tag"/> id="<see cref="InlineElementAttributes.ID"/>"
        /// class="<see cref="InlineElementAttributes.CssClass"/>" lang="<see cref="InlineElementAttributes.Language"/>"
        /// style="<see cref="InlineElementAttributes.Style"/>"".
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        protected static String GetPartialPhraseStartTag(String tag, InlineElementAttributes attributes)
        {
            StringBuilder tagBuilder = new StringBuilder(GetPartialPhraseStartTagWithoutStyle(tag, attributes));

            if(attributes != null)
            {
                if(!IsNullOrEmpty(attributes.Style))
                {
                    tagBuilder.AppendFormat(" style=\"{0}", attributes.Style);

                    if(!attributes.Style.EndsWith(";"))
                        tagBuilder.Append(";\"");
                    else
                        tagBuilder.Append("\"");
                } // if
            } // if

            return tagBuilder.ToString();
        }

        /// <summary>
        /// Creates a partial start tag of a form "&lt;<paramref name="tag"/> id="<see cref="InlineElementAttributes.ID"/>"
        /// class="<see cref="InlineElementAttributes.CssClass"/>" lang="<see cref="InlineElementAttributes.Language"/>"".
        /// <para />
        /// Note that <c>style</c> attribute is missing.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        protected static String GetPartialPhraseStartTagWithoutStyle(String tag, InlineElementAttributes attributes)
        {
            StringBuilder tagBuilder = new StringBuilder();
            tagBuilder.AppendFormat("<{0}", tag);

            if(attributes != null)
            {
                if(!IsNullOrEmpty(attributes.CssClass))
                    tagBuilder.AppendFormat(" class=\"{0}\"", attributes.CssClass);

                if(!IsNullOrEmpty(attributes.ID))
                    tagBuilder.AppendFormat(" id=\"{0}\"", attributes.ID);

                if(!IsNullOrEmpty(attributes.Language))
                    tagBuilder.AppendFormat(" lang=\"{0}\"", attributes.Language);
            } // if

            return tagBuilder.ToString();
        }

		protected static bool IsNullOrEmpty(String value)
		{
			return value == null || value == string.Empty;
		}
    }
}
