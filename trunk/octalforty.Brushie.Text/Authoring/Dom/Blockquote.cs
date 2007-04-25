using System;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Represents a blockquote.
    /// </summary>
    public sealed class Blockquote : BlockElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Blockquote"/> class.
        /// </summary>
        public Blockquote()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Blockquote"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attributes"></param>
        public Blockquote(DomElement parent, BlockElementAttributes attributes) : 
            base(parent, attributes)
        {
        }

        #region BlockElement Members
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