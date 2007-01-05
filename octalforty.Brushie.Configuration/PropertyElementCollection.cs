using System.Collections.Specialized;
using System.Configuration;

namespace octalforty.Brushie.Configuration
{
    /// <summary>
    /// Represents a collection of <see cref="PropertyElement"/> objects.
    /// </summary>
    public class PropertyElementCollection : ConfigurationElementCollectionBase<PropertyElement>
    {
        #region Public Properties
        /// <summary>
        /// Gets a reference to the <see cref="PropertyElement"/> with a given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public new PropertyElement this[string name]
        {
            get { return BaseGet(name) as PropertyElement; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="PropertyElementCollection"/> class.
        /// </summary>
        public PropertyElementCollection()
        {
        }
        
        /// <summary>
        /// Converts the current instance of <see cref="PropertyElementCollection"/> to
        /// an instance of <see cref="StringDictionary"/>.
        /// </summary>
        /// <returns></returns>
        public StringDictionary ToStringDictionary()
        {
            StringDictionary properties = new StringDictionary();
            
            foreach(PropertyElement property in this)
                properties.Add(property.Name, property.Value);
            
            return properties;
        }

        #region ConfigurationElementCollectionBase<PropertyElement> Members
        /// <summary>
        /// Gets the type of the <see cref="System.Configuration.ConfigurationElementCollection"></see>.
        /// </summary>
        /// <returns>
        /// The <see cref="System.Configuration.ConfigurationElementCollectionType"></see> 
        /// of this collection.
        /// </returns>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        /// <summary>
        /// Gets the name used to identify this collection of elements in the 
        /// configuration file when overridden in a derived class.
        /// </summary>
        /// <returns>
        /// The name of the collection; otherwise, an empty string. 
        /// The default is an empty string.
        /// </returns>
        protected override string ElementName
        {
            get { return "property"; }
        }

        /// <summary>
        /// Internal method which is invoked from implementation of 
        /// <see cref="ConfigurationElementCollectionBase{TConfigurationElement}.GetElementKey"/>.<para />
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object InternalGetElementKey(PropertyElement element)
        {
            return element.Name;
        }
        #endregion
    }
}
