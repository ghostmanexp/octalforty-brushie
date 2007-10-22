using System;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion
{
    /// <summary>
    /// Provides a base class for type serializers.
    /// </summary>
    public abstract class TypeSerializerBase : ITypeSerializer
    {
        #region Private Member Variables
        private Type inputType;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="TypeSerializerBase"/> class.
        /// </summary>
        /// <param name="inputType"></param>
        protected TypeSerializerBase(Type inputType)
        {
            this.inputType = inputType;
        }

        #region ITypeSerializer Members
        /// <summary>
        /// Returns a value which indicates whether this <see cref="ITypeSerializer"/>
        /// can serialize objects of type <paramref name="type"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool CanSerialize(Type type)
        {
            return inputType.IsAssignableFrom(type);
        }

        /// <summary>
        /// Serializes <paramref name="value"/> into <paramref name="xmlTextWriter"/> using
        /// <paramref name="serializationContext"/>.
        /// </summary>
        /// <param name="serializationContext"></param>
        /// <param name="value"></param>
        /// <param name="xmlTextWriter"></param>
        public abstract void Serialize(SerializationContext serializationContext, 
            object value, XmlTextWriter xmlTextWriter);
        #endregion
    }
}
