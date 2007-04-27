using System;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Represents a single row of a <see cref="Table"/>.
    /// </summary>
    public sealed class TableRow : BlockElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TableRow"/> class.
        /// </summary>
        public TableRow()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TableRow"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attributes"></param>
        public TableRow(DomElement parent, BlockElementAttributes attributes) : 
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
