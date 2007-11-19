using System;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Occurs when there was an error while invoking server method.
    /// </summary>
    public class XmlRpcInvocationException : XmlRpcException
    {
        #region Private Member Variables
        private XmlRpcFault xmlRpcFault;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a <see cref="XmlRpc.XmlRpcFault"/> which describes the error.
        /// </summary>
        public XmlRpcFault XmlRpcFault
        {
            get { return xmlRpcFault; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcInvocationException"/> class.
        /// </summary>
        /// <param name="xmlRpcFault"></param>
        public XmlRpcInvocationException(XmlRpcFault xmlRpcFault)
        {
            this.xmlRpcFault = xmlRpcFault;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcInvocationException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="xmlRpcFault"></param>
        public XmlRpcInvocationException(string message, XmlRpcFault xmlRpcFault) : 
            base(message)
        {
            this.xmlRpcFault = xmlRpcFault;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcInvocationException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="xmlRpcFault"></param>
        /// <param name="innerException"></param>
        public XmlRpcInvocationException(string message, XmlRpcFault xmlRpcFault, Exception innerException) : 
            base(message, innerException)
        {
            this.xmlRpcFault = xmlRpcFault;
        }
    }
}
