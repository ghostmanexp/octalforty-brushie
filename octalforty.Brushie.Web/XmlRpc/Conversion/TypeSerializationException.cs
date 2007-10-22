using System;

namespace octalforty.Brushie.Web.XmlRpc.Conversion
{
    /// <summary>
    /// Thrown when type serialization error occurs.
    /// </summary>
    public class TypeSerializationException : XmlRpcException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TypeSerializationException"/> class.
        /// </summary>
        public TypeSerializationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TypeSerializationException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public TypeSerializationException(string message) : 
            base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TypeSerializationException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TypeSerializationException(string message, Exception innerException) : 
            base(message, innerException)
        {
        }
    }
}
