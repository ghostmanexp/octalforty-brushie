using System;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// A <see cref="IXmlRpcWebRequestFactory"/> which creates <see cref="HttpXmlRpcWebRequest"/> objects.
    /// </summary>
    public class HttpXmlRpcWebRequestFactory : IXmlRpcWebRequestFactory
    {
        /// <summary>
        /// Initializes a new instance of <see cref="HttpXmlRpcWebRequestFactory"/> class.
        /// </summary>
        public HttpXmlRpcWebRequestFactory()
        {
        }

        #region IXmlRpcWebRequestFactory Members
        /// <summary>
        /// Creates an <see cref="IXmlRpcWebRequest"/> for <paramref name="serviceEndpointUri"/>.
        /// </summary>
        /// <param name="serviceEndpointUri"></param>
        /// <returns></returns>
        public IXmlRpcWebRequest CreateRequest(Uri serviceEndpointUri)
        {
            return new HttpXmlRpcWebRequest(serviceEndpointUri);
        }
        #endregion
    }
}
