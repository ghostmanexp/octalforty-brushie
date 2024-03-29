﻿namespace octalforty.Brushie.Instrumentation.Core
{
    /// <summary>
    /// Formatters are used to format instances of arbitrary classes to their string
    /// representations.
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Gets or sets a reference to the next formatter in the chain.
        /// </summary>
        IFormatter NextFormatter
        { get; set; }
        
        /// <summary>
        /// Formats <paramref name="value"/> and returns its string representation according
        /// to <paramref name="formatString"/>.
        /// </summary>
        /// <param name="value">Object to be formatted.</param>
        /// <param name="formatString">Format string to be used.</param>
        /// <returns></returns>
        string Format(object value, string formatString);
    }
}