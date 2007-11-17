using System;

using Castle.DynamicProxy;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Provides means of creating proxies to XML-RPC Services.
    /// </summary>
    public class XmlRpcServiceProxyGenerator
    {
        /// <summary>
        /// Creates a proxy for an XML-RPC service at <paramref name="serviceEndpointUri"/>.
        /// </summary>
        /// <typeparam name="T">XML-RPC service interface.</typeparam>
        /// <param name="serviceEndpointUri"></param>
        /// <returns></returns>
        public T CreateProxy<T>(Uri serviceEndpointUri)
            where T : IXmlRpcService
        {
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            return proxyGenerator.CreateInterfaceProxyWithoutTarget<T>(
                new XmlRpcServiceProxy(serviceEndpointUri, typeof(T)));
        }
    }
}
