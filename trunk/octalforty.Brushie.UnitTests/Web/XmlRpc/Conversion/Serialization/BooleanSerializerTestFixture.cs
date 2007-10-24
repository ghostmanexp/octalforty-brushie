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
            Assert.AreEqual("<value><boolean>true</boolean></value>", InternalSerialize(true));
            Assert.AreEqual("<value><boolean>false</boolean></value>", InternalSerialize(false));
        }
    }
}
