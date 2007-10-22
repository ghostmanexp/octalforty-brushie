using System.Xml;

using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// <see cref="XmlRpcStructureDeserializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class XmlRpcStructureDeserializerTestFixture : 
        TypeDeserializerTestFixture<XmlRpcStructureDeserializer>
    {
        [Test()]
        public void Deserialize()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(
                "<struct>" +
                    "<member>" +
                        "<name>LowerBound</name><value><i4>4</i4></value>" +
                    "</member>" +
                    "<member>" +
                        "<name>UpperBound</name><value>from</value>" +
                    "</member>" +
                "</struct>");

            Range range = (Range)InternalDeserialize(xmlDocument.FirstChild, typeof(Range));

            Assert.AreEqual(4, range.LowerBound);
            Assert.AreEqual("from", range.UpperBound);
        }
    }
}
