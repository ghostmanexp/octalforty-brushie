using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Serialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// <see cref="DoubleSerializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class DoubleSerializerTestFixture : TypeSerializerTestFixture<DoubleSerializer>
    {
        [Test()]
        public void Serialize()
        {
            Assert.AreEqual("<value><double>1</double></value>", InternalSerialize(1.0));
            Assert.AreEqual("<value><double>-153.43</double></value>", InternalSerialize(-153.43));
        }
    }
}
