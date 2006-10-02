using System.Collections.Generic;
using System.Xml;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Contains configuration information for octalforty Brushie Instrumentation framework.
    /// </summary>
    public sealed class ConfigurationSettings
    {
        #region Private Member Variables
        private Persister[] persisters;
        private Message[] messages;
        private Formatter[] formatters;
        private Binding[] bindings;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a reference to the collection of persister settings.
        /// </summary>
        public Persister[] Persisters
        {
            get { return persisters; }
        }

        /// <summary>
        /// Gets a reference to the collection of message properties.
        /// </summary>
        public Message[] Messages
        {
            get { return messages; }
        }

        /// <summary>
        /// Gets a reference to the collection of formatter properties.
        /// </summary>
        public Formatter[] Formatters
        {
            get { return formatters; }
        }

        /// <summary>
        /// Gets a reference to the collection of bindings.
        /// </summary>
        public Binding[] Bindings
        {
            get { return bindings; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationSettings"/> class
        /// from a given XML node.
        /// </summary>
        /// <param name="configurationXmlNode"></param>
        public ConfigurationSettings(XmlNode configurationXmlNode)
        {
            //
            // Initializing bindings
            XmlNodeList bindingsNodeList = configurationXmlNode.SelectNodes("./bindings/binding");
            List<Binding> bindingsList = new List<Binding>();
            
            foreach(XmlNode bindingXmlNode in bindingsNodeList)
                bindingsList.Add(new Binding(bindingXmlNode));

            bindings = bindingsList.ToArray();
        }
    }
}
