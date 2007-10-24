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
            Assert.AreEqual(true, InternalDeserialize("<value><boolean>true</boolean></value>", typeof(bool)));
            Assert.AreEqual(false, InternalDeserialize("<value><boolean>false</boolean></value>", typeof(bool)));

            Assert.AreEqual(true, InternalDeserialize("<value><boolean>1</boolean></value>", typeof(bool)));
            Assert.AreEqual(false, InternalDeserialize("<value><boolean>0</boolean></value>", typeof(bool)));
        }

        [Test()]
        public void CanDeserialize()
        {
            Assert.IsTrue(InternalCanDeserialize("<value><boolean>true</boolean></value>", typeof(bool)));
            Assert.IsTrue(InternalCanDeserialize("<value><boolean>false</boolean></value>", typeof(bool)));
            
            Assert.IsFalse(InternalCanDeserialize("<value><bool>false</bool></value>", typeof(bool)));
            Assert.IsFalse(InternalCanDeserialize("<boolean>false</boolean>", typeof(bool)));
        }
    }
}
