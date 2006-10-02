using System.Xml;

using octalforty.Brushie.Instrumentation.Core.Exceptions;
using octalforty.Brushie.Instrumentation.Core.Resources;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Configuration object which requires <c>name</c> attribute.
    /// </summary>
    public abstract class ConfigurationObjectWithNameAndType : ConfigurationObjectWithType
    {
        #region Private Constants
        private readonly string NameAttributeName = "name";
        #endregion

        #region Private Member Variables
        private string name;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a string which contains the name.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationObjectWithNameAndType"/> class
        /// with a given XML node.
        /// </summary>
        /// <param name="configurationXmlNode"></param>
        protected ConfigurationObjectWithNameAndType(XmlNode configurationXmlNode) : 
            base(configurationXmlNode)
        {
            if(configurationXmlNode.Attributes[NameAttributeName] == null)
                throw new ConfigurationException(string.Format(
                    Strings.ConfigurationObjectWithNameAndType_ConfigurationObjectWithNameAndType_RequiredAttributeMissing,
                    NameAttributeName));
            
            name = configurationXmlNode.Attributes["name"].Value;
        }
    }
}