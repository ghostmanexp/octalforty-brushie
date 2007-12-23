using Castle.DynamicProxy;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// An <see cref="IInterceptor"/> implementation, which is used to forward XML-RPC service
    /// method calls to a remote service endpoint.
    /// </summary>
    public class XmlRpcServiceProxyInterceptor : IInterceptor
    {
        #region Private Member Variables
        private XmlRpcSerializer xmlRpcSerializer = new XmlRpcSerializer();
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceProxyInterceptor"/> class.
        /// </summary>
        public XmlRpcServiceProxyInterceptor()
        {
        }

        #region IInterceptor Members
        public object Intercept(IInvocation invocation, params object[] args)
        {
            //
            // Only intercept calls to XML-RPC service methods
            if(XmlRpcServiceMethodInfo.IsXmlRpcServiceMethod(invocation.Method))
            {
                IXmlRpcServiceProxy xmlRpcServiceProxy = (IXmlRpcServiceProxy)invocation.InvocationTarget;
                IXmlRpcWebRequest xmlRpcWebRequest =
                    xmlRpcServiceProxy.WebRequestFactory.CreateRequest(xmlRpcServiceProxy.ServiceEndpointUri);

                XmlRpcServiceMethodInfo methodInfo =
                    XmlRpcServiceMethodInfo.CreateXmlRpcServiceMethodInfo(invocation.Method);

                xmlRpcSerializer.SerializeRequest(new XmlRpcRequest(methodInfo.Name, args), 
                    xmlRpcWebRequest.RequestStream);

                using(IXmlRpcWebResponse xmlRpcWebResponse = xmlRpcWebRequest.Invoke())
                {
                    XmlRpcResponse xmlRpcResponse =
                        xmlRpcSerializer.DeserializeResponse(xmlRpcWebResponse.ResponseStream, 
                        invocation.Method.ReturnType);

                    if(xmlRpcResponse is XmlRpcFaultResponse)
                        throw new XmlRpcInvocationException(((XmlRpcFaultResponse)xmlRpcResponse).Fault);

                    return ((XmlRpcSuccessResponse)xmlRpcResponse).ReturnValue;
                } // using
            } // if
            else
                return invocation.Proceed(args);
        }
        #endregion
    }
}
