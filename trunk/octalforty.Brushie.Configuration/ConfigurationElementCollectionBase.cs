using System;
using System.Configuration;

namespace octalforty.Brushie.Configuration
{
    /// <summary>
    /// Provides a base class for collections of configuration elements.
    /// </summary>
    /// <typeparam name="TConfigurationElement">Type of the configuration element.</typeparam>
    public abstract class ConfigurationElementCollectionBase<TConfigurationElement> :
        ConfigurationElementCollection
        where TConfigurationElement : ConfigurationElement, new()
    {
        #region Public Properties
        /// <summary>
        /// Gets a reference to the <see cref="TConfigurationElement"/> with a given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TConfigurationElement this[int index]
        {
            get { return BaseGet(index) as TConfigurationElement; }
        }
        #endregion
        
        /// <summary>
        /// Initializes a new instance of 
        /// <see cref="ConfigurationElementCollectionBase{TConfigurationElement}"/> class.
        /// </summary>
        protected ConfigurationElementCollectionBase()
        {
        }
        
        /// <summary>
        /// Adds <paramref name="element"/> to the collection.
        /// </summary>
        /// <param name="element"></param>
        public void Add(TConfigurationElement element)
        {
            BaseAdd(element);
        }

        #region ConfigurationElementCollection Members
        /// <summary>
        /// Creates a new <see cref="System.Configuration.ConfigurationElement"></see>.
        /// </summary>
        /// <returns>
        /// A new <see cref="System.Configuration.ConfigurationElement"></see>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new TConfigurationElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Object"></see> that acts as the key 
        /// for the specified <see cref="System.Configuration.ConfigurationElement"></see>.
        /// </returns>
        /// <param name="element">
        /// The <see cref="System.Configuration.ConfigurationElement"></see> 
        /// to return the key for.
        /// </param>
        protected override object GetElementKey(ConfigurationElement element)
        {
            if(element is TConfigurationElement)
                return InternalGetElementKey(element as TConfigurationElement);

#warning Provide more specific error message
            throw new ArgumentException();
        }
        #endregion

        #region Overridables
        /// <summary>
        /// Internal method which is invoked from implementation of <see cref="GetElementKey"/>.<para />
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected abstract object InternalGetElementKey(TConfigurationElement element);
        #endregion
    }
}
