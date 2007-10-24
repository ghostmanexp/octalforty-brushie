using System;

using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc.Conversion.Serialization;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc.Conversion.Serialization
{
    /// <summary>
    /// <see cref="DateTimeSerializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class DateTimeSerializerTestFixture : TypeSerializerTestFixture<DateTimeSerializer>
    {
        [Test()]
        public void Serialize()
        {
            Assert.AreEqual("<value><dateTime.iso8601>20070102T15:20:54</dateTime.iso8601></value>", 
                InternalSerialize(new DateTime(2007, 1, 2, 15, 20, 54)));
        }    
    }
}
