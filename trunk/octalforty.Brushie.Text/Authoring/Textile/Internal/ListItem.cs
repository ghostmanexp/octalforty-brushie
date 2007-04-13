namespace octalforty.Brushie.Text.Authoring.Textile.Internal
{
    /// <summary>
    /// Represents a single item of the <see cref="List"/>.
    /// </summary>
    internal sealed class ListItem
    {
        #region Private Member Variables
        private string qualifier;
        private string title;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a <see cref="string"/> which contains list item qualifier.
        /// </summary>
        public string Qualifier
        {
            get { return qualifier; }
        }

        /// <summary>
        /// Gets a <see cref="string"/> which contains list item title.
        /// </summary>
        public string Title
        {
            get { return title; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ListItem"/> class.
        /// </summary>
        public ListItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ListItem"/> class.
        /// </summary>
        /// <param name="qualifier"></param>
        /// <param name="title"></param>
        public ListItem(string qualifier, string title)
        {
            this.qualifier = qualifier;
            this.title = title;
        }
    }
}
