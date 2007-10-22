using System;
using System.Globalization;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// Serializes <see cref="DateTime"/> objects.
    /// </summary>
    public class DateTimeSerializer : TypeSerializerBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DateTimeSerializer"/> class.
        /// </summary>
        public DateTimeSerializer() :
            base(typeof(DateTime))
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
            xmlTextWriter.WriteElementString(String.Empty, "dateTime.iso8601", String.Empty,
                ((DateTime)value).ToString("yyyyMMddTHH:mm:ss"));
        }
        #endregion
    }
}
