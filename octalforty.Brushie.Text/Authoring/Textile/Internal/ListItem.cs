using System.Text.RegularExpressions;

namespace octalforty.Brushie.Text.Authoring.Textile.Internal
{
    /// <summary>
    /// Represents a single item of the <see cref="List"/>.
    /// </summary>
    internal sealed class ListItem
    {
        #region Private Member Variables
        private string qualifiers;
        private string title;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a <see cref="string"/> which contains list item qualifiers.
        /// </summary>
        public string Qualifiers
        {
            get { return qualifiers; }
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
        /// <param name="qualifiers"></param>
        /// <param name="title"></param>
        public ListItem(string qualifiers, string title)
        {
            this.qualifiers = qualifiers;
            this.title = title;
        }
    }
}
