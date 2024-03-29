﻿using System;
using System.Collections;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// Serializes <see cref="Array"/> objects.
    /// </summary>
    public class ArraySerializer : TypeSerializerBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ArraySerializer"/> class.
        /// </summary>
        public ArraySerializer() : 
            base(typeof(Array))
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
            xmlTextWriter.WriteStartElement("array");
            xmlTextWriter.WriteStartElement("data");

            if(value != null && value is IEnumerable)
            {
                foreach(object _value in (IEnumerable) value)
                {
                    serializationContext.Serialize(_value, xmlTextWriter);
                } // foreach
            } // if

            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndElement();
        }
        #endregion
    }
}
