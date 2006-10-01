using System;

namespace octalforty.Brushie.Instrumentation.Core.Messages
{
    /// <summary>
    /// Simple text message.
    /// </summary>
    public class TextMessage : MessageBase
    {
        #region Private Member Variables
        private string message;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the message string.
        /// </summary>
        public virtual string Message
        {
            get { return message; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="TextMessage"/> class with given
        /// severity, source, time and message string.
        /// </summary>
        /// <param name="severity">Message severity.</param>
        /// <param name="source">Message source.</param>
        /// <param name="time">Message time.</param>
        /// <param name="message">Message string.</param>
        public TextMessage(MessageSeverity severity, string source, 
            DateTime time, string message) : 
            base(severity, source, time)
        {
            this.message = message;
        }
    }
}
