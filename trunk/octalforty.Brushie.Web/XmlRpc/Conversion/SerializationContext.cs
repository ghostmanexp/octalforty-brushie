using System;
using System.Collections.Generic;
using System.Xml;

using octalforty.Brushie.Web.XmlRpc.Conversion.Serialization;

namespace octalforty.Brushie.Web.XmlRpc.Conversion
{
    /// <summary>
    /// Serialization context.
    /// </summary>
    public class SerializationContext
    {
        #region Private Member Variables
        private IList<ITypeSerializer> typeSerializers = new List<ITypeSerializer>();
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="SerializationContext"/>.
        /// </summary>
        public SerializationContext()
        {
            typeSerializers.Add(new Int32Serializer());
            typeSerializers.Add(new DoubleSerializer());
            typeSerializers.Add(new StringSerializer());
            typeSerializers.Add(new BooleanSerializer());
            typeSerializers.Add(new DateTimeSerializer());
            typeSerializers.Add(new ByteArraySerializer());
            typeSerializers.Add(new ArraySerializer());
            typeSerializers.Add(new XmlRpcStructureSerializer());
        }

        /// <summary>
        /// Serializes <paramref name="value"/> into <paramref name="xmlTextWriter"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="xmlTextWriter"></param>
        /// <exception cref="TypeSerializationException">
        /// When no <see cref="ITypeSerializer"/> is registered for the 
        /// type of <paramref name="value"/>.
        /// </exception>
        public virtual void Serialize(object value, XmlTextWriter xmlTextWriter)
        {
            if(value != null)
            {
                ITypeSerializer typeSerializer = GetTypeSerializer(value.GetType());
                if(typeSerializer == null)
                    throw new TypeSerializationException(
                        string.Format("Could not find type serializer for {0}", value.GetType().FullName));

                typeSerializer.Serialize(this, value, xmlTextWriter);
            } // if
        }

        /// <summary>
        /// Returns a <see cref="ITypeSerializer"/> for <paramref name="exportedType"/>.
        /// </summary>
        /// <param name="exportedType"></param>
        /// <returns></returns>
        public virtual ITypeSerializer GetTypeSerializer(Type exportedType)
        {
            foreach(ITypeSerializer typeSerializer in typeSerializers)
                if(typeSerializer.CanSerialize(exportedType))
                    return typeSerializer;

            return null;
        }
    }
}
