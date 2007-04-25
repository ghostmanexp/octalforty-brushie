using System;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Represents a heading.
    /// </summary>
    public sealed class Heading : BlockElement
    {
        #region Private Member Variables
        private Int32 level;
        private String text;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value which indicates the absolute level of the <see cref="Heading"/>.
        /// </summary>
        public Int32 Level
        {
            get { return level; }
        }

        /// <summary>
        /// Gets a <see cref="String"/> which contains the text of this <see cref="Heading"/>.
        /// </summary>
        public String Text
        {
            get { return text; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Heading"/> class.
        /// </summary>
        public Heading()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Heading"/> class/
        /// </summary>
        /// <param name="parent">The parent of this <see cref="Heading"/>.</param>
        /// <param name="attributes"><see cref="BlockElementAttributes"/> of this <see cref="Heading"/>.</param>
        /// <param name="level">The value which contains the absolute level of this <see cref="Heading"/>.</param>
        /// <param name="text">The text of this <see cref="Heading"/>.</param>
        public Heading(DomElement parent, BlockElementAttributes attributes, Int32 level, String text) : 
            base(parent, attributes)
        {
            this.level = level;
            this.text = text;
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