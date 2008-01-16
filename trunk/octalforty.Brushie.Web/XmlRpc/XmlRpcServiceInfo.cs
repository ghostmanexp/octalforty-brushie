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
        private string name = String.Empty;
        private string description = String.Empty;
        private IDictionary<string, XmlRpcServiceMethodInfo> methods = 
            new Dictionary<string, XmlRpcServiceMethodInfo>();
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a string which contains the name of the XML-RPC service.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets a string which contains the description of the XML-RPC service.
        /// </summary>
        public string Description
        {
            get { return description; }
        }

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
        /// Initializes a new instance of <see cref="XmlRpcServiceInfo"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="methods"></param>
        internal XmlRpcServiceInfo(string name, string description, IDictionary<string, XmlRpcServiceMethodInfo> methods) :
            this(methods)
        {
            this.name = name;
            this.description = description;
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
            //
            // Service name and description
            string name = String.Empty;
            string description = String.Empty;

            if(Attribute.IsDefined(serviceType, typeof(XmlRpcServiceAttribute)))
            {
                XmlRpcServiceAttribute xmlRpcServiceAttribute =
                    (XmlRpcServiceAttribute)Attribute.GetCustomAttribute(serviceType, typeof(XmlRpcServiceAttribute));
                name = xmlRpcServiceAttribute.Name;
                description = xmlRpcServiceAttribute.Description;
            } // if

            IDictionary<string, XmlRpcServiceMethodInfo> serviceMethods  =
                new Dictionary<string, XmlRpcServiceMethodInfo>();

            //
            // Collect methods. Interfaces come first.
            Type[] interfaceTypes = serviceType.GetInterfaces();
            foreach(Type interfaceType in interfaceTypes)
            {
                if(!serviceType.IsInterface)
                    CreateInterfaceMethodsInfo(serviceType, interfaceType, serviceMethods);
                else 
                    CreateMethodsMethodsInfo(interfaceType.GetMethods(), 
                        serviceMethods);
            } // foreach

            //
            // Now the type itself
            CreateMethodsMethodsInfo(serviceType.GetMethods(), serviceMethods);

            return new XmlRpcServiceInfo(name, description, serviceMethods);
        }

        private static void CreateInterfaceMethodsInfo(Type serviceType, 
            Type interfaceType, IDictionary<string, XmlRpcServiceMethodInfo> serviceMethods)
        {
            InterfaceMapping interfaceMapping = serviceType.GetInterfaceMap(interfaceType);
            CreateMethodsMethodsInfo(interfaceMapping.InterfaceMethods, serviceMethods);
        }

        private static void CreateMethodsMethodsInfo(MethodInfo[] methods, 
            IDictionary<string, XmlRpcServiceMethodInfo> serviceMethods)
        {
            for(int i = 0; i < methods.GetLength(0); ++i)
            {
                XmlRpcServiceMethodAttribute serviceMethodAttribute =
                    (XmlRpcServiceMethodAttribute)Attribute.GetCustomAttribute(
                        methods[i],
                        typeof(XmlRpcServiceMethodAttribute));

                if(serviceMethodAttribute != null)
                {
                    XmlRpcServiceMethodInfo xmlRpcServiceMethodInfo =
                        XmlRpcServiceMethodInfo.CreateXmlRpcServiceMethodInfo(
                            methods[i]);

                    serviceMethods.Add(xmlRpcServiceMethodInfo.Name, xmlRpcServiceMethodInfo);
                } // if
            } // for
        }
    }
}
