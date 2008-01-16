using System;

namespace octalforty.Brushie.Web.XmlRpc
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class XmlRpcServiceAttribute : Attribute
    {
        #region Private Member Variables
        private string name;
        private string description;
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
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceAttribute"/> class.
        /// </summary>
        /// <param name="name"></param>
        public XmlRpcServiceAttribute(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceAttribute"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public XmlRpcServiceAttribute(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
    }
}
