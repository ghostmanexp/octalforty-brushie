using System;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlRpcException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcException"/> class.
        /// </summary>
        public XmlRpcException()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public XmlRpcException(string message) : 
            base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public XmlRpcException(string message, Exception innerException) : 
            base(message, innerException)
        {
        }
    }
}
