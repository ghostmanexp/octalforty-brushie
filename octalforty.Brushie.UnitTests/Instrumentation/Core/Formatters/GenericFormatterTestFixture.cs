using NUnit.Framework;

using octalforty.Brushie.Instrumentation.Core;
using octalforty.Brushie.Instrumentation.Core.Formatters;

namespace octalforty.Brushie.UnitTests.Instrumentation.Core.Formatters
{
    /// <summary>
    /// <see cref="GenericFormatter"/>
    /// </summary>
    [TestFixture()]
    public class GenericFormatterTestFixture
    {
        [Test()]
        public void Format()
        {
            IFormatter formatter = new GenericFormatter();
            
            Assert.AreEqual("1", formatter.Format(1, ""));
            Assert.AreEqual("(null)", formatter.Format(null, ""));
            Assert.AreEqual("Formatter", formatter.Format("Formatter", ""));
        }
    }
}
