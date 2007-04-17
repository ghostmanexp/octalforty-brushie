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

        public string Html
        {
            get { return htmlBuilder.ToString(); }
        }

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
            htmlBuilder.AppendFormat("<h{0}>{1}</h{0}>", heading.Level, heading.Text);
        }

        /// <summary>
        /// Visits the <paramref name="blockquote"/> element.
        /// </summary>
        /// <param name="blockquote"></param>
        public void Visit(Blockquote blockquote)
        {
            htmlBuilder.Append("<blockquote>");
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
            htmlBuilder.Append("<p>");
            VisitChildElements(paragraph);
            htmlBuilder.Append("</p>");
        }

        /// <summary>
        /// Visits the <paramref name="hyperlink"/> element.
        /// </summary>
        /// <param name="hyperlink"></param>
        public void Visit(Hyperlink hyperlink)
        {
            htmlBuilder.AppendFormat("<a href\"{0}\">{1}</a>", hyperlink.Url, hyperlink.InnerText);
        }

        /// <summary>
        /// Visits the <paramref name="textBlock"/> element.
        /// </summary>
        /// <param name="textBlock"></param>
        public void Visit(TextBlock textBlock)
        {
            htmlBuilder.AppendFormat("<ins>{0}</ins>", textBlock.InnerText);
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
    }
}
