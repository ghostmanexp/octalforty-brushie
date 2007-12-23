using System;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Defines a contract for factory which creates <see cref="IXmlRpcWebRequest"/> objects.
    /// </summary>
    public interface IXmlRpcWebRequestFactory
    {
        /// <summary>
        /// Creates an <see cref="IXmlRpcWebRequest"/> for <paramref name="serviceEndpointUri"/>.
        /// </summary>
        /// <param name="serviceEndpointUri"></param>
        /// <returns></returns>
        IXmlRpcWebRequest CreateRequest(Uri serviceEndpointUri);
    }
}
