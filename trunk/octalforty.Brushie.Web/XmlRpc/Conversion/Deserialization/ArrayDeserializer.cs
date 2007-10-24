using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// Deserializes arrays.
    /// </summary>
    public class ArrayDeserializer : ITypeDeserializer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ArrayDeserializer"/> class.
        /// </summary>
        public ArrayDeserializer()
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
        public bool CanSerialize(XmlNode xmlNode, Type type)
        {
            return xmlNode.Name == "value" && xmlNode.FirstChild.Name == "array" && type.IsArray;
        }

        /// <summary>
        /// Serializes from <paramref name="xmlNode"/> using
        /// <paramref name="deserializationContext"/>.
        /// </summary>
        /// <param name="deserializationContext"></param>
        /// <param name="xmlNode"></param>
        /// <param name="type"></param>
        public object Deserialize(DeserializationContext deserializationContext, XmlNode xmlNode, Type type)
        {
            ArrayList array = new ArrayList();

            foreach(XmlNode valueNode in xmlNode.FirstChild.SelectNodes("./value"))
            {
                array.Add(deserializationContext.Deserialize(valueNode, type.GetElementType()));
            } // foreach

            return array.ToArray(type.GetElementType());
        }
        #endregion
    }
}
