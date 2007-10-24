using System;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Denotes a member of an XML-RPC-compliant structure.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class XmlRpcMemberAttribute : Attribute
    {
        #region Private Member Variables
        private string name = string.Empty;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the name of this XML-RPC member.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcMemberAttribute"/> class.
        /// </summary>
        public XmlRpcMemberAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcMemberAttribute"/> class.
        /// </summary>
        /// <param name="name"></param>
        public XmlRpcMemberAttribute(string name)
        {
            this.name = name;
        }
    }
}
