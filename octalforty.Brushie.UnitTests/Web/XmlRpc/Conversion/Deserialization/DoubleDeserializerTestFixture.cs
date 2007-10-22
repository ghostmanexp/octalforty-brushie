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
            Assert.AreEqual(1.0, InternalDeserialize("<double>1.0</double>", typeof(double)));
            Assert.AreEqual(-541.4, InternalDeserialize("<double>-541.4</double>", typeof(double)));
        }
    }
}
