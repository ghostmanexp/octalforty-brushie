using System;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Denotes an XML-RPC service method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class XmlRpcServiceMethodAttribute : Attribute
    {
        #region Private Member Variables
        private string name;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a string which contains the name of the method.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceMethodAttribute"/> class.
        /// </summary>
        /// <param name="name"></param>
        public XmlRpcServiceMethodAttribute(string name)
        {
            this.name = name;
        }
    }
}
