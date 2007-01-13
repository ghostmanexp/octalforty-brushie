using octalforty.Brushie.Instrumentation.Core.Formatters;

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

        #region Private Properties
        /// <summary>
        /// Gets or sets a reference to the root <see cref="IFormatter"/>.
        /// </summary>
        private IFormatter RootFormatter
        {
            get { return rootFormatter; }
            set { rootFormatter = value; }
        }
        #endregion

        /// <summary>
        /// Explicit static constructor to tell C# compiler not to mark the type as 
        /// <c>beforefieldinit</c>.
        /// </summary>
        static FormattingManager()
        {
        }

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
        /// Removes all formatters from the formatting chain.
        /// </summary>
        public static void RemoveFormatters()
        {
            Instance.InternalRemoveFormatters();
        }

        /// <summary>
        /// Formats <paramref name="value"/> and returns its string representation according
        /// to <paramref name="formatString"/>.
        /// </summary>
        /// <param name="value">Object to be formatted.</param>
        /// <param name="formatString">Format string to be used.</param>
        /// <returns></returns>
        public static string Format(object value, string formatString)
        {
            return Instance.IternalFormat(value, formatString);
        }

        /// <summary>
        /// Adds <paramref name="formatter"/> to the formatting chain.
        /// </summary>
        /// <param name="formatter">Formatter.</param>
        private void InternalAddFormatter(IFormatter formatter)
        {
            InternalAddFormatterRecursive(RootFormatter, formatter);
        }

        /// <summary>
        /// Removes all formatters from the formatting chain.
        /// </summary>
        private void InternalRemoveFormatters()
        {
            RootFormatter = new NullFormatter();
        }

        /// <summary>
        /// Recursively adds <paramref name="formatter"/> to the formatting chain.
        /// </summary>
        /// <param name="parentFormatter">Parent formatter.</param>
        /// <param name="formatter">Formatter.</param>
        private void InternalAddFormatterRecursive(IFormatter parentFormatter, IFormatter formatter)
        {
            if(parentFormatter.NextFormatter == null)
                parentFormatter.NextFormatter = formatter;
            else 
                InternalAddFormatterRecursive(parentFormatter.NextFormatter, formatter);
        }

        /// <summary>
        /// Formats <paramref name="value"/> and returns its string representation according
        /// to <paramref name="formatString"/>.
        /// </summary>
        /// <param name="value">Object to be formatted.</param>
        /// <param name="formatString">Format string to be used.</param>
        /// <returns></returns>
        private string IternalFormat(object value, string formatString)
        {
            return RootFormatter.Format(value, formatString);
        }
    }
}