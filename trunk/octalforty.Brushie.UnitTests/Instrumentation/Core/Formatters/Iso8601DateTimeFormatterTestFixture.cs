using System;

using NUnit.Framework;

using octalforty.Brushie.Instrumentation.Core;
using octalforty.Brushie.Instrumentation.Core.Formatters;

namespace octalforty.Brushie.UnitTests.Instrumentation.Core.Formatters
{
    /// <summary>
    /// <see cref="Brushie.Instrumentation.Core.Formatters.Iso8601DateTimeFormatter"/>
    /// </summary>
    [TestFixture()]
    public class Iso8601DateTimeFormatterTestFixture
    {
        [Test()]
        public void Format()
        {
            IFormatter formatter = new Iso8601DateTimeFormatter();
            
            Assert.IsNull(formatter.Format("Formatter"));
            Assert.IsNull(formatter.Format(1));
            
            Assert.AreEqual("2006-01-02T12:34:56", formatter.Format(new DateTime(2006, 01, 02, 12, 34, 56)));
        }
    }
}
