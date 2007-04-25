using System;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Represents a single inline Textile element.
    /// </summary>
    public abstract class InlineElement : DomElement
    {
        #region Private Member Variables
        private InlineElementAttributes attributes = InlineElementAttributes.Empty;
        private String innerText;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a reference to the <see cref="InlineElementAttributes"/> which contains
        /// the attributes for this <see cref="InlineElement"/>.
        /// </summary>
        public virtual InlineElementAttributes Attributes
        {
            get { return attributes; }
        }

        /// <summary>
        /// Gets a <see cref="String"/> which contains the text of this <see cref="InlineElement"/>.
        /// </summary>
        public virtual String InnerText
        {
            get { return innerText; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="InlineElement"/> class.
        /// </summary>
        protected InlineElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InlineElement"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="innerText"></param>
        protected InlineElement(DomElement parent, String innerText) :
            this(parent, InlineElementAttributes.Empty, innerText)
        {
            this.innerText = innerText;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InlineElement"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attributes"></param>
        /// <param name="innerText"></param>
        protected InlineElement(DomElement parent, InlineElementAttributes attributes, String innerText) : 
            base(parent)
        {
            this.attributes = attributes;
            this.innerText = innerText;
        }
    }
}