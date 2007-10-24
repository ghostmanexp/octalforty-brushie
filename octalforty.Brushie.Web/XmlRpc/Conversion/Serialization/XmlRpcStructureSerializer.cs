using System;
using System.Reflection;
using System.Xml;

using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// Serializes classes and structures marked with <see cref="XmlRpcStructureAttribute"/>.
    /// </summary>
    public class XmlRpcStructureSerializer : ITypeSerializer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcStructureSerializer"/> class.
        /// </summary>
        public XmlRpcStructureSerializer()
        {
        }

        #region ITypeSerializer Members
        /// <summary>
        /// Returns a value which indicates whether this <see cref="ITypeSerializer"/>
        /// can serialize objects of type <paramref name="type"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool CanSerialize(Type type)
        {
            return Attribute.IsDefined(type, typeof(XmlRpcStructureAttribute));
        }

        /// <summary>
        /// Serializes <paramref name="value"/> into <paramref name="xmlTextWriter"/> using
        /// <paramref name="serializationContext"/>.
        /// </summary>
        /// <param name="serializationContext"></param>
        /// <param name="value"></param>
        /// <param name="xmlTextWriter"></param>
        public void Serialize(SerializationContext serializationContext, 
            object value, XmlTextWriter xmlTextWriter)
        {
            xmlTextWriter.WriteStartElement("value");
            xmlTextWriter.WriteStartElement("struct");

            //
            // Serializing properties marked with XmlRpcMemberAttribute
            PropertyInfo[] properties = 
                value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(PropertyInfo property in properties)
            {
                if(Attribute.IsDefined(property, typeof(XmlRpcMemberAttribute)))
                {
                    //
                    // Find out member name
                    string memberName = property.Name;
                    XmlRpcMemberAttribute memberAttribute =
                        (XmlRpcMemberAttribute)Attribute.GetCustomAttribute(property, typeof(XmlRpcMemberAttribute));
                    if(!string.IsNullOrEmpty(memberAttribute.Name))
                        memberName = memberAttribute.Name;

                    xmlTextWriter.WriteStartElement("member");

                    xmlTextWriter.WriteElementString("name", String.Empty, memberName);

                    serializationContext.Serialize(property.GetValue(value, null), xmlTextWriter);

                    xmlTextWriter.WriteEndElement();
                } // if
            } // foreach

            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndElement();
        }
        #endregion
    }
}
