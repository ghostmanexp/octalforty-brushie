using System;
using System.Text.RegularExpressions;

namespace octalforty.Brushie.Text.Authoring.Textile.Internal
{
    /// <summary>
    /// Provides a base class for all elements extracted from Textile markup.
    /// </summary>
    /// <remarks>
    /// This class exposes <see cref="Expression"/> property, which is extracted from
    /// <c>Expression</c> regex group and provides access to the verbatim
    /// Textile markup as found in the source text.
    /// </remarks>
    internal abstract class Element
    {
        #region Private Member Variables
        private String expression;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a <see cref="String"/> which contains the whole Textile expression 
        /// of the element.
        /// </summary>
        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Element"/> class.
        /// </summary>
        protected Element()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Element"/> class extracting <see cref="Expression"/> value
        /// from the <paramref name="match"/>.
        /// </summary>
        /// <param name="match"></param>
        protected Element(Match match)
        {
            expression = match.Groups["Expression"].Value;
        }
    }
}
