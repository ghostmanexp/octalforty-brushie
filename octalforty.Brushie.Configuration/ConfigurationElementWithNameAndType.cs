using System.Configuration;

namespace octalforty.Brushie.Configuration
{
    /// <summary>
    /// Provides a base class for configuration elements with <see cref="Name"/> and
    /// <see cref="ConfigurationElementWithType.Type"/> properties.
    /// </summary>
    public class ConfigurationElementWithNameAndType : ConfigurationElementWithType
    {
        #region Private Constants
        private const string NamePropertyName = "name";
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a string with the name.
        /// </summary>
        [ConfigurationProperty(NamePropertyName, IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return this[NamePropertyName] as string; }
            set { this[NamePropertyName] = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationElementWithNameAndType"/> class.
        /// </summary>
        public ConfigurationElementWithNameAndType()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationElementWithNameAndType"/> class with
        /// a given name and type.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public ConfigurationElementWithNameAndType(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}
