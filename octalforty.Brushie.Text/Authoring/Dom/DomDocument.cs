using System;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Represents a Textile document.
    /// </summary>
    public sealed class DomDocument : DomElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DomDocument"/> class.
        /// </summary>
        public DomDocument()
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