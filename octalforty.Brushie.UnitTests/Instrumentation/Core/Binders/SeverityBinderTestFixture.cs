using System;

using NUnit.Framework;

using octalforty.Brushie.Instrumentation.Core;
using octalforty.Brushie.Instrumentation.Core.Binders;
using octalforty.Brushie.Instrumentation.Core.Messages;

namespace octalforty.Brushie.UnitTests.Instrumentation.Core.Binders
{
    /// <summary>
    /// <see cref="SeverityBinder"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class SeverityBinderTestFixture
    {
        [Test()]
        public void CanBindAllSeverities()
        {
            IBinder binder = new SeverityBinder(new MessageSeverity[] { MessageSeverity.Sink } );

            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.CriticalError, 
                string.Empty, DateTime.Now, string.Empty)));
            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Debug,
                string.Empty, DateTime.Now, string.Empty)));
            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Error, 
                string.Empty, DateTime.Now, string.Empty)));
            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Exception, 
                string.Empty, DateTime.Now, string.Empty)));
            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Information, 
                string.Empty, DateTime.Now, string.Empty)));
            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Unknown,
                string.Empty, DateTime.Now, string.Empty)));
            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Warning,
                string.Empty, DateTime.Now, string.Empty)));
        }
        
        [Test()]
        public void CanBindSpecificSeverities()
        {
            IBinder binder = new SeverityBinder(new MessageSeverity[] 
                { MessageSeverity.CriticalError, MessageSeverity.Exception } );
            
            Assert.IsTrue(binder.CanBind(new Message(MessageSeverity.CriticalError, string.Empty)));
            Assert.IsTrue(binder.CanBind(new Message(MessageSeverity.Exception, string.Empty)));
            
            Assert.IsFalse(binder.CanBind(new Message(MessageSeverity.Error, string.Empty)));
        }

        [Test()]
        public void CanBind()
        {
            IBinder binder = new SeverityBinder(null);

            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Debug,
                string.Empty, DateTime.Now, string.Empty)));
            Assert.IsTrue(binder.CanBind(new Message()));

            binder = new SeverityBinder(new MessageSeverity[] {} );

            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Debug,
                string.Empty, DateTime.Now, string.Empty)));
            Assert.IsTrue(binder.CanBind(new Message()));
        }
    }
}
