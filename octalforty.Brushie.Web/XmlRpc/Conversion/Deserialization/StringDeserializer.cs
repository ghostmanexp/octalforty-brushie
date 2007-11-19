using System;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// Initializes a new instance of <see cref="StringDeserializer"/> class.
    /// </summary>
    public class StringDeserializer : ITypeDeserializer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="StringDeserializer"/> class.
        /// </summary>
        public StringDeserializer()
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
            return xmlNode.Name == "value" && 
                ((xmlNode.FirstChild.NodeType == XmlNodeType.Element && xmlNode.FirstChild.Name == "string") || 
                (xmlNode.FirstChild.NodeType == XmlNodeType.Text)) &&
                (type == typeof(string) || type == typeof(object));
        }

        /// <summary>
        /// Serializes from <paramref name="xmlNode"/> using
        /// <paramref name="deserializationContext"/>.
        /// </summary>
        /// <param name="deserializationContext"></param>
        /// <param name="xmlNode"></param>
        /// <param name="type"></param>
        public object Deserialize(DeserializationContext deserializationContext,
            XmlNode xmlNode, Type type)
        {
            return xmlNode.FirstChild.InnerText;
        }
        #endregion
    }
}
