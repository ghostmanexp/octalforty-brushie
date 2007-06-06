using System;

namespace octalforty.Brushie.Text.Authoring.Dom
{
    /// <summary>
    /// Defines a base class for all elements in DOM.
    /// </summary>
    public abstract class DomElement
    {
        #region Private Member Variables
        private DomElementCollection childElements = new DomElementCollection();
        private DomElement parent;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a collection of <see cref="DomElement"/> objects, which are the
        /// children of this <see cref="DomElement"/>.
        /// </summary>
        public virtual DomElementCollection ChildElements
        {
            get { return childElements; }
        }

        /// <summary>
        /// Gets a reference to the <see cref="DomElement"/> which is the parent of
        /// this <see cref="DomElement"/>.
        /// </summary>
        public virtual DomElement Parent
        {
            get { return parent; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="DomElement"/> class.
        /// </summary>
        protected DomElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DomElement"/> class with a
        /// reference to the parent element.
        /// </summary>
        /// <param name="parent">
        /// A reference to the <see cref="DomElement"/> which is the parent of this element.
        /// </param>
        protected DomElement(DomElement parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Appends <paramref name="domElement"/> to the end of the list of child elements of
        /// this <see cref="DomElement"/>.
        /// </summary>
        /// <param name="domElement">Child element to be added.</param>
        public virtual void AppendChild(DomElement domElement)
        {
            ChildElements.Add(domElement);
        }

        #region Overridables
        /// <summary>
        /// Accepts a <paramref name="domElementVisitor"/>.
        /// </summary>
        /// <param name="domElementVisitor">DOM element visitor.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="domElementVisitor"/> is a <c>null</c> reference.
        /// </exception>
        public abstract void Accept(IDomElementVisitor domElementVisitor);
        #endregion
    }
}