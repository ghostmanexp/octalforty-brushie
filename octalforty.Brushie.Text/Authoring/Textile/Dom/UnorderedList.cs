namespace octalforty.Brushie.Text.Authoring.Textile.Dom
{
    /// <summary>
    /// Represents an unordered list.
    /// </summary>
    public sealed class UnorderedList : List
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnorderedList"/> class.
        /// </summary>
        public UnorderedList()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="UnorderedList"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attributes"></param>
        public UnorderedList(DomElement parent, BlockElementAttributes attributes) : 
            base(parent, attributes)
        {
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
