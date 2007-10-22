using System;
using System.Xml;

using octalforty.Brushie.Web.XmlRpc.Conversion;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Deserialization
{
    public class TypeDeserializerTestFixture<TTypeDeserializer>
        where TTypeDeserializer : class, ITypeDeserializer, new()
    {
        private static ITypeDeserializer CreateTypeDeserializer()
        {
            return new TTypeDeserializer();
        }

        protected static object InternalDeserialize(string serializedObject, Type type)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(serializedObject);

            return InternalDeserialize(xmlDocument, type);
        }

        protected static object InternalDeserialize(XmlNode xmlNode, Type type)
        {
            DeserializationContext deserializationContext = new DeserializationContext();
            return CreateTypeDeserializer().Deserialize(deserializationContext, 
                xmlNode, type);
        }
    }
}
