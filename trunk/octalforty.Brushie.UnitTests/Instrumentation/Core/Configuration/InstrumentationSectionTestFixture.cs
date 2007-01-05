using System;

using NUnit.Framework;

using octalforty.Brushie.Instrumentation.Core.Configuration;
using octalforty.Brushie.Instrumentation.Core.Formatters;
using octalforty.Brushie.Instrumentation.Core.Messages;
using octalforty.Brushie.Instrumentation.Core.Persisters;

namespace octalforty.Brushie.UnitTests.Instrumentation.Core.Configuration
{
    /// <summary>
    /// <see cref="InstrumentationSection"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class InstrumentationSectionTestFixture
    {
        [Test()]
        public void Persisters()
        {
            InstrumentationSection section = ConfigurationManager.ConfigurationSection;
            
            Assert.AreEqual(3, section.Persisters.Count);

            Assert.AreEqual("consolePersister", section.Persisters[0].Name);
            Assert.AreEqual(typeof(ConsolePersister), 
                Type.GetType(section.Persisters[0].Type));

            Assert.AreEqual("{Time:yyyy-MM-dd hh:mm:ss.fff} - {Severity:-20} - {Source:-30} - {Message}",
                section.Persisters[0].CustomProperties["formatString"].Value);
            
            Assert.AreEqual("tracePersister", section.Persisters[1].Name);
            Assert.AreEqual(typeof(TracePersister),
                Type.GetType(section.Persisters[1].Type));

            Assert.AreEqual("{Time:yyyy-MM-dd hh:mm:ss.fff} - {Severity} - {Source} - {Message}",
                section.Persisters[1].CustomProperties["formatString"].Value);
        }
        
        [Test()]
        public void Messages()
        {
            InstrumentationSection section = ConfigurationManager.ConfigurationSection;

            Assert.AreEqual("textMessage", section.Messages[0].Name);
            Assert.AreEqual(typeof(TextMessage),
                Type.GetType(section.Messages[0].Type));
        }
        
        [Test()]
        public void Formatters()
        {
            InstrumentationSection section = ConfigurationManager.ConfigurationSection;
            
            Assert.AreEqual(typeof(DateTimeFormatter), Type.GetType(section.Formatters[0].Type));
            Assert.AreEqual(typeof(GenericFormatter), Type.GetType(section.Formatters[1].Type));
        }
        
        [Test()]
        public void Bindings()
        {
            InstrumentationSection section = ConfigurationManager.ConfigurationSection;

            Assert.AreEqual("consolePersister", section.Bindings[0].PersisterName);
            Assert.AreEqual("*", section.Bindings[0].Severity);
            Assert.AreEqual("*", section.Bindings[0].Source);
            Assert.AreEqual("*", section.Bindings[0].Message);

            Assert.AreEqual("nullPersister", section.Bindings[1].PersisterName);
            Assert.AreEqual("Exception,Error", section.Bindings[1].Severity);
            Assert.AreEqual("*", section.Bindings[1].Source);
            Assert.AreEqual("textMesssage", section.Bindings[1].Message);
        }
    }
}
