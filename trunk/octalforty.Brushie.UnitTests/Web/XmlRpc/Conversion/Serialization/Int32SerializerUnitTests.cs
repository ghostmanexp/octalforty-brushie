using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Serialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// <see cref="Int32Serializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class Int32SerializerUnitTests : TypeSerializerTestFixture<Int32Serializer>
    {
        [Test()]
        public void Serialize()
        {
            Assert.AreEqual("<value><i4>123</i4></value>", InternalSerialize(123));
            Assert.AreEqual("<value><i4>-9123</i4></value>", InternalSerialize(-9123));
        }
    }
}
