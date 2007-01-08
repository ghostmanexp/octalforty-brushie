using System.Configuration;

namespace octalforty.Brushie.Configuration
{
    /// <summary>
    /// Provides a base class for configuration sections.
    /// </summary>
    /// <remarks>
    /// This class provides <see cref="XmlNamespace"/> property, which is mapped to the standard
    /// <c>xmlns</c> attribute of the configuration section so that it fakes the
    /// concept of XML namespaces and somewhat fools .NET Framework since it does
    /// not support namespaces in configuration sections.
    /// </remarks>
    public abstract class ConfigurationSectionBase : ConfigurationSection
    {
        #region Public Properties
        /// <summary>
        /// Gets a string with the XML namespace of the configuration section.
        /// </summary>
        [ConfigurationProperty("xmlns", IsRequired = false)]
        public string XmlNamespace
        {
            get { return this["xmlns"] as string; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationSectionBase"/> class.
        /// </summary>
        protected ConfigurationSectionBase()
        {
        }
    }
}
