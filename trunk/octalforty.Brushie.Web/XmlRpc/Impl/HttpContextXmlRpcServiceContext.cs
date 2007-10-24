using System.IO;
using System.Web;

using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.Web.XmlRpc.Impl
{
    /// <summary>
    /// An implementation of <see cref="IXmlRpcServiceContext"/> which uses <see cref="HttpContext"/>.
    /// </summary>
    public class HttpContextXmlRpcServiceContext : IXmlRpcServiceContext
    {
        #region Private Member Variables
        private IXmlRpcService xmlRpcService;
        private HttpContext httpContext;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="HttpContextXmlRpcServiceContext"/>.
        /// </summary>
        /// <param name="xmlRpcService"></param>
        /// <param name="httpContext"></param>
        public HttpContextXmlRpcServiceContext(IXmlRpcService xmlRpcService, HttpContext httpContext)
        {
            this.xmlRpcService = xmlRpcService;
            this.httpContext = httpContext;
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
            get { return httpContext.Request.InputStream; }
        }

        /// <summary>
        /// Gets a reference to the output stream.
        /// </summary>
        public Stream OutputStream
        {
            get { return httpContext.Response.OutputStream; }
        }
        #endregion
    }
}
