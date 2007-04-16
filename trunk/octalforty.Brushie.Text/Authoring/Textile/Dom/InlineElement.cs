using System;

namespace octalforty.Brushie.Text.Authoring.Textile.Dom
{
    /// <summary>
    /// Represents a single inline Textile element.
    /// </summary>
    public abstract class InlineElement : DomElement
    {
        #region Private Member Variables
        private PhraseElementAttributes attributes;
        private String innerText;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a reference to the <see cref="PhraseElementAttributes"/> which contains
        /// the attributes for this <see cref="InlineElement"/>.
        /// </summary>
        public virtual PhraseElementAttributes Attributes
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
        /// <param name="parent"></param>
        /// <param name="innerText"></param>
        protected InlineElement(DomElement parent, string innerText) : 
            base(parent)
        {
            this.innerText = innerText;
        }
    }
}
