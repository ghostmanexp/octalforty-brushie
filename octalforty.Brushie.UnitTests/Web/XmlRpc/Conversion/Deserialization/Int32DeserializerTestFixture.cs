using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// <see cref="Int32Deserializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class Int32DeserializerTestFixture : TypeDeserializerTestFixture<Int32Deserializer>
    {
        [Test()]
        public void Deserialize()
        {
            Assert.AreEqual(1234, InternalDeserialize("<i4>1234</i4>", typeof(int)));
            Assert.AreEqual(-21234, InternalDeserialize("<int>-21234</int>", typeof(int)));
        }
    }
}
