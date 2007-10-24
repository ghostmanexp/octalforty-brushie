using System;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// Serializes <see cref="bool"/> objects.
    /// </summary>
    public class BooleanSerializer : TypeSerializerBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BooleanSerializer"/> class.
        /// </summary>
        public BooleanSerializer() :
            base(typeof(Boolean))
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
            xmlTextWriter.WriteElementString(String.Empty, "boolean", String.Empty,
                ((bool)value).ToString().ToLower());
            xmlTextWriter.WriteEndElement();
        }
        #endregion
    }
}
