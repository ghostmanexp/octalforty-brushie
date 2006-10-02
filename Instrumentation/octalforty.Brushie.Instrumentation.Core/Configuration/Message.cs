using System.Xml;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Contains configuration settings for <c>message</c> element.
    /// </summary>
    public class Message : ConfigurationObjectWithNameAndType
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Message"/> class with a 
        /// given XML node.
        /// </summary>
        /// <param name="configurationXmlNode"></param>
        public Message(XmlNode configurationXmlNode) : 
            base(configurationXmlNode)
        {
        }
    }
}
