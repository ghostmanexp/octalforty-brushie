using System;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// Deserializes <see cref="int"/> objects.
    /// </summary>
    public class Int32Deserializer : ITypeDeserializer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Int32Deserializer"/> class.
        /// </summary>
        public Int32Deserializer()
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
            return (xmlNode.Name == "i4" || xmlNode.Name == "int") &&
                type == typeof(int);
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
            return Convert.ToInt32(xmlNode.InnerText);
        }
        #endregion
    }
}
