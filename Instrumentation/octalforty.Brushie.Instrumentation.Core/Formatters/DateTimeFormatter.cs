using System;

namespace octalforty.Brushie.Instrumentation.Core.Formatters
{
    /// <summary>
    /// Formats instances of <see cref="System.DateTime"/> structure.
    /// </summary>
    public class DateTimeFormatter : FormatterBase
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
            if(value is DateTime)
            {
                DateTime dateTime = (DateTime)value;
                return formatString == null || formatString == string.Empty ?
                    dateTime.ToString() : dateTime.ToString(formatString);
            } // if
            
            return null;
        }
        #endregion
    }
}