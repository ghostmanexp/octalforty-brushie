using System;

using NUnit.Framework;

using octalforty.Brushie.Instrumentation.Core;
using octalforty.Brushie.Instrumentation.Core.Binders;
using octalforty.Brushie.Instrumentation.Core.Messages;

namespace octalforty.Brushie.UnitTests.Instrumentation.Core.Binders
{
    /// <summary>
    /// <see cref="SourceBinder"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class SourceBinderTestFixture
    {
        [Test()]
        public void CanBindAllSources()
        {
            IBinder binder = new SourceBinder(new string[] { "*" } );
            
            Assert.IsTrue(binder.CanBind(new Message(MessageSeverity.CriticalError, string.Empty)));
            Assert.IsTrue(binder.CanBind(new Message(MessageSeverity.CriticalError, "Source 1")));
            Assert.IsTrue(binder.CanBind(new Message(MessageSeverity.CriticalError, null)));
            Assert.IsTrue(binder.CanBind(new Message(MessageSeverity.CriticalError, "Source 2")));
        }
        
        [Test()]
        public void CanBindSpecificSources()
        {
            IBinder binder = new SourceBinder(new string[] { "Source 1", "Source 2" } );

            Assert.IsFalse(binder.CanBind(new Message(MessageSeverity.CriticalError, string.Empty)));
            Assert.IsTrue(binder.CanBind(new Message(MessageSeverity.CriticalError, "Source 1")));
            Assert.IsFalse(binder.CanBind(new Message(MessageSeverity.CriticalError, null)));
            Assert.IsTrue(binder.CanBind(new Message(MessageSeverity.CriticalError, "Source 2")));
        }

        [Test()]
        public void CanBind()
        {
            IBinder binder = new SourceBinder(null);

            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Debug,
                string.Empty, DateTime.Now, string.Empty)));
            Assert.IsTrue(binder.CanBind(new Message()));

            binder = new SourceBinder(new string[] {} );

            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Debug,
                string.Empty, DateTime.Now, string.Empty)));
            Assert.IsTrue(binder.CanBind(new Message()));
        }
    }
}
