using System;

namespace octalforty.Brushie.Text.Authoring
{
    /// <summary>
    /// Defines the type of elements that will be authored.
    /// </summary>
    [Flags()]
    public enum  AuthoringScope
    {
        /// <summary>
        /// Type is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Authors emphasis.
        /// </summary>
        Emphasis = 1,

        /// <summary>
        /// Authors strong emphasis.
        /// </summary>
        StrongEmphasis = 2,

        /// <summary>
        /// Authors italics.
        /// </summary>
        Italics = 4,

        /// <summary>
        /// Authors bold.
        /// </summary>
        Bold = 8,

        /// <summary>
        /// Authors spans.
        /// </summary>
        Span = 16,

        /// <summary>
        /// Authors deleted text.
        /// </summary>
        Deleted = 32,

        /// <summary>
        /// Authors inserted text.
        /// </summary>
        Inserted = 64,

        /// <summary>
        /// Authors subscript.
        /// </summary>
        Subscript = 128,

        /// <summary>
        /// Authors superscript.
        /// </summary>
        Superscript = 256,

        /// <summary>
        /// Authors headings.
        /// </summary>
        Heading = 512,

        /// <summary>
        /// Authors blockquotes.
        /// </summary>
        Blockquote = 1024,

        /// <summary>
        /// Authors footnotes.
        /// </summary>
        Footnote = 2048,

        /// <summary>
        /// Authors numeric lists.
        /// </summary>
        NumericList = 4096,

        /// <summary>
        /// Authors bulleted lists.
        /// </summary>
        BulletedList = 8192,

        /// <summary>
        /// Authors tables.
        /// </summary>
        Table = 16384,

        /// <summary>
        /// Authors links.
        /// </summary>
        Link = 32768,

        /// <summary>
        /// Authors images.
        /// </summary>
        Image = 65536,

        /// <summary>
        /// Authors acronyms.
        /// </summary>
        Acronym = 131072,

        /// <summary>
        /// Authors footnote references.
        /// </summary>
        FootnoteReference = 262144,

        /// <summary>
        /// Authors all elements.
        /// </summary>
        All = Acronym | Blockquote | Bold | BulletedList | Deleted | Emphasis |
            Footnote | FootnoteReference | Heading | Image | Inserted | Italics |
            Link | NumericList | Span | StrongEmphasis | Subscript | Superscript | Table
    }
}
