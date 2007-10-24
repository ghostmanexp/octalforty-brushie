using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// <see cref="DoubleDeserializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class DoubleDeserializerTestFixture : TypeDeserializerTestFixture<DoubleDeserializer>
    {
        [Test()]
        public void Deserialize()
        {
            Assert.AreEqual(1.0, InternalDeserialize("<value><double>1.0</double></value>", typeof(double)));
            Assert.AreEqual(-541.4, InternalDeserialize("<value><double>-541.4</double></value>", typeof(double)));
        }

        [Test()]
        public void CanDeserialize()
        {
            Assert.IsTrue(InternalCanDeserialize("<value><double>1.0</double></value>", typeof(double)));

            Assert.IsFalse(InternalCanDeserialize("<value><d>1.0</d></value>", typeof(double)));
            Assert.IsFalse(InternalCanDeserialize("<double>1.0</double>", typeof(double)));
        }
    }
}
