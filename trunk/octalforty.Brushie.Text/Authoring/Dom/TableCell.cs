using System;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Represents a single cell of a <see cref="TableRow"/>.
    /// </summary>
    public class TableCell : BlockElement
    {
        #region Private Member Variables
        private Int32 columnSpan;
        private Int32 rowSpan;
        private TableCellAlignment alignment;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value which contains the number of columns in the <see cref="Table"/> that 
        /// this <see cref="TableCell"/> should span.
        /// </summary>
        public Int32 ColumnSpan
        {
            get { return columnSpan; }
        }

        /// <summary>
        /// Gets or sets a value which contains the number of rows in the <see cref="Table"/> that 
        /// this <see cref="TableCell"/> should span.
        /// </summary>
        public Int32 RowSpan
        {
            get { return rowSpan; }
        }

        /// <summary>
        /// Gets a value which indicates how the content of this cell is aligned.
        /// </summary>
        public TableCellAlignment Alignment
        {
            get { return alignment; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="TableCell"/> class.
        /// </summary>
        public TableCell()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TableCell"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attributes"></param>
        public TableCell(DomElement parent, BlockElementAttributes attributes) : 
            base(parent, attributes)
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
