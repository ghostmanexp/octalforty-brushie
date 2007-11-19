using System;
using System.Net;

using Castle.Core.Interceptor;
using Castle.DynamicProxy;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Provides a base class for creating client-side XML-RPC Service proxies.
    /// </summary>
    public class XmlRpcServiceProxy : IXmlRpcServiceProxy
    {
        #region Private Member Variables
        private Uri serviceEndpointUri;
        private XmlRpcServiceInfo xmlRpcServiceInfo;
        private IXmlRpcWebRequestFactory webRequestFactory;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceProxy"/> class.
        /// </summary>
        /// <param name="serviceEndpointUri"></param>
        /// <param name="xmlRpcServiceType"></param>
        internal XmlRpcServiceProxy(Uri serviceEndpointUri, Type xmlRpcServiceType) :
            this(xmlRpcServiceType)
        {
            this.serviceEndpointUri = serviceEndpointUri;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceProxy"/> class.
        /// </summary>
        /// <param name="xmlRpcServiceType"></param>
        internal XmlRpcServiceProxy(Type xmlRpcServiceType)
        {
            xmlRpcServiceInfo =
                XmlRpcServiceInfo.CreateXmlRpcServiceInfo(xmlRpcServiceType);
        }

        #region IXmlRpcServiceProxy Members
        public Uri ServiceEndpointUri
        {
            get { return serviceEndpointUri; }
            set { serviceEndpointUri = value; }
        }

        public IXmlRpcWebRequestFactory WebRequestFactory
        {
            get { return webRequestFactory; }
            set { webRequestFactory = value; }
        }
        #endregion
    }
}
