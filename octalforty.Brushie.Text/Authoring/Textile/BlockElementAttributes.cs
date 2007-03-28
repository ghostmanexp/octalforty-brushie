using System;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Defines block-level Textile elements attributes.
    /// </summary>
    public class BlockElementAttributes : PhraseElementAttributes
    {
        #region Private Member Variables
        private BlockElementAlignment alignment;
        private Int32 leftIndent;
        private Int32 rightIndent;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the <see cref="BlockElementAlignment"/> member, which defines the
        /// alignment mode for the current block element.
        /// </summary>
        public BlockElementAlignment Alignment
        {
            get { return alignment; }
        }

        /// <summary>
        /// Gets a value, which indicates the level of left indent of current
        /// block element (in <c>em</c>s).
        /// </summary>
        public Int32 LeftIndent
        {
            get { return leftIndent; }
        }

        /// <summary>
        /// Gets a value, which indicates the level of right indent of current
        /// block element (in <c>em</c>s).
        /// </summary>
        public Int32 RightIndent
        {
            get { return rightIndent; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="BlockElementAttributes"/> class
        /// with a given set of attributes.
        /// </summary>
        /// <param name="cssClass">Element CSS class.</param>
        /// <param name="id">Element ID.</param>
        /// <param name="style">Inline element style.</param>
        /// <param name="language">Element language specification.</param>
        /// <param name="alignment">Element alignment.</param>
        /// <param name="leftIndent">Element left indent (in <c>em</c>s).</param>
        /// <param name="rightIndent">Element right indent (in <c>em</c>s).</param>
        public BlockElementAttributes(string cssClass, string id, string style, string language,
            BlockElementAlignment alignment, Int32 leftIndent, Int32 rightIndent) : 
            base(cssClass, id, style, language)
        {
            this.alignment = alignment;
            this.leftIndent = leftIndent;
            this.rightIndent = rightIndent;
        }
    }
}
