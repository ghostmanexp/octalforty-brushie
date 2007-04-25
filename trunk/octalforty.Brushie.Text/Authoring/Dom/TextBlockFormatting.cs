namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Defines formatting for the <see cref="TextBlock"/>.
    /// </summary>
    public enum TextBlockFormatting
    {
        /// <summary>
        /// Formatting is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Emphasized text block.
        /// </summary>
        /// <remarks>
        /// This is a structural emphasis, as opposed to <see cref="Italics"/>.
        /// </remarks>
        Emphasis = 1,

        /// <summary>
        /// Strongly emphasized text block.
        /// </summary>
        /// <remarks>
        /// This is a structural emphasis, as opposed to <see cref="Bold"/>.
        /// </remarks>
        StrongEmphasis = 2,

        /// <summary>
        /// Italics.
        /// </summary>
        Italics = 3,

        /// <summary>
        /// Bold.
        /// </summary> 
        Bold = 4,

        /// <summary>
        /// Citation.
        /// </summary>
        Citation = 5,
        
        /// <summary>
        /// Deleted text.
        /// </summary>
        Deleted = 6,

        /// <summary>
        /// Inserted text.
        /// </summary>
        Inserted = 7,

        /// <summary>
        /// Superscript.
        /// </summary>
        Superscript = 8,

        /// <summary>
        /// Subscript.
        /// </summary>
        Subscript = 9,

        /// <summary>
        /// Code.
        /// </summary>
        Code = 10,

        /// <summary>
        /// Span.
        /// </summary>
        Span = 11
    }
}