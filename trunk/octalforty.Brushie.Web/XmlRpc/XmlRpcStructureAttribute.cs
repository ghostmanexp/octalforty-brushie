using System;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Denotes an XML-RPC-compliant structure.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, 
        AllowMultiple = false, Inherited = true)]
    public sealed class XmlRpcStructureAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcStructureAttribute"/> class.
        /// </summary>
        public XmlRpcStructureAttribute()
        {
        }
    }
}
