using System.IO;

using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.Web.XmlRpc.Impl
{
    /// <summary>
    /// An implementation of <see cref="IXmlRpcServiceContext"/>.
    /// </summary>
    public class XmlRpcServiceContext : IXmlRpcServiceContext
    {
        #region Private Member Variables
        private IXmlRpcService xmlRpcService;
        private Stream inputStream;
        private Stream outputStream;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceContext"/>.
        /// </summary>
        /// <param name="xmlRpcService"></param>
        /// <param name="inputStream"></param>
        /// <param name="outputStream"></param>
        public XmlRpcServiceContext(IXmlRpcService xmlRpcService, Stream inputStream, Stream outputStream)
        {
            this.xmlRpcService = xmlRpcService;
            this.inputStream = inputStream;
            this.outputStream = outputStream;
        }

        #region IXmlRpcServiceContext Members
        // <summary>
        /// Gets the XML-RPC service.
        /// </summary>
        public IXmlRpcService XmlRpcService
        {
            get { return xmlRpcService; }
        }

        /// <summary>
        /// Gets a reference to the input stream.
        /// </summary>
        public Stream InputStream
        {
            get { return inputStream; }
        }

        /// <summary>
        /// Gets a reference to the output stream.
        /// </summary>
        public Stream OutputStream
        {
            get { return outputStream; }
        }
        #endregion
    }
}
