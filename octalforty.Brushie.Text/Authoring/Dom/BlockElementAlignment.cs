namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Defines the alignment of a block-level Textile element.
    /// <seealso cref="BlockElement"/>
    /// <seealso cref="BlockElementAttributes"/>
    /// </summary>
    public enum BlockElementAlignment
    {
        /// <summary>
        /// Alignment is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Left-aligned block.
        /// </summary>
        Left = 1,

        /// <summary>
        /// Right-aligned block.
        /// </summary>
        Right = 2,

        /// <summary>
        /// Centered block.
        /// </summary>
        Center = 3,

        /// <summary>
        /// Justified block.
        /// </summary>
        Justify = 4
    }
}