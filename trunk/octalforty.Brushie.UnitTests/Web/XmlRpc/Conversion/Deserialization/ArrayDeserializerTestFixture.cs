using System;

using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// <see cref="ArrayDeserializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class ArrayDeserializerTestFixture : TypeDeserializerTestFixture<ArrayDeserializer>
    {
        [Test()]
        public void Deserialize()
        {
            object[] array = 
                (object[])InternalDeserialize("<array><data><value><i4>12</i4></value>" +
                "<value><string>Egypt</string></value><value><boolean>0</boolean></value>" +
                "<value><i4>-31</i4></value></data></array>", typeof(Array));

            Assert.AreEqual(4, array.GetLength(0));

            Assert.AreEqual(12, array[0]);
            Assert.AreEqual("Egypt", array[1]);
            Assert.AreEqual(false, array[2]);
            Assert.AreEqual(-31, array[3]);
        }
    }
}
