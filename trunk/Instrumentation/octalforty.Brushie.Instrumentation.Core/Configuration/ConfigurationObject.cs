using System.Xml;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Base class for configuration objects.
    /// </summary>
    public abstract class ConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationObject"/> class
        /// with a given XML node.
        /// </summary>
        /// <param name="configurationXmlNode"></param>
        protected ConfigurationObject(XmlNode configurationXmlNode)
        {
        }
    }
}
