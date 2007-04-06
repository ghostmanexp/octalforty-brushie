using System;
using System.Text.RegularExpressions;

namespace octalforty.Brushie.Text.Authoring.Textile.Internal
{
    /// <summary>
    /// Represents a single hyperlink.
    /// </summary>
    internal sealed class Hyperlink : Element
    {
        #region Private Member Variables
        private String text;
        private String title;
        private String url;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a <see cref="String"/> which contains the text of the hyperlink.
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// Gets or sets a <see cref="String"/> which contains the title of the hyperlink.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// Gets or sets a <see cref="String"/> which contains the URL of the hyperlink.
        /// </summary>
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Hyperlink"/> class.
        /// </summary>
        public Hyperlink()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Element"/> class extracting all required values
        /// from the <paramref name="match"/>.
        /// </summary>
        /// <param name="match"></param>
        /// <remarks>
        /// This constructor expects <c>Expression</c>, <c>Text</c>, <c>Title</c> and <c>Url</c> groups
        /// to be present in <paramref name="match"/>.
        /// </remarks>
        public Hyperlink(Match match) : 
            base(match)
        {
            text = match.Groups["Text"].Value;
            title = match.Groups["Title"].Value;
            url = match.Groups["Url"].Value;
        }
    }
}
