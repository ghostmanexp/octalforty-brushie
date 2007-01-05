using System.Configuration;

using octalforty.Brushie.Configuration;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Represents a container for persister configuration elements.
    /// </summary>
    public sealed class PersisterElementCollection : 
        ConfigurationElementCollectionBase<PersisterElement>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PersisterElementCollection"/> class.
        /// </summary>
        public PersisterElementCollection()
        {
        }

        #region ConfigurationElementCollectionBase<PersisterElement> Members
        /// <summary>
        /// Internal method which is invoked from implementation of <see cref="ConfigurationElementCollectionBase{TConfigurationElement}.GetElementKey"/>.<para />
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object InternalGetElementKey(PersisterElement element)
        {
            return element.Name;
        }

        /// <summary>
        /// Gets the name used to identify this collection of elements in the configuration 
        /// file when overridden in a derived class.
        /// </summary>
        /// <returns>
        /// The name of the collection; otherwise, an empty string. The default is an 
        /// empty string.
        /// </returns>
        protected override string ElementName
        {
            get { return "persister"; }
        }

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
        #endregion
    }
}
