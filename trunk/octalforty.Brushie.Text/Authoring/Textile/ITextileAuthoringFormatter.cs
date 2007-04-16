using System;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Defines a contract for the formatter used by <see cref="TextileAuthoringEngine"/> to
    /// transform Textile markup.
    /// </summary>
    public interface ITextileAuthoringFormatter
    {
        /// <summary>
        /// Formats a heading of a given level with provided attributes.
        /// </summary>
        /// <param name="level">Heading level.</param>
        /// <param name="text">Heading text.</param>
        /// <param name="attributes">Attributes of the heading block element.</param>
        /// <returns></returns>
        String FormatHeading(Int32 level, String text, BlockElementAttributes attributes);

        /// <summary>
        /// Formats a blockquote with provided attribute.
        /// </summary>
        /// <param name="text">Blockquote text.</param>
        /// <param name="attributes">Attributes of the blockquote block element.</param>
        /// <returns></returns>
        String FormatBlockquote(String text, BlockElementAttributes attributes);

        /// <summary>
        /// Formats a hyperlink with given text, title, URL and attributes.
        /// </summary>
        /// <param name="text">Hyperlink text (the text that appears between <c>a</c> tags).</param>
        /// <param name="title">Hyperlink title (the text that appears in <c>title</c> attribute).</param>
        /// <param name="url">Hyperlink URL (the text that appears in <c>href</c> attribute).</param>
        /// <param name="attributes">Attributes of the hyperlink.</param>
        /// <returns></returns>
        String FormatHyperlink(String text, String title, String url, PhraseElementAttributes attributes);

        /// <summary>
        /// Formats a hyperlink with given text, title, URL and attributes.
        /// </summary>
        /// <param name="alternateText">Image alternate text.</param>
        /// <param name="url">Hyperlink URL (the text that appears in <c>href</c> attribute).</param>
        /// <param name="attributes">Attributes of the hyperlink.</param>
        /// <returns></returns>
        String FormatImage(String alternateText, String url, BlockElementAttributes attributes);

        /// <summary>
        /// Formats a text formatting block, as defined by <paramref name="formatting"/>.
        /// </summary>
        /// <param name="formatting">Text formatting.</param>
        /// <param name="text">The text.</param>
        /// <param name="attributes">Attributes of the phrase element.</param>
        /// <returns></returns>
        String FormatTextFormatting(TextFormatting formatting, String text, PhraseElementAttributes attributes);

        /// <summary>
        /// Formats a paragraph with provided attributes.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        String FormatParagraph(string text, BlockElementAttributes attributes);

        /// <summary>
        /// Formats a reference to a footnote with the given identifier.
        /// </summary>
        /// <param name="footnoteID"></param>
        /// <returns></returns>
        String FormatFootnoteReference(Int32 footnoteID);
        
        /// <summary>
        /// Formats a footnote with provided attribute.
        /// </summary>
        /// <param name="footnoteID">Footnote identifier.</param>
        /// <param name="text">Footnote text.</param>
        /// <param name="attributes">Attributes of the footnote block element.</param>
        /// <returns></returns>
        String FormatFootnote(Int32 footnoteID, String text, BlockElementAttributes attributes);
    }
}
