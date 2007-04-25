using System;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Represents a link.
    /// </summary>
    public sealed class Hyperlink : InlineElement
    {
        #region Private Member Variables
        private String title;
        private String url;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a <see cref="String"/> which contains the title of the hyperlink.
        /// </summary>
        public String Title
        {
            get { return title; }
        }

        /// <summary>
        /// Gets a <see cref="String"/> which contains the URL of the hyperlink.
        /// </summary>
        public String Url
        {
            get { return url; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Hyperlink"/> class.
        /// </summary>
        public Hyperlink()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="Hyperlink"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="innerText"></param>
        /// <param name="title"></param>
        /// <param name="url"></param>
        public Hyperlink(DomElement parent, String innerText, String title, String url) : 
            base(parent, innerText)
        {
            this.title = title;
            this.url = url;
        }

        #region DomElement Members
        /// <summary>
        /// Accepts a <paramref name="domElementVisitor"/>.
        /// </summary>
        /// <param name="domElementVisitor">DOM element visitor.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="domElementVisitor"/> is a <c>null</c> reference.
        /// </exception>
        public override void Accept(IDomElementVisitor domElementVisitor)
        {
            domElementVisitor.Visit(this);
        }
        #endregion
    }
}