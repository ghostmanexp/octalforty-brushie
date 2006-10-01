using octalforty.Brushie.Instrumentation.Core.Internal;

namespace octalforty.Brushie.Instrumentation.Core
{
    /// <summary>
    /// Responsible for formatting objects.
    /// </summary>
    public sealed class FormattingManager
    {
        #region Private Member Variables
        private IFormatter rootFormatter = new NullFormatter();
        #endregion

        #region Private Static Member Variables
        private static FormattingManager instance = new FormattingManager();
        #endregion

        #region Public Static Properties
        /// <summary>
        /// Gets a reference to the single instance of <see cref="FormattingManager"/> class.
        /// </summary>
        public static FormattingManager Instance
        {
            get { return instance; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="FormattingManager"/> class.
        /// </summary>
        private FormattingManager()
        {
        }
        
        /// <summary>
        /// Adds <paramref name="formatter"/> to the formatting chain.
        /// </summary>
        /// <param name="formatter">Formatter.</param>
        public static void AddFormatter(IFormatter formatter)
        {
            Instance.InternalAddFormatter(formatter);
        }
        
        /// <summary>
        /// Formats <paramref name="value"/> and returns its string representation.
        /// </summary>
        /// <param name="value">Object to be formatted.</param>
        /// <returns></returns>
        public static string Format(object value)
        {
            return Instance.IternalFormat(value);
        }

        /// <summary>
        /// Adds <paramref name="formatter"/> to the formatting chain.
        /// </summary>
        /// <param name="formatter">Formatter.</param>
        private void InternalAddFormatter(IFormatter formatter)
        {
            InternalAddFormatterRecursive(rootFormatter, formatter);
        }

        /// <summary>
        /// Recursively adds <paramref name="formatter"/> to the formatting chain.
        /// </summary>
        /// <param name="formatter">Formatter.</param>
        private void InternalAddFormatterRecursive(IFormatter parentFormatter, IFormatter formatter)
        {
            if(parentFormatter.NextFormatter == null)
                parentFormatter.NextFormatter = formatter;
            else 
                InternalAddFormatterRecursive(parentFormatter.NextFormatter, formatter);
        }
        
        /// <summary>
        /// Formats <paramref name="value"/> and returns its string representation.
        /// </summary>
        /// <param name="value">Object to be formatted.</param>
        /// <returns></returns>
        private string IternalFormat(object value)
        {
            return rootFormatter.Format(value);
        }
    }
}
