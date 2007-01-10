namespace octalforty.Brushie.Instrumentation.Core.Formatters
{
    /// <summary>
    /// Generic formatter which simply invokes <see cref="string.Format(string,object)"/> on
    /// supplied instance.
    /// </summary>
    public class GenericFormatter : FormatterBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GenericFormatter"/> class.
        /// </summary>
        public GenericFormatter()
        {
        }

        #region FormatterBase Members
        /// <summary>
        /// Internal method, invoked from <see cref="FormatterBase.Format"/> implementation.<para />
        /// Formats <paramref name="value"/> and returns its string representation.<para />
        /// Should return <see langword="null" /> as an indication that this formatter cannot 
        /// format <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Object to be formatted.</param>
        /// <param name="formatString">Format string to be used.</param>
        /// <returns></returns>
        protected override string InternalFormat(object value, string formatString)
        {
            if(formatString == null || formatString == string.Empty)
                return value == null ? "(null)" : value.ToString();

            string effectiveFormatString = string.Format("{{0,{0}}}", formatString);
            return string.Format(effectiveFormatString, value);
        }
        #endregion
    }
}