using System;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Defines attributes for a phrase Textile element.
    /// </summary>
    public class PhraseElementAttributes
    {
        #region Private Member Variables
        private String cssClass;
        private String id;
        private String style;
        private String language;
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
        /// Initializes a new instance of <see cref="PhraseElementAttributes"/> class with
        /// given values of attributes.
        /// </summary>
        /// <param name="cssClass">Element CSS class.</param>
        /// <param name="id">Element ID.</param>
        /// <param name="style">Inline element style.</param>
        /// <param name="language">Element language specification.</param>
        public PhraseElementAttributes(string cssClass, string id, string style, string language)
        {
            this.cssClass = cssClass;
            this.id = id;
            this.style = style;
            this.language = language;
        }
    }
}
