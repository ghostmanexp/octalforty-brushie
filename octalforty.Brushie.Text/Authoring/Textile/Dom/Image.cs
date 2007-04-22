using System;

namespace octalforty.Brushie.Text.Authoring.Textile.Dom
{
    /// <summary>
    /// Represents an image.
    /// </summary>
    public sealed class Image : BlockElement
    {
        #region Private Member Variables
        private String url;
        private String alternateText;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a <see cref="String"/> which contains the URL of the image.
        /// </summary>
        public String Url
        {
            get { return url; }
        }

        /// <summary>
        /// Gets a <see cref="String"/> which contains the alternate text for the image.
        /// </summary>
        public String AlternateText
        {
            get { return alternateText; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Image"/> class.
        /// </summary>
        public Image()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Image"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attributes"></param>
        /// <param name="url"></param>
        /// <param name="alternateText"></param>
        public Image(DomElement parent, BlockElementAttributes attributes, string url, string alternateText) : 
            base(parent, attributes)
        {
            this.url = url;
            this.alternateText = alternateText;
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
