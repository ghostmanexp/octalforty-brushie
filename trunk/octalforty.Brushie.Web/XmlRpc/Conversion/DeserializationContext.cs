using System;
using System.Collections.Generic;
using System.Xml;

using octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization;

namespace octalforty.Brushie.Web.XmlRpc.Conversion
{
    /// <summary>
    /// Deserialization context.
    /// </summary>
    public class DeserializationContext
    {
        #region Private Member Variables
        private IList<ITypeDeserializer> typeDeserializers = new List<ITypeDeserializer>();
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="DeserializationContext"/> class.
        /// </summary>
        public DeserializationContext()
        {
            typeDeserializers.Add(new Int32Deserializer());
            typeDeserializers.Add(new DoubleDeserializer());
            typeDeserializers.Add(new BooleanDeserializer());
            typeDeserializers.Add(new DateTimeDeserializer());
            typeDeserializers.Add(new StringDeserializer());
            typeDeserializers.Add(new XmlRpcStructureDeserializer());
        }

        /// <summary>
        /// Deserializes object from <paramref name="xmlNode"/>.
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual object Deserialize(XmlNode xmlNode, Type type)
        {
            ITypeDeserializer typeDeserializer = GetTypeDeserializer(xmlNode, type);
            return typeDeserializer.Deserialize(this, xmlNode, type);
        }

        /// <summary>
        /// Returns a <see cref="ITypeDeserializer"/> from <paramref name="xmlNode"/>.
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual ITypeDeserializer GetTypeDeserializer(XmlNode xmlNode, Type type)
        {
            foreach(ITypeDeserializer typeDeserializer in typeDeserializers)
                if(typeDeserializer.CanSerialize(xmlNode, type))
                    return typeDeserializer;

            return null;
        }
    }
}
