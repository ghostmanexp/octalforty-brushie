using System;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Represents a block of text.
    /// </summary>
    public sealed class TextBlock : InlineElement
    {
        #region Private Member Variables
        private TextBlockFormatting formatting = TextBlockFormatting.Unknown;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value which indicates how the formatting of the text block is altered.
        /// </summary>
        public TextBlockFormatting Formatting
        {
            get { return formatting; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="TextBlock"/> class.
        /// </summary>
        public TextBlock()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextBlock"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attributes"></param>
        /// <param name="innerText"></param>
        /// <param name="formatting"></param>
        public TextBlock(DomElement parent, InlineElementAttributes attributes, 
            string innerText, TextBlockFormatting formatting) : 
            base(parent, attributes, innerText)
        {
            this.formatting = formatting;
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