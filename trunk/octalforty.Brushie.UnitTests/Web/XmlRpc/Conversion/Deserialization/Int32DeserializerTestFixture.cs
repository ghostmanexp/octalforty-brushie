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
            Assert.AreEqual(1234, InternalDeserialize("<value><i4>1234</i4></value>", typeof(int)));
            Assert.AreEqual(-21234, InternalDeserialize("<value><int>-21234</int></value>", typeof(int)));
        }

        [Test()]
        public void CanDeserialize()
        {
            Assert.IsTrue(InternalCanDeserialize("<value><i4>1234</i4></value>", typeof(int)));
            Assert.IsTrue(InternalCanDeserialize("<value><int>-21234</int></value>", typeof(int)));

            Assert.IsFalse(InternalCanDeserialize("<value><i>1234</i></value>", typeof(int)));
            Assert.IsFalse(InternalCanDeserialize("<int>-21234</int>", typeof(int)));
        }
    }
}
