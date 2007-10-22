using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Serialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// <see cref="XmlRpcStructureSerializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class XmlRpcStructureSerializerTestFixture : 
        TypeSerializerTestFixture<XmlRpcStructureSerializer>
    {
        [Test()]
        public void Serialize()
        {
            Assert.AreEqual(
                "<struct>" +
                    "<member>" +
                        "<name>LowerBound</name><value><i4>4</i4></value>" +
                    "</member>" +
                    "<member>" +
                        "<name>UpperBound</name><value>from</value>" +
                    "</member>" +
                "</struct>", InternalSerialize(new Range(4, "from")));
        }
    }
}
