using System;
using System.Net;

using Castle.Core.Interceptor;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Provides a base class for creating client-side XML-RPC Service proxies.
    /// </summary>
    public class XmlRpcServiceProxy : IInterceptor
    {
        #region Private Member Variables
        private Uri serviceEndpointUri;
        private XmlRpcServiceInfo xmlRpcServiceInfo;
        private XmlRpcSerializer xmlRpcSerializer = new XmlRpcSerializer();
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceProxy"/> class.
        /// </summary>
        /// <param name="serviceEndpointUri"></param>
        /// <param name="xmlRpcServiceType"></param>
        internal XmlRpcServiceProxy(Uri serviceEndpointUri, Type xmlRpcServiceType)
        {
            this.serviceEndpointUri = serviceEndpointUri;

            xmlRpcServiceInfo =
                XmlRpcServiceInfo.CreateXmlRpcServiceInfo(xmlRpcServiceType);
        }

        #region IInterceptor Members
        public void Intercept(IInvocation invocation)
        {
            WebRequest webRequest = WebRequest.Create(serviceEndpointUri);
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml";

            XmlRpcServiceMethodInfo methodInfo =
                XmlRpcServiceMethodInfo.CreateXmlRpcServiceMethodInfo(invocation.Method);
            
            xmlRpcSerializer.SerializeRequest(new XmlRpcRequest(methodInfo.Name, 
                1, 2, 3), webRequest.GetRequestStream());

            WebResponse webResponse = webRequest.GetResponse();
        }
        #endregion
    }
}
