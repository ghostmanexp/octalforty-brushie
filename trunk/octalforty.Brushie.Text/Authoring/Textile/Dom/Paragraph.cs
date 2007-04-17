using System;

namespace octalforty.Brushie.Text.Authoring.Textile.Dom
{
    /// <summary>
    /// Represents a paragraph.
    /// </summary>
    public sealed class Paragraph : BlockElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Paragraph"/> class.
        /// </summary>
        public Paragraph()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Paragraph"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attributes"></param>
        public Paragraph(DomElement parent, BlockElementAttributes attributes) : 
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
