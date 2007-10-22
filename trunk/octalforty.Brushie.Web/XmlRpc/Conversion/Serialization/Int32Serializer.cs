using System;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// Serializes <see cref="int"/> objects.
    /// </summary>
    public class Int32Serializer : TypeSerializerBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Int32Serializer"/> class.
        /// </summary>
        public Int32Serializer() : 
            base(typeof(int))
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
            xmlTextWriter.WriteElementString(String.Empty, "i4", String.Empty, value.ToString());
        }
        #endregion
    }
}
