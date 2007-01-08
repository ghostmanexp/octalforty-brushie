using System;

namespace octalforty.Brushie.Instrumentation.Core.Messages
{
    /// <summary>
    /// A simple message with just required fields.
    /// </summary>
    public class Message : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Message"/> class.
        /// </summary>
        public Message()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MessageBase"/> class with given
        /// severity and source.
        /// </summary>
        /// <param name="severity">Message severity.</param>
        /// <param name="source">Message source.</param>
        public Message(MessageSeverity severity, string source) : 
            base(severity, source, DateTime.Now)
        {
        }
    }
}
