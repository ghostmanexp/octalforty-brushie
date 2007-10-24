using System;
using System.Globalization;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// Deserializes <see cref="bool"/> objects.
    /// </summary>
    public class DateTimeDeserializer : ITypeDeserializer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DateTimeDeserializer"/> class.
        /// </summary>
        public DateTimeDeserializer()
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
            return xmlNode.Name == "value" && xmlNode.FirstChild.Name == "dateTime.iso8601" && type == typeof(DateTime);
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
            return DateTime.ParseExact(xmlNode.InnerText, "yyyyMMddTHH:mm:ss", CultureInfo.InvariantCulture);
        }
        #endregion
    }
}
