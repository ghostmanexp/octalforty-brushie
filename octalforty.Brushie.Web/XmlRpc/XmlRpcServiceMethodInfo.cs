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
        /// <param name="methodInfo"></param>
        /// <param name="parameterTypes"></param>
        internal XmlRpcServiceMethodInfo(string name, MethodInfo methodInfo, Type[] parameterTypes)
        {
            this.name = name;
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
                (XmlRpcServiceMethodAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(XmlRpcServiceMethodAttribute));
            return new XmlRpcServiceMethodInfo(serviceMethodAttribute.Name, methodInfo, GetParameterTypes(methodInfo));
        }

        private static Type[] GetParameterTypes(MethodInfo methodInfo)
        {
            List<Type> parameterTypes = new List<Type>();
            foreach(ParameterInfo parameter in methodInfo.GetParameters())
                parameterTypes.Add(parameter.ParameterType);

            return parameterTypes.ToArray();
        }
    }
}
