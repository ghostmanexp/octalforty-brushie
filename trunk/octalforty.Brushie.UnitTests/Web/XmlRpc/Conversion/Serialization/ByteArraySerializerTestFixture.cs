using System;

using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Serialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// Юнит-тесты для <see cref="ByteArraySerializer"/>.
    /// </summary>
    [TestFixture()]
    public class ByteArraySerializerTestFixture : TypeSerializerTestFixture<ByteArraySerializer>
    {
        [Test()]
        public void Serialize()
        {
            byte[] bytes = new byte[] { 1, 2, 3 ,4 };

            Assert.AreEqual(string.Format("<value><base64>{0}</base64></value>", Convert.ToBase64String(bytes)),
                InternalSerialize(bytes));
            Assert.AreEqual("<value><base64 /></value>",
                InternalSerialize(null));
        }
    }
}
