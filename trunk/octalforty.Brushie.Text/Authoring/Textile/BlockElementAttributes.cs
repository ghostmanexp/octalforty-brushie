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
        /// <param name="cssClass"></param>
        /// <param name="id"></param>
        /// <param name="style"></param>
        /// <param name="language"></param>
        /// <param name="alignment"></param>
        /// <param name="leftIndent"></param>
        /// <param name="rightIndent"></param>
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
