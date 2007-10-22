using System.IO;
using System.Xml;

using octalforty.Brushie.Web.XmlRpc.Conversion;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Serialization
{
    public class TypeSerializerTestFixture<TTypeSerializer>
        where TTypeSerializer : class, ITypeSerializer, new()
    {
        private static ITypeSerializer CreateTypeSerializer()
        {
            return new TTypeSerializer();
        }

        protected static string InternalSerialize(object value)
        {
            SerializationContext serializationContext = new SerializationContext();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            CreateTypeSerializer().Serialize(serializationContext, value, xmlTextWriter);

            return stringWriter.ToString();
        }
    }
}
