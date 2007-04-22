using System;

namespace octalforty.Brushie.Text.Authoring.Textile.Dom
{
    /// <summary>
    /// Represents an acronym.
    /// </summary>
    public sealed class Acronym : InlineElement
    {
        #region Private Member Variables
        private String title;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a <see cref="String"/> which contains the title of the <see cref="Acronym"/>.
        /// </summary>
        public String Title
        {
            get { return title; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Acronym"/> class.
        /// </summary>
        public Acronym()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Acronym"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="innerText"></param>
        /// <param name="title"></param>
        public Acronym(DomElement parent, string innerText, string title) : 
            base(parent, innerText)
        {
            this.title = title;
        }

        #region DomElement Members
        /// <summary>
        /// Accepts a <paramref name="domElementVisitor"/>.
        /// </summary>
        /// <param name="domElementVisitor">DOM element visitor.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="domElementVisitor"/> is a <c>null</c> reference.
        /// </exception>
        public override void Accept(IDomElementVisitor domElementVisitor)
        {
            domElementVisitor.Visit(this);
        }
        #endregion
    }
}
