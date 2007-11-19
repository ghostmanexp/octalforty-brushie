using System;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Defines a contract for the XML-RPC Service proxy.
    /// </summary>
    public interface IXmlRpcServiceProxy
    {
        Uri ServiceEndpointUri
        { get; set; }

        IXmlRpcWebRequestFactory WebRequestFactory
        { get; set; }
    }
}
