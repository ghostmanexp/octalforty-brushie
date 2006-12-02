using System;
using System.Threading;

namespace octalforty.Brushie.Instrumentation.Core.Messages
{
    /// <summary>
    /// Abstract base class for message classes.
    /// </summary>
    public abstract class MessageBase : IMessage
    {
        #region Private Member Variables
        private MessageSeverity severity;
        private string source;
        private DateTime time;
        private int threadID;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="MessageBase"/> class.
        /// </summary>
        protected MessageBase()
        {
            threadID = Thread.CurrentThread.ManagedThreadId;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MessageBase"/> class with given
        /// severity, source and time.
        /// </summary>
        /// <param name="severity">Message severity.</param>
        /// <param name="source">Message source.</param>
        /// <param name="time">Message time.</param>
        protected MessageBase(MessageSeverity severity, string source, DateTime time) :
            this()
        {
            this.severity = severity;
            this.source = source;
            this.time = time;
        }

        #region IMessage Members
        /// <summary>
        /// Gets message severity level.
        /// </summary>
        public virtual MessageSeverity Severity
        {
            get { return severity; }
        }

        /// <summary>
        /// Gets a string that identifies message source.
        /// </summary>
        public virtual string Source
        {
            get { return source; }
        }

        /// <summary>
        /// Gets a time of the message.
        /// </summary>
        public virtual DateTime Time
        {
            get { return time; }
        }

        /// <summary>
        /// Gets a value which uniquely identifies the thread.
        /// </summary>
        public virtual int ThreadID
        {
            get { return threadID; }
        }
        #endregion
    }
}