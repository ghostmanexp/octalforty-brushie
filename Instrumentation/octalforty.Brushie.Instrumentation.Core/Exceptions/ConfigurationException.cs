using System;
using System.Runtime.Serialization;

namespace octalforty.Brushie.Instrumentation.Core.Exceptions
{
    /// <summary>
    /// octalforty Brushie Instrumentation Framework configuration exception.
    /// </summary>
    [Serializable()]
    public class ConfigurationException : InstrumentationException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationException"/> class.
        /// </summary>
        public ConfigurationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationException"/> class with
        /// a given message.
        /// </summary>
        /// <param name="message">Message.</param>
        public ConfigurationException(string message) :
            base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InstrumentationException"/> class with
        /// a given message and inner exception.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public ConfigurationException(string message, Exception innerException) :
            base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InstrumentationException"/> class.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        public ConfigurationException(SerializationInfo serializationInfo,
            StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}
