namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Defines text formatting.
    /// </summary>
    public enum TextFormatting
    {
        /// <summary>
        /// Text formatting is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Strong structural emphasis.
        /// </summary>
        StrongEmphasis = 1,

        /// <summary>
        /// Bold text (design element, as opposed to <see cref="StrongEmphasis"/>).
        /// </summary>
        Bold = 2,

        /// <summary>
        /// Structural emphasis.
        /// </summary>
        Emphasis = 3,

        /// <summary>
        /// Italicized text (design element, as opposed to <see cref="Emphasis"/>).
        /// </summary>
        Italics = 4
    }
}
