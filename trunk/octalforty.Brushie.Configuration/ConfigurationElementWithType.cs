using System.Configuration;

namespace octalforty.Brushie.Configuration
{
    /// <summary>
    /// Provides a base class for configuration elements with 
    /// <see cref="Type"/> property.
    /// </summary>
    public class ConfigurationElementWithType : ConfigurationElement
    {
        #region Private Constants
        private const string TypePropertyName = "type";
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a string with an assembly-qualified name of the type.
        /// </summary>
        [ConfigurationProperty(TypePropertyName, IsRequired = true)]
        public string Type
        {
            get { return this[TypePropertyName] as string; }
            set { this[TypePropertyName] = value; }
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
