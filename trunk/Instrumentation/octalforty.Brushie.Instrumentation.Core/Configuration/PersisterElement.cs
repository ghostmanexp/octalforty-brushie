using System.Configuration;

using octalforty.Brushie.Configuration;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Represents a configuration element for a persister.
    /// </summary>
    public sealed class PersisterElement : ConfigurationElementWithNameAndType
    {
        #region Public Properties
        /// <summary>
        /// Gets the collection of custom properties for the persister.
        /// </summary>
        [ConfigurationProperty("properties", IsRequired = false)]
        [ConfigurationCollection(typeof(PropertyElementCollection))]
        public PropertyElementCollection CustomProperties
        {
            get { return this["properties"] as PropertyElementCollection; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="PersisterElement"/> class.
        /// </summary>
        public PersisterElement()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="PersisterElement"/> class with
        /// a given name and type.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public PersisterElement(string name, string type) :
            base(name, type)
        {
        }
    }
}
