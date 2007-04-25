using System;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Represents a footnote.
    /// <seealso cref="FootnoteReference"/>
    /// </summary>
    /// <remarks>
    /// Footnotes are referenced by <see cref="FootnoteReference"/>.
    /// </remarks>
    public sealed class Footnote : BlockElement
    {
        #region Private Member Variables
        private Int32 number;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value which indicates the number of the footnote.
        /// </summary>
        public Int32 Number
        {
            get { return number; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Footnote"/> class.
        /// </summary>
        public Footnote()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Footnote"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attributes"></param>
        /// <param name="number"></param>
        public Footnote(DomElement parent, BlockElementAttributes attributes, Int32 number) :
            base(parent, attributes)
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