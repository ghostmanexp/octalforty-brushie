using System;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// Deserializes <see cref="bool"/> objects.
    /// </summary>
    public class BooleanDeserializer : ITypeDeserializer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BooleanDeserializer"/> class.
        /// </summary>
        public BooleanDeserializer()
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
        public bool CanSerialize(XmlNode xmlNode, Type type)
        {
            return xmlNode.Name == "value" && xmlNode.FirstChild.Name == "boolean" && type == typeof(bool);
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
            return Convert.ToBoolean(xmlNode.FirstChild.InnerText);
        }
        #endregion
    }
}
