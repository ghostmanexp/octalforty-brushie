using System;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion
{
    /// <summary>
    /// Defines a contract for a type serializer.
    /// </summary>
    public interface ITypeSerializer
    {
        /// <summary>
        /// Returns a value which indicates whether this <see cref="ITypeSerializer"/>
        /// can serialize objects of type <paramref name="type"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool CanSerialize(Type type);

        /// <summary>
        /// Serializes <paramref name="value"/> into <paramref name="xmlTextWriter"/> using
        /// <paramref name="serializationContext"/>.
        /// </summary>
        /// <param name="serializationContext"></param>
        /// <param name="value"></param>
        /// <param name="xmlTextWriter"></param>
        void Serialize(SerializationContext serializationContext,
            object value, XmlTextWriter xmlTextWriter);
    }
}
