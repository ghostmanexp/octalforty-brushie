namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Represents a single block-level Textile element, such as paragraph or blockquote.
    /// </summary>
    public abstract class BlockElement : DomElement
    {
        #region Private Member Variables
        private BlockElementAttributes attributes = BlockElementAttributes.Empty;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a reference to the <see cref="BlockElementAttributes"/>, which contains
        /// attributes of this <see cref="BlockElement"/>.
        /// </summary>
        public virtual BlockElementAttributes Attributes
        {
            get { return attributes; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="BlockElement"/> class.
        /// </summary>
        protected BlockElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BlockElement"/> class with given properties.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attributes"></param>
        protected BlockElement(DomElement parent, BlockElementAttributes attributes) : 
            base(parent)
        {
            this.attributes = attributes;
        }
    }
}