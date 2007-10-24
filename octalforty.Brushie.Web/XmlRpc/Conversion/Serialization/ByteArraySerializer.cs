using System;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// Serializes <see cref="Byte"/> arrays.
    /// </summary>
    public class ByteArraySerializer : TypeSerializerBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ByteArraySerializer"/> class.
        /// </summary>
        public ByteArraySerializer() : 
            base(typeof(byte[]))
        {
        }

        #region TypeSerializerBase Members
        /// <summary>
        /// Serializes <paramref name="value"/> into <paramref name="xmlTextWriter"/> using
        /// <paramref name="serializationContext"/>.
        /// </summary>
        /// <param name="serializationContext"></param>
        /// <param name="value"></param>
        /// <param name="xmlTextWriter"></param>
        public override void Serialize(SerializationContext serializationContext, object value,
            XmlTextWriter xmlTextWriter)
        {
            xmlTextWriter.WriteStartElement("value");
            xmlTextWriter.WriteStartElement("base64");
            xmlTextWriter.WriteString(Convert.ToBase64String((byte[])value));
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndElement();
        }
        #endregion
    }
}
