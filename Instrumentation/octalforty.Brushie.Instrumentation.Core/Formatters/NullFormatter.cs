﻿using octalforty.Brushie.Instrumentation.Core.Formatters;

namespace octalforty.Brushie.Instrumentation.Core.Formatters
{
    /// <summary>
    /// Special case formatter.
    /// </summary>
    public class NullFormatter : FormatterBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NullFormatter"/> class.
        /// </summary>
        public NullFormatter()
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
            return null;
        }
        #endregion
    }
}