using System;
using System.Globalization;

namespace octalforty.Brushie.Instrumentation.Core.Formatters
{
    /// <summary>
    /// Formats <see cref="DateTime"/> instances according to ISO 8601.
    /// </summary>
    public class Iso8601DateTimeFormatter : FormatterBase
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
        protected override string InternalFormat(object value)
        {
            if(value is DateTime)
                return ((DateTime)value).ToString("s", DateTimeFormatInfo.InvariantInfo);
            
            return null;
        }
        #endregion
    }
}
