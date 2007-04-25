using System;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Represents a footnote reference.
    /// <seealso cref="Footnote"/>
    /// </summary>
    /// <remarks>
    /// Footnote references reference <see cref="Footnote"/>.
    /// </remarks>
    public sealed class FootnoteReference : InlineElement
    {
        #region Private Member Variables
        private Int32 number;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value which indicates the number of the referenced footnote.
        /// </summary>
        public Int32 Number
        {
            get { return number; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="FootnoteReference"/> class.
        /// </summary>
        public FootnoteReference()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="FootnoteReference"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="number"></param>
        public FootnoteReference(DomElement parent, Int32 number) : 
            base(parent, String.Empty)
        {
            this.number = number;
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