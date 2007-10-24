﻿using System;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Dispatches XML-RPC service requests.
    /// </summary>
    public class XmlRpcServiceDispatcher : IXmlRpcServiceDispatcher, IXmlRpcSerializerParameterTypesProvider
    {
        #region Private Member Variables
        private Type serviceType;
        private XmlRpcSerializer xmlRpcSerializer;
        private XmlRpcServiceInfo xmlRpcServiceInfo;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceDispatcher"/> class.
        /// </summary>
        /// <param name="serviceType"></param>
        public XmlRpcServiceDispatcher(Type serviceType)
        {
            this.serviceType = serviceType;
            
            xmlRpcSerializer = new XmlRpcSerializer();
            xmlRpcServiceInfo = XmlRpcServiceInfo.CreateXmlRpcServiceInfo(serviceType);
        }

        /// <summary>
        /// Dispatches a request to the XML-RPC service.
        /// </summary>
        /// <param name="xmlRpcServiceContext"></param>
        public void Dispatch(IXmlRpcServiceContext xmlRpcServiceContext)
        {
            XmlRpcRequest xmlRpcRequest = DeserializeRequest(xmlRpcServiceContext);
            XmlRpcServiceMethodInfo xmlRpcServiceMethodInfo = 
                xmlRpcServiceInfo.GetMethod(xmlRpcRequest.MethodName);

            object returnValue = null;

            try
            {
                returnValue = xmlRpcServiceMethodInfo.Invoke(xmlRpcServiceContext.XmlRpcService, 
                    xmlRpcRequest.Parameters);
            } // try

            catch(Exception e)
            {
                //returnValue = new XmlRpcF
            } // catch

            XmlRpcSuccessResponse xmlRpcResponse = new XmlRpcSuccessResponse(returnValue);
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
            return xmlRpcServiceInfo.GetMethod(methodName).ParameterTypes;
        }
        #endregion

        private XmlRpcRequest DeserializeRequest(IXmlRpcServiceContext xmlRpcServiceContext)
        {
            return xmlRpcSerializer.DeserializeRequest(xmlRpcServiceContext.InputStream, this);
        }
    }
}
