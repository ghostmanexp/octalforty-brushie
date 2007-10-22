using System;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Denotes a member of an XML-RPC-compliant structure.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class XmlRpcMemberAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcMemberAttribute"/> class.
        /// </summary>
        public XmlRpcMemberAttribute()
        {
        }
    }
}
