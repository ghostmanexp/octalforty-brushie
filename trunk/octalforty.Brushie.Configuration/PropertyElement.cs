using System.Configuration;

namespace octalforty.Brushie.Configuration
{
    /// <summary>
    /// Represents a <c>property</c> configuration element.
    /// </summary>
    public sealed class PropertyElement : ConfigurationElement
    {
        #region Public Properties
        /// <summary>
        /// Gets a string with the name of the property.
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return this["name"] as string; }
            internal set { this["name"] = value; }
        }
        
        /// <summary>
        /// Gets a string with the value of the property.
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return this["value"] as string; }
            internal set { this["value"] = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="PropertyElement"/> class.
        /// </summary>
        public PropertyElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PropertyElement"/> class
        /// with a given name and value.
        /// </summary>
        public PropertyElement(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
