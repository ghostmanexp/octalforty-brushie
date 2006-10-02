using System.Collections.Generic;
using System.Configuration;
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

        #region Public Static Properties
        /// <summary>
        /// Gets a reference to the instance of <see cref="ConfigurationSettings"/> class.
        /// </summary>
        public static ConfigurationSettings Instance
        {
            get
            {
                return ConfigurationManager.GetSection("octalforty.brushie.instrumentation")
                    as ConfigurationSettings;
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationSettings"/> class
        /// from a given XML node.
        /// </summary>
        /// <param name="configurationXmlNode"></param>
        public ConfigurationSettings(XmlNode configurationXmlNode)
        {
            System.Xml.XmlNamespaceManager namespaceManager =
                new XmlNamespaceManager();
            InitializePersisters(configurationXmlNode, namespaceManager);
            InitializeMessages(configurationXmlNode, namespaceManager);
            InitializeFormatters(configurationXmlNode, namespaceManager);
            InitializeBindings(configurationXmlNode, namespaceManager);
        }

        /// <summary>
        /// Initializes persisters.
        /// </summary>
        /// <param name="configurationXmlNode"></param>
        private void InitializePersisters(XmlNode configurationXmlNode, 
            System.Xml.XmlNamespaceManager namespaceManager)
        {
            XmlNodeList persistersNodeList =
                configurationXmlNode.SelectNodes("./instrumentation:persisters/instrumentation:persister", 
                namespaceManager);
            List<Persister> persistersList = new List<Persister>();

            foreach(XmlNode persisterXmlNode in persistersNodeList)
                persistersList.Add(new Persister(persisterXmlNode));

            persisters = persistersList.ToArray();
            
        }

        /// <summary>
        /// Initializes messages.
        /// </summary>
        /// <param name="configurationXmlNode"></param>
        private void InitializeMessages(XmlNode configurationXmlNode, 
            System.Xml.XmlNamespaceManager namespaceManager)
        {
            XmlNodeList messagesNodeList =
                configurationXmlNode.SelectNodes("./instrumentation:messages/instrumentation:message", 
                namespaceManager);
            List<Message> messagesList = new List<Message>();

            foreach(XmlNode messageXmlNode in messagesNodeList)
                messagesList.Add(new Message(messageXmlNode));

            messages = messagesList.ToArray();
        }

        /// <summary>
        /// Initializes formatters.
        /// </summary>
        /// <param name="configurationXmlNode"></param>
        private void InitializeFormatters(XmlNode configurationXmlNode, 
            System.Xml.XmlNamespaceManager namespaceManager)
        {
            XmlNodeList formattersNodeList =
                configurationXmlNode.SelectNodes("./instrumentation:formatters/instrumentation:formatter", 
                namespaceManager);
            List<Formatter> formattersList = new List<Formatter>();

            foreach(XmlNode formatterXmlNode in formattersNodeList)
                formattersList.Add(new Formatter(formatterXmlNode));

            formatters = formattersList.ToArray();
        }

        /// <summary>
        /// Initializes bindings.
        /// </summary>
        /// <param name="configurationXmlNode"></param>
        private void InitializeBindings(XmlNode configurationXmlNode, 
            System.Xml.XmlNamespaceManager namespaceManager)
        {
            XmlNodeList bindingsNodeList =
                configurationXmlNode.SelectNodes("./instrumentation:bindings/instrumentation:binding", 
                namespaceManager);
            List<Binding> bindingsList = new List<Binding>();
            
            foreach(XmlNode bindingXmlNode in bindingsNodeList)
                bindingsList.Add(new Binding(bindingXmlNode));

            bindings = bindingsList.ToArray();
        }
    }
}