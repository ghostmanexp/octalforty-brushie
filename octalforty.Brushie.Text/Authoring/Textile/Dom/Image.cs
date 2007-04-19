using System;

namespace octalforty.Brushie.Text.Authoring.Textile.Dom
{
    /// <summary>
    /// Represents an image.
    /// </summary>
    public sealed class Image : InlineElement
    {
        #region Private Member Variables
        private String url;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a <see cref="String"/> which contains the URL of the image.
        /// </summary>
        public String Url
        {
            get { return url; }
        }
        #endregion
        
        /// <summary>
        /// Initializes a new instance of <see cref="Image"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attributes"></param>
        /// <param name="innerText"></param>
        /// <param name="url"></param>
        public Image(DomElement parent, InlineElementAttributes attributes, string innerText, string url) : 
            base(parent, attributes, innerText)
        {
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
