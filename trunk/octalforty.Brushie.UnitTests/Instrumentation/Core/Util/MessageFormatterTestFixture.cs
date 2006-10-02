using System;

using NUnit.Framework;

using octalforty.Brushie.Instrumentation.Core;
using octalforty.Brushie.Instrumentation.Core.Formatters;
using octalforty.Brushie.Instrumentation.Core.Messages;
using octalforty.Brushie.Instrumentation.Core.Util;

namespace octalforty.Brushie.UnitTests.Instrumentation.Core.Util
{
    /// <summary>
    /// <see cref="MessageFormatter"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class MessageFormatterTestFixture
    {
        [TestFixtureSetUp()]
        public void TestFixtureSetUp()
        {
            FormattingManager.AddFormatter(new DateTimeFormatter());
            FormattingManager.AddFormatter(new GenericFormatter());
        }
        
        [Test()]
        public void FormatMessage()
        {
            IMessage message = new TextMessage(MessageSeverity.CriticalError, "Message Source",
                new DateTime(2006, 10, 12, 12, 34, 56, 78), "Message");
            Assert.AreEqual("2006-10-12 12:34:56.078 - (CriticalError) : Message Source - Message",
                MessageFormatter.FormatMessage(message,
                    "{Time:yyyy-MM-dd hh:mm:ss.fff} - ({Severity}) : {Source} - {Message}"));
        }
        
        [TestFixtureTearDown()]
        public void TestFixtureTearDown()
        {
            FormattingManager.RemoveFormatters();
        }
    }
}