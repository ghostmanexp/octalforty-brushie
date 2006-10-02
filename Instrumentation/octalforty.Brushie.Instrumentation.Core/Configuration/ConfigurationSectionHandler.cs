using System.Configuration;
using System.Xml;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Configuration section handler for "octalforty.brushie.instrumentation"
    /// section in application configuration file.
    /// </summary>
    public sealed class ConfigurationSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members
        /// <summary>
        /// Creates a configuration section handler.
        /// </summary>
        /// <returns>
        /// The created section handler object.
        /// </returns>
        /// <param name="parent">Parent object.</param>
        /// <param name="section">Section XML node.</param>
        /// <param name="configContext">Configuration context object.</param>
        public object Create(object parent, object configContext, XmlNode section)
        {
            return new ConfigurationSettings(section);
        }
        #endregion
    }
}