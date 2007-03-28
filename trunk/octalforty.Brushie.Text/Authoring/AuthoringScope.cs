using System;

namespace octalforty.Brushie.Text.Authoring
{
    /// <summary>
    /// Defines 
    /// </summary>
    [Flags()]
    public enum AuthoringScope
    {
        /// <summary>
        /// Authoring scope is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Author text formatting (emphasis, citation, subscript, superscript, etc.).
        /// </summary>
        TextFormatting = 1,

        /// <summary>
        /// Author headings.
        /// </summary>
        Headings = 2,

        /// <summary>
        /// Author blockquotes.
        /// </summary>
        Blockquotes = 4,

        /// <summary>
        /// Authors footnotes.
        /// </summary>
        Footnotes = 8,

        /// <summary>
        /// Authors lists.
        /// </summary>
        Lists = 16,

        /// <summary>
        /// Authors tables.
        /// </summary>
        Tables = 32,

        /// <summary>
        /// Authors links.
        /// </summary>
        Links = 64,

        /// <summary>
        /// Authors images.
        /// </summary>
        Images = 128,

        /// <summary>
        /// Author all elements.
        /// </summary>
        All = TextFormatting | Headings | Blockquotes | Footnotes | Lists | Tables | Links | Images
    }
}
