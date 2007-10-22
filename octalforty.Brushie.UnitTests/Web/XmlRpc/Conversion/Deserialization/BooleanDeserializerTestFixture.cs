using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// <see cref="BooleanDeserializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class BooleanDeserializerTestFixture : TypeDeserializerTestFixture<BooleanDeserializer>
    {
        [Test()]
        public void Deserialize()
        {
            Assert.AreEqual(true, InternalDeserialize("<boolean>true</boolean>", typeof(bool)));
            Assert.AreEqual(false, InternalDeserialize("<boolean>false</boolean>", typeof(bool)));
        }
    }
}
