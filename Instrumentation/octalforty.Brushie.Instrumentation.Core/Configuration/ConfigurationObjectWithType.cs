using System.Xml;

using octalforty.Brushie.Instrumentation.Core.Exceptions;
using octalforty.Brushie.Instrumentation.Core.Resources;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Configuration object which requires <c>type</c> attribute.
    /// </summary>
    public abstract class ConfigurationObjectWithType : ConfigurationObject
    {
        #region Private Constants
        private readonly string TypeAttributeName = "type";
        #endregion

        #region Private Member Variables
        private string type;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a string which contains assembly-qualified name of the type.
        /// </summary>
        public string Type
        {
            get { return type; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationObjectWithType"/> class
        /// with a given XML node.
        /// </summary>
        /// <param name="configurationXmlNode"></param>
        protected ConfigurationObjectWithType(XmlNode configurationXmlNode) : 
            base(configurationXmlNode)
        {
            if(configurationXmlNode.Attributes[TypeAttributeName] == null)
                throw new ConfigurationException(string.Format(
                    Strings.ConfigurationObjectWithType_ConfigurationObjectWithType_RequiredAttributeMissing,
                    TypeAttributeName));

            type = configurationXmlNode.Attributes[TypeAttributeName].Value;
        }
    }
}
