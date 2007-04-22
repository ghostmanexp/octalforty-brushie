using System;

namespace octalforty.Brushie.Text.Authoring.Textile.Dom
{
    /// <summary>
    /// Represents a block of text.
    /// </summary>
    public sealed class TextBlock : InlineElement
    {
        #region Private Member Variables
        private TextBlockModifier modifier = TextBlockModifier.Unknown;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value which indicates how the style of the text block is altered.
        /// </summary>
        public TextBlockModifier Modifier
        {
            get { return modifier; }
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
        /// <param name="modifier"></param>
        public TextBlock(DomElement parent, InlineElementAttributes attributes, 
            string innerText, TextBlockModifier modifier) : 
            base(parent, attributes, innerText)
        {
            this.modifier = modifier;
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
