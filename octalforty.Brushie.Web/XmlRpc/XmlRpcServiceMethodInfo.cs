using System;
using System.Collections.Generic;
using System.Reflection;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Describes XML-RPC Service Method.
    /// </summary>
    public sealed class XmlRpcServiceMethodInfo
    {
        #region Private Member Variables
        private string name;
        private string description;
        private Type[] parameterTypes;
        private MethodInfo methodInfo;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a string which contains the name of the XML-RPC Service Method.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets a string which contains the description of the XML-RPC Service Method.
        /// </summary>
        public string Description
        {
            get { return description; }
        }

        /// <summary>
        /// Gets an array of <see cref="Type"/> objects which describe types of method parameters.
        /// </summary>
        public Type[] ParameterTypes
        {
            get { return parameterTypes; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceMethodInfo"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="methodInfo"></param>
        /// <param name="parameterTypes"></param>
        internal XmlRpcServiceMethodInfo(string name, string description, MethodInfo methodInfo, Type[] parameterTypes)
        {
            this.name = name;
            this.description = description;
            this.parameterTypes = parameterTypes;
            this.methodInfo = methodInfo;
        }

        /// <summary>
        /// Invokes the current XML-RPC Service Method.
        /// </summary>
        /// <param name="xmlRpcService"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Invoke(IXmlRpcService xmlRpcService, object[] parameters)
        {
            return methodInfo.Invoke(xmlRpcService, parameters);
        }

        /// <summary>
        /// Creates a <see cref="XmlRpcServiceMethodInfo"/> object for the <paramref name="methodInfo"/>.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static XmlRpcServiceMethodInfo CreateXmlRpcServiceMethodInfo(MethodInfo methodInfo)
        {
            XmlRpcServiceMethodAttribute serviceMethodAttribute =
                (XmlRpcServiceMethodAttribute)Attribute.GetCustomAttribute(methodInfo, 
                typeof(XmlRpcServiceMethodAttribute));

            return new XmlRpcServiceMethodInfo(serviceMethodAttribute.Name, serviceMethodAttribute.Description,  
                methodInfo, GetParameterTypes(methodInfo));
        }

        /// <summary>
        /// Returns the name of an XML-RPC service method for <paramref name="methodInfo"/>.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static string GetXmlRpcServiceMethodName(MethodInfo methodInfo)
        {
            XmlRpcServiceMethodAttribute serviceMethodAttribute =
                (XmlRpcServiceMethodAttribute)Attribute.GetCustomAttribute(methodInfo,
                typeof(XmlRpcServiceMethodAttribute));

            return serviceMethodAttribute.Name;
        }

        private static Type[] GetParameterTypes(MethodInfo methodInfo)
        {
            List<Type> parameterTypes = new List<Type>();
            foreach(ParameterInfo parameter in methodInfo.GetParameters())
                parameterTypes.Add(parameter.ParameterType);

            return parameterTypes.ToArray();
        }

        public static bool IsXmlRpcServiceMethod(MethodInfo methodInfo)
        {
            return Attribute.IsDefined(methodInfo, typeof(XmlRpcServiceMethodAttribute));
        }
    }
}
