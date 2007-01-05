using System.Configuration;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Provides a base class for configuration elements with 
    /// <see cref="Type"/> property.
    /// </summary>
    public class ConfigurationElementWithType : ConfigurationElement
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets a string with an assembly-qualified name of the type.
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return this["type"] as string; }
            set { this["type"] = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationElementWithType"/> class.
        /// </summary>
        public ConfigurationElementWithType()
        {
        }
    }
}