using System.Configuration;

using octalforty.Brushie.Configuration;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Represents a container for message configuration elements.
    /// </summary>
    public sealed class MessageElementCollection :
        ConfigurationElementCollectionBase<MessageElement>
    {
        #region Public Properties
        /// <summary>
        /// Gets a reference to the <see cref="MessageElement"/> with a given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public new MessageElement this[string name]
        {
            get { return BaseGet(name) as MessageElement; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="MessageElementCollection"/> class.
        /// </summary>
        public MessageElementCollection()
        {
        }

        #region ConfigurationElementCollectionBase<MessageElement> Members
        /// <summary>
        /// Internal method which is invoked from implementation of <see cref="ConfigurationElementCollectionBase{TConfigurationElement}.GetElementKey"/>.<para />
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object InternalGetElementKey(MessageElement element)
        {
            return element.Name;
        }

        /// <summary>
        /// Gets the name used to identify this collection of elements in the configuration 
        /// file when overridden in a derived class.
        /// </summary>
        /// <returns>
        /// The name of the collection; otherwise, an empty string. The default is an empty string.
        /// </returns>
        protected override string ElementName
        {
            get { return "message"; }
        }

        /// <summary>
        /// Gets the type of the <see cref="T:System.Configuration.ConfigurationElementCollection"></see>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Configuration.ConfigurationElementCollectionType"></see> 
        /// of this collection.
        /// </returns>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }
        #endregion
    }
}
