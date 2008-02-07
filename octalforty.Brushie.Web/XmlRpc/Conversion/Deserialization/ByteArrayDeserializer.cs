using System;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// Deserializes byte arrays.
    /// </summary>
    public class ByteArrayDeserializer : ITypeDeserializer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ByteArrayDeserializer"/>.
        /// </summary>
        public ByteArrayDeserializer()
        {
        }

        #region ITypeDeserializer Members
        /// <summary>
        /// Returns a value which indicates whether this <see cref="ITypeDeserializer"/>
        /// can deserialize objects from <paramref name="xmlNode"/> into <paramref name="type"/>.
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool CanDeserialize(XmlNode xmlNode, Type type)
        {
            return xmlNode.Name == "value" && xmlNode.FirstChild.Name == "base64" &&
                (type == typeof(byte[]) || type == typeof(object));
        }

        /// <summary>
        /// Serializes from <paramref name="xmlNode"/> using
        /// <paramref name="deserializationContext"/>.
        /// </summary>
        /// <param name="deserializationContext"></param>
        /// <param name="xmlNode"></param>
        /// <param name="type"></param>
        public object Deserialize(DeserializationContext deserializationContext, XmlNode xmlNode, Type type)
        {
            return string.IsNullOrEmpty(xmlNode.FirstChild.InnerText) ? null : 
                Convert.FromBase64String(xmlNode.FirstChild.InnerText);
        }
        #endregion
    }
}
