using System;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Defines a contract for authoring engines.
    /// <seealso cref="IBlockElementParser"/>
    /// <seealso cref="IInlineElementParser"/>
    /// </summary>
    public interface IAuthoringEngine
    {
        /// <summary>
        /// Adds <paramref name="blockElementParser"/> to the end of the block
        /// elements parsing chain.
        /// </summary>
        /// <param name="blockElementParser">Block element parser to be added.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="blockElementParser"/> is a <c>null</c> reference.
        /// </exception>
        void AddBlockElementParser(IBlockElementParser blockElementParser);

        /// <summary>
        /// Adds <paramref name="inlineElementParser"/> to the end of the inline
        /// elements parsing chain.
        /// </summary>
        /// <param name="inlineElementParser">Inline element parser to be added.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="inlineElementParser"/> is a <c>null</c> reference.
        /// </exception>
        void AddInlineElementParser(IInlineElementParser inlineElementParser);

        /// <summary>
        /// Parses <paramref name="text"/>, extracts block elements and append them
        /// to <paramref name="parentElement"/>.
        /// <seealso cref="BlockElement"/>
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="text"></param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="parentElement"/> is a <c>null</c> reference.
        /// </exception>
        void ParseBlockElements(DomElement parentElement, String text);

        /// <summary>
        /// Parses <paramref name="text"/>, extracts inline elements and append them
        /// to <paramref name="parentElement"/>.
        /// <seealso cref="InlineElement"/>
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="text"></param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="parentElement"/> is a <c>null</c> reference.
        /// </exception>
        void ParseInlineElements(DomElement parentElement, String text);

        /// <summary>
        /// Parses <paramref name="text"/> and produces <see cref="DomDocument"/>.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        DomDocument Parse(String text);
    }
}
