using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Serialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// <see cref="ArraySerializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class ArraySerializerTestFixture : TypeSerializerTestFixture<ArraySerializer>
    {
        [Test()]
        public void Serialize()
        {
            Assert.AreEqual("<value><array><data><value><i4>12</i4></value>" +
                "<value>Egypt</value></data></array></value>", 
                InternalSerialize(new object[] { 12, "Egypt" }));
        }
    }
}
