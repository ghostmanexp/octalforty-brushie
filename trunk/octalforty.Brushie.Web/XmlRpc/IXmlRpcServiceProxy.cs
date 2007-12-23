using System;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Defines a contract for the XML-RPC Service proxy.
    /// </summary>
    public interface IXmlRpcServiceProxy
    {
        /// <summary>
        /// Gets or sets an <see cref="Uri"/> which contains the service
        /// point for this proxy.
        /// </summary>
        Uri ServiceEndpointUri
        { get; set; }

        IXmlRpcWebRequestFactory WebRequestFactory
        { get; set; }
    }
}
