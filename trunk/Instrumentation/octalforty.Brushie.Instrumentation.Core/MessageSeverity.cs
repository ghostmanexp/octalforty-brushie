using System;

namespace octalforty.Brushie.Instrumentation.Core
{
    /// <summary>
    /// Defines message severity level.
    /// </summary>
    [Serializable()]
    public enum MessageSeverity
    {
        /// <summary>
        /// Message severity is unknown.
        /// </summary>
        Unknown,
        
        /// <summary>
        /// Informational message.
        /// </summary>
        Information,
        
        /// <summary>
        /// Debugging message.
        /// </summary>
        Debug,
        
        /// <summary>
        /// Warning message.
        /// </summary>
        Warning,
        
        /// <summary>
        /// Error message.
        /// </summary>
        Error,
        
        /// <summary>
        /// Critical error message.
        /// </summary>
        CriticalError,
        
        /// <summary>
        /// Exception message.
        /// </summary>
        Exception
    }
}
