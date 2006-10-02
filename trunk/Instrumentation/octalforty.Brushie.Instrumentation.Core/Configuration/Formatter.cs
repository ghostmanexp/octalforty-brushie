using System.Xml;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Contains settings for <c>formatter</c> configuration element.
    /// </summary>
    public class Formatter : ConfigurationObjectWithType
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Formatter"/> class from
        /// a given XML node.
        /// </summary>
        /// <param name="formatterXmlNode"></param>
        public Formatter(XmlNode formatterXmlNode) :
            base(formatterXmlNode)
        {
        }
    }
}
