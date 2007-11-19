using System.IO;
using System.Net;

using Castle.DynamicProxy;

namespace octalforty.Brushie.Web.XmlRpc
{
    public class XmlRpcServiceProxyInterceptor : IInterceptor
    {
        #region Private Member Variables
        private XmlRpcSerializer xmlRpcSerializer = new XmlRpcSerializer();
        #endregion

        #region IInterceptor Members
        public object Intercept(IInvocation invocation, params object[] args)
        {
            //
            // Only intercept calls to XML-RPC service methods
            if(XmlRpcServiceMethodInfo.IsXmlRpcServiceMethod(invocation.Method))
            {
                WebRequest webRequest =
                    WebRequest.Create(((IXmlRpcServiceProxy)invocation.InvocationTarget).ServiceEndpointUri);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml";

                XmlRpcServiceMethodInfo methodInfo =
                    XmlRpcServiceMethodInfo.CreateXmlRpcServiceMethodInfo(invocation.Method);

                Stream requestStream = webRequest.GetRequestStream();
                xmlRpcSerializer.SerializeRequest(new XmlRpcRequest(methodInfo.Name, args), 
                    requestStream);
                requestStream.Close();

                WebResponse webResponse = webRequest.GetResponse();

                XmlRpcResponse xmlRpcResponse =
                    xmlRpcSerializer.DeserializeResponse(webResponse.GetResponseStream(),
                    invocation.Method.ReturnType);

                if(xmlRpcResponse is XmlRpcFaultResponse)
                    throw new XmlRpcInvocationException(((XmlRpcFaultResponse)xmlRpcResponse).Fault);

                return ((XmlRpcSuccessResponse)xmlRpcResponse).ReturnValue;
            } // if
            else
                return invocation.Proceed(args);
        }
        #endregion
    }
}
