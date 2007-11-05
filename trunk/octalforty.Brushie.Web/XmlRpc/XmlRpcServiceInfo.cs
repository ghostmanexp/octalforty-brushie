using System;
using System.Collections.Generic;
using System.Reflection;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Describes XML-RPC Service.
    /// </summary>
    public sealed class XmlRpcServiceInfo
    {
        #region Private Member Variables
        private IDictionary<string, XmlRpcServiceMethodInfo> methods = 
            new Dictionary<string, XmlRpcServiceMethodInfo>();
        #endregion

        #region Public Properties
        /// <summary>
        /// Returns a read-only collection of <see cref="XmlRpcServiceMethodInfo"/> objects
        /// which describe methods of the XML-RPC Service.
        /// </summary>
        public IList<XmlRpcServiceMethodInfo> Methods
        {
            get { return new List<XmlRpcServiceMethodInfo>(methods.Values).AsReadOnly(); }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceInfo"/> class.
        /// </summary>
        /// <param name="methods"></param>
        internal XmlRpcServiceInfo(IDictionary<string, XmlRpcServiceMethodInfo> methods)
        {
            this.methods = methods;
        }

        /// <summary>
        /// Returns a <see cref="XmlRpcServiceMethodInfo"/> for method <paramref name="methodName"/>.
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public XmlRpcServiceMethodInfo GetMethod(string methodName)
        {
            return methods[methodName];
        }

        /// <summary>
        /// Creates <see cref="XmlRpcServiceInfo"/> for the <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static XmlRpcServiceInfo CreateXmlRpcServiceInfo(Type serviceType)
        {
            IDictionary<string, XmlRpcServiceMethodInfo> methods =
                new Dictionary<string, XmlRpcServiceMethodInfo>();

            //
            // Collect methods. Interfaces come first.
            Type[] interfaceTypes = serviceType.GetInterfaces();
            foreach(Type interfaceType in interfaceTypes)
            {
                InterfaceMapping interfaceMapping = serviceType.GetInterfaceMap(interfaceType);
                for(int i = 0; i < interfaceMapping.InterfaceMethods.GetLength(0); ++i)
                {
                    XmlRpcServiceMethodAttribute serviceMethodAttribute =
                        (XmlRpcServiceMethodAttribute)Attribute.GetCustomAttribute(
                            interfaceMapping.InterfaceMethods[i],
                            typeof(XmlRpcServiceMethodAttribute));

                    if(serviceMethodAttribute != null)
                    {
                        XmlRpcServiceMethodInfo xmlRpcServiceMethodInfo =
                            XmlRpcServiceMethodInfo.CreateXmlRpcServiceMethodInfo(
                            interfaceMapping.InterfaceMethods[i]);

                        methods.Add(xmlRpcServiceMethodInfo.Name, xmlRpcServiceMethodInfo);
                    } // if
                } // for
            } // foreach

            return new XmlRpcServiceInfo(methods);
        }
    }
}
