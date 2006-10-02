using System.Collections.Generic;
using System.Xml;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Contains configuration settings for <c>persister</c> element.
    /// </summary>
    public class Persister : ConfigurationObjectWithNameAndType
    {
        #region Private Member Variables
        private Dictionary<string, string> properties = new Dictionary<string, string>();
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a reference to the dictionary of properties of the persister.
        /// </summary>
        public Dictionary<string, string> Properties
        {
            get { return properties; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Persister"/> class with a 
        /// given XML node.
        /// </summary>
        /// <param name="configurationXmlNode"></param>
        public Persister(XmlNode configurationXmlNode) : 
            base(configurationXmlNode)
        {
            XmlNodeList propertiesNodeList = 
                configurationXmlNode.SelectNodes("./instrumentation:property",
                    new XmlNamespaceManager());
            foreach(XmlNode propertyXmlNode in propertiesNodeList)
                properties.Add(propertyXmlNode.Attributes["name"].Value,
                    propertyXmlNode.Attributes["value"] == null ? propertyXmlNode.InnerText :
                    propertyXmlNode.Attributes["value"].Value);
        }
    }
}
