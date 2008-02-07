using System;

using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// Юнит-тесты для <see cref="ByteArrayDeserializer"/>.
    /// </summary>
    [TestFixture()]
    public class ByteArrayDeserializerTestFixture : TypeDeserializerTestFixture<ByteArrayDeserializer>
    {
        [Test()]
        public void Deserialize()
        {
            byte[] bytes = new byte[] { 1, 2, 3, 4 };

            Assert.AreEqual(bytes, 
                InternalDeserialize(string.Format("<value><base64>{0}</base64></value>", 
                Convert.ToBase64String(bytes)), typeof(byte[])));

            byte[] deserializedBytes = (byte[])InternalDeserialize("<value><base64></base64></value>", typeof(byte[]));
            
            Assert.IsNull(deserializedBytes);
        }
    }
}
