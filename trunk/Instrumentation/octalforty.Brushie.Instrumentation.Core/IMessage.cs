using System;

namespace octalforty.Brushie.Instrumentation.Core
{
    /// <summary>
    /// Interface, which must be implemented by all message classes, used
    /// in octalforty Brushie Instrumentation framework.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets message severity level.
        /// </summary>
        MessageSeverity Severity
        { get; }

        /// <summary>
        /// Gets a string that identifies message source.
        /// </summary>
        string Source
        { get; }

        /// <summary>
        /// Gets a time of the message.
        /// </summary>
        DateTime Time
        { get; }
    }
}