using System;

using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Deserialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Deserialization
{
    /// <summary>
    /// <see cref="DateTimeDeserializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class DateTimeDeserializerTestFixture : TypeDeserializerTestFixture<DateTimeDeserializer>
    {
        [Test()]
        public void Deserialize()
        {
            Assert.AreEqual(new DateTime(2007, 1, 2, 15, 20, 54),
                InternalDeserialize("<dateTime.iso8601>20070102T15:20:54</dateTime.iso8601>", 
                typeof(DateTime)));
        }
    }
}
