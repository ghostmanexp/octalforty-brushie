namespace octalforty.Brushie.Text.Authoring.Textile.Dom
{
    /// <summary>
    /// Represents a list.
    /// <seealso cref="ListItem"/>
    /// </summary>
    public abstract class List : BlockElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="List"/> class.
        /// </summary>
        protected List()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="List"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attributes"></param>
        protected List(DomElement parent, BlockElementAttributes attributes) : 
            base(parent, attributes)
        {
        }
    }
}
