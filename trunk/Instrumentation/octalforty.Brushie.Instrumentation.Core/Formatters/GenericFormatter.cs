namespace octalforty.Brushie.Instrumentation.Core.Formatters
{
    /// <summary>
    /// Generic formatter which simply invokes <see cref="object.ToString"/> on
    /// supplied instance.
    /// </summary>
    public class GenericFormatter : FormatterBase
    {
        #region FormatterBase Members
        /// <summary>
        /// Internal method, invoked from <see cref="FormatterBase.Format"/> implementation.<para />
        /// Formats <paramref name="value"/> and returns its string representation.<para />
        /// Should return <see langword="null" /> as an indication that this formatter cannot 
        /// format <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Object to be formatted.</param>
        /// <returns></returns>
        protected override string InternalFormat(object value, string formatString)
        {
            return value == null ? "(null)" : value.ToString();
        }
        #endregion
    }
}
