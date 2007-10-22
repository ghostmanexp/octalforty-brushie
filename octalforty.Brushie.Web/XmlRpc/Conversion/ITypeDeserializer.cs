using System;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion
{
    /// <summary>
    /// Defines a contract for a type deserializer.
    /// </summary>
    public interface ITypeDeserializer
    {
        /// <summary>
        /// Returns a value which indicates whether this <see cref="ITypeDeserializer"/>
        /// can deserialize objects from <paramref name="xmlNode"/> into <paramref name="type"/>.
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool CanSerialize(XmlNode xmlNode, Type type);

        /// <summary>
        /// Serializes from <paramref name="xmlNode"/> using
        /// <paramref name="deserializationContext"/>.
        /// </summary>
        /// <param name="deserializationContext"></param>
        /// <param name="xmlNode"></param>
        /// <param name="type"></param>
        object Deserialize(DeserializationContext deserializationContext,
            XmlNode xmlNode, Type type);
    }
}
