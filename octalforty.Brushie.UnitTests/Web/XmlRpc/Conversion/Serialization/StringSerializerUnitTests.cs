using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Serialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// <see cref="StringSerializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class StringSerializerUnitTests : TypeSerializerTestFixture<StringSerializer>
    {
        [Test()]
        public void Serialize()
        {
            Assert.AreEqual("123", InternalSerialize("123"));
            Assert.AreEqual("123&lt;&gt;", InternalSerialize("123<>"));
        }
    }
}
