using System;

using octalforty.Brushie.Instrumentation.Core.Messages;

namespace octalforty.Brushie.Instrumentation.Core.Util
{
    /// <summary>
    /// Adapter used to simplyfy instrumentation of complex data structures.
    /// </summary>
    public sealed class TextMessageInstrumentationAdapter
    {
        #region Private Member Variables
        private MessageSeverity defaultSeverity;
        private string defaultSource;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets default message severity.
        /// </summary>
        public MessageSeverity DefaultSeverity
        {
            get { return defaultSeverity; }
        }

        /// <summary>
        /// Gets default source of the message.
        /// </summary>
        public string DefaultSource
        {
            get { return defaultSource; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="TextMessageInstrumentationAdapter"/> class
        /// with given severity and source.
        /// </summary>
        /// <param name="defaultSeverity">Default message severity.</param>
        /// <param name="defaultSource">Default message source.</param>
        public TextMessageInstrumentationAdapter(MessageSeverity defaultSeverity, string defaultSource)
        {
            this.defaultSeverity = defaultSeverity;
            this.defaultSource = defaultSource;
        }
        
        /// <summary>
        /// Instruments <paramref name="message"/>.
        /// </summary>
        /// <param name="message"></param>
        public void Instrument(string message)
        {
            Instrument(DefaultSeverity, message);
        }
        
        /// <summary>
        /// Instruments message <paramref name="messageFormat"/> formatted with <paramref name="args"/>.
        /// </summary>
        /// <param name="messageFormat"></param>
        /// <param name="args"></param>
        public void InstrumentFormat(string messageFormat, params object[] args)
        {
            if(InstrumentationManager.Instance.IsInstrumentationEnabled)
                Instrument(string.Format(messageFormat, args));
        }

        /// <summary>
        /// Instruments <paramref name="message"/>.
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="message"></param>
        public void Instrument(MessageSeverity severity, string message)
        {
            InstrumentationManager.Instrument(new TextMessage(severity, DefaultSource, 
                DateTime.Now, message));
        }

        /// <summary>
        /// Instruments message <paramref name="messageFormat"/> formatted with <paramref name="args"/>.
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="messageFormat"></param>
        /// <param name="args"></param>
        public void InstrumentFormat(MessageSeverity severity, string messageFormat, 
            params object[] args)
        {
            if(InstrumentationManager.Instance.IsInstrumentationEnabled)
                Instrument(severity, string.Format(messageFormat, args));
        }
    }
}