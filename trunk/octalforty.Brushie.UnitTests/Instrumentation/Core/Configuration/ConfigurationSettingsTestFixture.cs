using System;
using System.IO;
using System.Xml;

using NUnit.Framework;

using octalforty.Brushie.Instrumentation.Core.Configuration;

namespace octalforty.Brushie.UnitTests.Instrumentation.Core.Configuration
{
    /// <summary>
    /// <see cref="ConfigurationSettings"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class ConfigurationSettingsTestFixture
    {
        [Test()]
        public void XmlNodeConstructor()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                "octalforty.Brushie.Instrumentation.xml"));
            
            ConfigurationSettings configurationSettings = 
                new ConfigurationSettings(xmlDocument.DocumentElement);
            
            Assert.AreEqual(1, configurationSettings.Persisters.GetLength(0));
            Assert.AreEqual(1, configurationSettings.Persisters[0].Properties.Count);
            
            Assert.AreEqual(1, configurationSettings.Messages.GetLength(0));
            
            Assert.AreEqual(2, configurationSettings.Formatters.GetLength(0));
            
            Assert.AreEqual(1, configurationSettings.Bindings.GetLength(0));
        }
    }
}