using System;

using NUnit.Framework;

using octalforty.Brushie.Instrumentation.Core;
using octalforty.Brushie.Instrumentation.Core.Binders;
using octalforty.Brushie.Instrumentation.Core.Messages;

namespace octalforty.Brushie.UnitTests.Instrumentation.Core.Binders
{
    /// <summary>
    /// <see cref="MessageTypeBinder"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class MessageTypeBinderTestFixture
    {
        [Test()]
        public void CanBindAllTypes()
        {
            IBinder binder = new MessageTypeBinder(new string[] { "*" } );
            
            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Debug, 
                string.Empty, DateTime.Now, string.Empty)));
        }
        
        [Test()]
        public void CanBindSpecificMessages()
        {
            IBinder binder = new MessageTypeBinder(new string[] { typeof(TextMessage).AssemblyQualifiedName });
            
            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Debug, 
                string.Empty, DateTime.Now, string.Empty)));
            Assert.IsFalse(binder.CanBind(new Message(MessageSeverity.CriticalError, string.Empty)));
        }
        
        [Test()]
        public void CanBind()
        {
            IBinder binder = new MessageTypeBinder(null);

            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Debug,
                string.Empty, DateTime.Now, string.Empty)));
            Assert.IsTrue(binder.CanBind(new Message()));

            binder = new MessageTypeBinder(new string[] {} );

            Assert.IsTrue(binder.CanBind(new TextMessage(MessageSeverity.Debug,
                string.Empty, DateTime.Now, string.Empty)));
            Assert.IsTrue(binder.CanBind(new Message()));
        }
    }
}
