using System;
using System.Collections.Generic;
using System.Reflection;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Dispatches XML-RPC service requests.
    /// </summary>
    public class XmlRpcServiceDispatcher : IXmlRpcSerializerParameterTypesProvider
    {
        #region Private Member Variables
        private Type serviceType;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceDispatcher"/> class.
        /// </summary>
        /// <param name="serviceType"></param>
        public XmlRpcServiceDispatcher(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        /// <summary>
        /// Dispatches a request to the XML-RPC service.
        /// </summary>
        /// <param name="xmlRpcServiceContext"></param>
        public void Dispatch(IXmlRpcServiceContext xmlRpcServiceContext)
        {
            XmlRpcSerializer xmlRpcSerializer = new XmlRpcSerializer();
            XmlRpcRequest xmlRpcRequest = xmlRpcSerializer.DeserializeRequest(xmlRpcServiceContext.InputStream, this);

            object returnValue = GetXmlRpcServiceMethod(xmlRpcRequest.MethodName).Invoke(xmlRpcServiceContext.XmlRpcService,
                xmlRpcRequest.Parameters);
            XmlRpcResponse xmlRpcResponse = new XmlRpcResponse(returnValue);
            xmlRpcSerializer.SerializeResponse(xmlRpcResponse, xmlRpcServiceContext.OutputStream);
        }

        #region IXmlRpcSerializerParameterTypesProvider Members
        /// <summary>
        /// Returns types of parameters for an <see cref="XmlRpcRequest"/>.
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public Type[] GetRequestParameterTypes(string methodName)
        {
            //
            // First find the requested method
            MethodInfo xmlRpcServiceMethod = GetXmlRpcServiceMethod(methodName);
            if(xmlRpcServiceMethod == null)
                throw new NotImplementedException();

            //
            // Now collect its parameter types
            List<Type> parameterTypes = new List<Type>();
            foreach(ParameterInfo parameter in xmlRpcServiceMethod.GetParameters())
                parameterTypes.Add(parameter.ParameterType);

            return parameterTypes.ToArray();
        }
        #endregion

        private MethodInfo GetXmlRpcServiceMethod(string methodName)
        {
            MethodInfo[] methods = serviceType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            foreach(MethodInfo method in methods)
            {
                if(Attribute.IsDefined(method, typeof(XmlRpcServiceMethodAttribute), true))
                {
                    XmlRpcServiceMethodAttribute serviceMethodAttribute =
                        (XmlRpcServiceMethodAttribute)
                            Attribute.GetCustomAttribute(method, typeof(XmlRpcServiceMethodAttribute));
                    if(serviceMethodAttribute.Name == methodName)
                        return method;
                } // if
            } // foreach

            return null;
        }
    }
}
