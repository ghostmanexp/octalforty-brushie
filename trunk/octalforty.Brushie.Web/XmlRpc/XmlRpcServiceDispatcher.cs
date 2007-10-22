using System;
using System.IO;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Dispatches XML-RPC service requests.
    /// </summary>
    public class XmlRpcServiceDispatcher
    {
        #region Private Member Variables
        private Type serviceType;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceDispatcher"/> class.
        /// </summary>
        /// <param name="serviceType"></param>
        public XmlRpcServiceDispatcher(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        public void Dispatch(Stream inputStream, Stream outputStream)
        {
            XmlRpcSerializer xmlRpcSerializer = new XmlRpcSerializer();
            /*XmlRpcRequest xmlRpcRequest = xmlRpcSerializer.DeserializeRequest(inputStream, 
                );*/
        }
    }
}
