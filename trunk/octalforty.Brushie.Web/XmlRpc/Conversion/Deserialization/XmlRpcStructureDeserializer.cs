using System;
using System.Reflection;
using System.Xml;

using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// Deserializes classes and structures marked with <see cref="XmlRpcStructureAttribute"/>.
    /// </summary>
    public class XmlRpcStructureDeserializer : ITypeDeserializer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcStructureDeserializer"/>.
        /// </summary>
        public XmlRpcStructureDeserializer()
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
            return xmlNode.Name == "value" && xmlNode.FirstChild.Name == "struct" &&
                Attribute.IsDefined(type, typeof(XmlRpcStructureAttribute));
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
            //
            // Creating instance of type
            object value = Activator.CreateInstance(type);

            foreach(XmlNode memberNode in xmlNode.FirstChild.SelectNodes("./member"))
            {
                string memberName = memberNode.SelectSingleNode("./name").InnerText;
                
                PropertyInfo property = type.GetProperty(memberName, BindingFlags.Instance |
                    BindingFlags.Public);
                property.SetValue(value, 
                    deserializationContext.Deserialize(
                        memberNode.SelectSingleNode("./value"), property.PropertyType), null);
            } // foreach

            return value;
        }
        #endregion
    }
}
