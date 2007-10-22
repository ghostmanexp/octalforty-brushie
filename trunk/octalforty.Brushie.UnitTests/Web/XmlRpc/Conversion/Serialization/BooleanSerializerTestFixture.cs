using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Serialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// <see cref="BooleanSerializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class BooleanSerializerTestFixture : TypeSerializerTestFixture<BooleanSerializer>
    {
        [Test()]
        public void Serialize()
        {
            Assert.AreEqual("<boolean>true</boolean>", InternalSerialize(true));
            Assert.AreEqual("<boolean>false</boolean>", InternalSerialize(false));
        }
    }
}
