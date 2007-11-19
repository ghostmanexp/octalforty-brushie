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
        /// Creates a proxy for an XML-RPC service.
        /// </summary>
        /// <typeparam name="T">XML-RPC service interface.</typeparam>
        /// <returns></returns>
        public T CreateProxy<T>()
            where T : IXmlRpcService
        {
            //
            // First, create mixin
            XmlRpcServiceProxy mixin = new XmlRpcServiceProxy(typeof(T));

            //
            // Now create implementation
            GeneratorContext generatorContext = new GeneratorContext();
            generatorContext.AddMixinInstance(mixin);

            ProxyGenerator proxyGenerator = new ProxyGenerator();
            return (T)proxyGenerator.CreateCustomProxy(typeof(T), new XmlRpcServiceProxyInterceptor(),
                mixin, generatorContext);
        }

        /// <summary>
        /// Creates a proxy for an XML-RPC service at <paramref name="serviceEndpointUri"/>.
        /// </summary>
        /// <typeparam name="T">XML-RPC service interface.</typeparam>
        /// <param name="serviceEndpointUri"></param>
        /// <returns></returns>
        public T CreateProxy<T>(Uri serviceEndpointUri)
            where T : IXmlRpcService
        {
            T t = CreateProxy<T>();
            ((IXmlRpcServiceProxy)t).ServiceEndpointUri = serviceEndpointUri;

            return t;
        }
    }
}
