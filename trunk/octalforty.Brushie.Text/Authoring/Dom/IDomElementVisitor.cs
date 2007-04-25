using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Defines the contract for visitors for <see cref="DomElement"/>.
    /// </summary>
    public interface IDomElementVisitor
    {
        /// <summary>
        /// Visits the <paramref name="document"/> element.
        /// </summary>
        /// <param name="document"></param>
        void Visit(DomDocument document);

        /// <summary>
        /// Visits the <paramref name="heading"/> element.
        /// </summary>
        /// <param name="heading"></param>
        void Visit(Heading heading);

        /// <summary>
        /// Visits the <paramref name="blockquote"/> element.
        /// </summary>
        /// <param name="blockquote"></param>
        void Visit(Blockquote blockquote);

        /// <summary>
        /// Visits the <paramref name="paragraph"/> element.
        /// <seealso cref="Paragraph"/>
        /// </summary>
        /// <param name="paragraph"></param>
        void Visit(Paragraph paragraph);

        /// <summary>
        /// Visits the <paramref name="footnote"/> element.
        /// </summary>
        /// <param name="footnote"></param>
        void Visit(Footnote footnote);

        /// <summary>
        /// Visits the <paramref name="footnoteReference"/> element.
        /// </summary>
        /// <param name="footnoteReference"></param>
        void Visit(FootnoteReference footnoteReference);

        /// <summary>
        /// Visits the <paramref name="orderedList"/> element.
        /// </summary>
        /// <param name="orderedList"></param>
        void Visit(OrderedList orderedList);

        /// <summary>
        /// Visits the <paramref name="unorderedList"/> element.
        /// </summary>
        /// <param name="unorderedList"></param>
        void Visit(UnorderedList unorderedList);

        /// <summary>
        /// Visits the <paramref name="listItem"/> element.
        /// </summary>
        /// <param name="listItem"></param>
        void Visit(ListItem listItem);

        /// <summary>
        /// Visits the <paramref name="hyperlink"/> element.
        /// </summary>
        /// <param name="hyperlink"></param>
        void Visit(Hyperlink hyperlink);

        /// <summary>
        /// Visits the <paramref name="image"/> elements.
        /// </summary>
        /// <param name="image"></param>
        void Visit(Image image);

        /// <summary>
        /// Visits the <paramref name="textBlock"/> element.
        /// </summary>
        /// <param name="textBlock"></param>
        void Visit(TextBlock textBlock);

        /// <summary>
        /// Visits the <paramref name="acronym"/> element.
        /// </summary>
        /// <param name="acronym"></param>
        void Visit(Acronym acronym);
    }
}