namespace octalforty.Brushie.Instrumentation.Core.Formatters
{
    /// <summary>
    /// Abstract base class for formatters.
    /// </summary>
    public abstract class FormatterBase : IFormatter
    {
        #region Private Member Variables
        private IFormatter nextFormatter;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="FormatterBase"/> class.
        /// </summary>
        protected FormatterBase()
        {
        }

        #region IFormatter Members
        /// <summary>
        /// Gets or sets a reference to the next formatter in the chain.
        /// </summary>
        public virtual IFormatter NextFormatter
        {
            get { return nextFormatter; }
            set { nextFormatter = value; }
        }

        /// <summary>
        /// Formats <paramref name="value"/> and returns its string representation according
        /// to <paramref name="formatString"/>.
        /// </summary>
        /// <param name="value">Object to be formatted.</param>
        /// <returns></returns>
        public virtual string Format(object value, string formatString)
        {
            string formattedValue = InternalFormat(value, formatString);
            if(formattedValue == null && NextFormatter != null)
                return NextFormatter.Format(value, formatString);
            
            return formattedValue;
        }
        #endregion

        #region Overridables
        /// <summary>
        /// Internal method, invoked from <see cref="Format"/> implementation.<para />
        /// Formats <paramref name="value"/> and returns its string representation.<para />
        /// Should return <see langword="null" /> as an indication that this formatter cannot 
        /// format <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Object to be formatted.</param>
        /// <returns></returns>
        protected abstract string InternalFormat(object value, string formatString);
        #endregion
    }
}