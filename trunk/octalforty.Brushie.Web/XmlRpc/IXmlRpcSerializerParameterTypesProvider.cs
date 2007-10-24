using System;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Defines a contract for the XML-RPC serializer parameter types provider.
    /// </summary>
    public interface IXmlRpcSerializerParameterTypesProvider
    {
        /// <summary>
        /// Returns types of parameters for an <see cref="XmlRpcRequest"/>.
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        Type[] GetRequestParameterTypes(string methodName);
    }
}
