namespace octalforty.Brushie.Text.Authoring.Textile.Dom
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
        void Visit(Document document);

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
        /// Visits the <paramref name="text"/> element.
        /// </summary>
        /// <param name="text"></param>
        void Visit(Text text);
    }
}
