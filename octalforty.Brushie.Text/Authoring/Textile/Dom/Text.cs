using System;

namespace octalforty.Brushie.Text.Authoring.Textile.Dom
{
    public sealed class Text : InlineElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Text"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="innerText"></param>
        public Text(DomElement parent, string innerText) :
            base(parent, innerText)
        {
        }

        #region DomElementMembers
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
