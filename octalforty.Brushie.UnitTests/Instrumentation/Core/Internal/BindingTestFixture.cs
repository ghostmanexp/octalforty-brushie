using NUnit.Framework;

using octalforty.Brushie.Instrumentation.Core.Configuration;
using octalforty.Brushie.Instrumentation.Core.Internal;

namespace octalforty.Brushie.UnitTests.Instrumentation.Core.Internal
{
    /// <summary>
    /// <see cref="Binding"/> unit tests.
    /// </summary>
    //[TestFixture()]
    public class BindingTestFixture
    {
        [Test()]
        public void BindingElementConstructor()
        {
            Binding binding = new Binding(new BindingElement("consolePersister", "", "*", "*"));
            
            Assert.AreEqual("consolePersister", binding.PersisterName);
            Assert.IsEmpty(binding.Messages);
            Assert.AreEqual("*", binding.Severities);
            Assert.AreEqual("*", binding.Sources);
        }
    }
}
