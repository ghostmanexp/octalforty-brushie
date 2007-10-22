using System.Xml;

using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// <see cref="StringDeserializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class StringDeserializerUnitTests : TypeDeserializerTestFixture<StringDeserializer>
    {
        [Test()]
        public void Deserialize()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<string>string value</string>");

            Assert.AreEqual("string value", InternalDeserialize(xmlDocument, typeof(string)));
            Assert.AreEqual("string value", 
                InternalDeserialize(xmlDocument.FirstChild.FirstChild, typeof(string)));
        }
    }
}
