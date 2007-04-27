using System;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Represents a table.
    /// <see cref="TableRow"/>
    /// <see cref="TableCell"/>
    /// </summary>
    public sealed class Table : BlockElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Table"/> class.
        /// </summary>
        public Table()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Table"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attributes"></param>
        public Table(DomElement parent, BlockElementAttributes attributes) : 
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
