using System;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// Serializes <see cref="String"/> objects.
    /// </summary>
    public class StringSerializer : TypeSerializerBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="StringSerializer"/> class.
        /// </summary>
        public StringSerializer() :
            base(typeof(string))
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
            xmlTextWriter.WriteStartElement("string");

            xmlTextWriter.WriteString((string)value);

            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndElement();
        }
        #endregion
    }
}
