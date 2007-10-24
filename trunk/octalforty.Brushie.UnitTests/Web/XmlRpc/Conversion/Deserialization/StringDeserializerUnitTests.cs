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
            xmlDocument.LoadXml("<value><string>string value</string></value>");

            Assert.AreEqual("string value", InternalDeserialize(xmlDocument, typeof(string)));
            Assert.AreEqual("string value", 
                InternalDeserialize(xmlDocument.FirstChild.FirstChild, typeof(string)));
        }

        [Test()]
        public void CanDeserialize()
        {
            Assert.IsTrue(InternalCanDeserialize("<value>String</value>", typeof(string)));
            Assert.IsTrue(InternalCanDeserialize("<value><string>String</string></value>", typeof(string)));

            Assert.IsFalse(InternalCanDeserialize("<value><str>String</str></value>", typeof(string)));
            Assert.IsFalse(InternalCanDeserialize("<string>String</string>", typeof(string)));
        }
    }
}
