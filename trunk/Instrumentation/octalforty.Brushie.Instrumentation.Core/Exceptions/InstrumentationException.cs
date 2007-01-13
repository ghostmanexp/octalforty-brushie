using System;
using System.Runtime.Serialization;

namespace octalforty.Brushie.Instrumentation.Core.Exceptions
{
    /// <summary>
    /// Base class for all exception classes within octalforty Brushie Instrumentation Framework.
    /// </summary>
    [Serializable()]
    public class InstrumentationException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InstrumentationException"/> class.
        /// </summary>
        public InstrumentationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InstrumentationException"/> class with
        /// a given message.
        /// </summary>
        /// <param name="message">Message.</param>
        public InstrumentationException(string message) :
            base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InstrumentationException"/> class with
        /// a given message and inner exception.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public InstrumentationException(string message, Exception innerException) : 
            base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InstrumentationException"/> class.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        public InstrumentationException(SerializationInfo serializationInfo, 
            StreamingContext streamingContext) : 
            base(serializationInfo, streamingContext)
        {
        }
    }
}
