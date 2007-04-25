using System;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Defines attributes for an inline Textile element.
    /// <seealso cref="InlineElement"/>
    /// </summary>
    public class InlineElementAttributes
    {
        #region Private Member Variables
        private String cssClass;
        private String id;
        private String style;
        private String language;
        #endregion

        #region Public Static Constants
        /// <summary>
        /// An empty <see cref="InlineElementAttributes"/>.
        /// </summary>
        public static readonly InlineElementAttributes Empty =
            new InlineElementAttributes(String.Empty, string.Empty, String.Empty, String.Empty);
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a <see cref="String"/> which contains the CSS style of
        /// current element.
        /// </summary>
        public String CssClass
        {
            get { return cssClass; }
        }

        /// <summary>
        /// Gets a <see cref="String"/> which contains the ID of current element.
        /// </summary>
        public String ID
        {
            get { return id; }
        }

        /// <summary>
        /// Gets a <see cref="String"/> which contains inline style definition
        /// for the current element.
        /// </summary>
        public String Style
        {
            get { return style; }
        }

        /// <summary>
        /// Gets a <see cref="String"/> which contains the language of the
        /// current element.
        /// </summary>
        public String Language
        {
            get { return language; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="InlineElementAttributes"/> class with
        /// given values of attributes.
        /// </summary>
        /// <param name="cssClass">Element CSS class.</param>
        /// <param name="id">Element ID.</param>
        /// <param name="style">Inline element style.</param>
        /// <param name="language">Element language specification.</param>
        public InlineElementAttributes(String cssClass, String id, String style, String language)
        {
            this.cssClass = cssClass;
            this.id = id;
            this.style = style;
            this.language = language;
        }
    }
}