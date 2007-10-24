using System;
using System.IO;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Defines a contract for the XML-RPC service execution context.
    /// </summary>
    public interface IXmlRpcServiceContext
    {
        /// <summary>
        /// Gets the the XML-RPC service.
        /// </summary>
        IXmlRpcService XmlRpcService
        { get; }

        /// <summary>
        /// Gets a reference to the input stream.
        /// </summary>
        Stream InputStream
        { get; }

        /// <summary>
        /// Gets a reference to the output stream.
        /// </summary>
        Stream OutputStream
        { get; }
    }
}
