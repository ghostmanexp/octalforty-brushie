using System;

using NUnit.Framework;

using octalforty.Brushie.Instrumentation.Core;
using octalforty.Brushie.Instrumentation.Core.Formatters;

namespace octalforty.Brushie.UnitTests.Instrumentation.Core.Formatters
{
    /// <summary>
    /// <see cref="DateTimeFormatter"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class DateTimeFormatterTestFixture
    {
        [Test()]
        public void Format()
        {
            IFormatter formatter = new DateTimeFormatter();
            DateTime dateTime = new DateTime(2006, 01, 02, 12, 34, 56, 78);
            
            Assert.IsNull(formatter.Format(1, string.Empty));
            Assert.AreEqual(dateTime.ToString(), formatter.Format(dateTime, null));
            Assert.AreEqual(dateTime.ToString(), formatter.Format(dateTime, string.Empty));
            
            Assert.AreEqual(dateTime.ToString("s"), formatter.Format(dateTime, "s"));
        }
    }
}
