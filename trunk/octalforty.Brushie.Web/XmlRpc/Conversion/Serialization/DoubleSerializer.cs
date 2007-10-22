using System;
using System.Globalization;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// Serializes <see cref="double"/> objects.
    /// </summary>
    public class DoubleSerializer : TypeSerializerBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DoubleSerializer"/> class.
        /// </summary>
        public DoubleSerializer() : 
            base(typeof(double))
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
            xmlTextWriter.WriteElementString(String.Empty, "double", String.Empty,
                ((double)value).ToString(CultureInfo.InvariantCulture));
        }
        #endregion
    }
}
