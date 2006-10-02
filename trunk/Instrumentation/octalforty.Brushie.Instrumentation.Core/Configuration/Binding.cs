using System.Collections.Generic;
using System.Xml;

using octalforty.Brushie.Instrumentation.Core.Exceptions;
using octalforty.Brushie.Instrumentation.Core.Resources;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Contains settings for <c>binding</c> configuration element.
    /// </summary>
    public class Binding
    {
        #region Private Constants
        private readonly string PersisterNamePropertyName = "persisterName";
        #endregion

        #region Private Member Variables
        private string persisterName = string.Empty;
        private string[] severities;
        private string[] sources;
        private string[] messages;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the name of the persister this binding is associated with.
        /// </summary>
        public string PersisterName
        {
            get { return persisterName; }
        }

        /// <summary>
        /// Gets a reference to the collection of severities for this binding.
        /// </summary>
        public string[] Severities
        {
            get { return severities; }
        }

        /// <summary>
        /// Gets a reference to the collection of sources for this binding.
        /// </summary>
        public string[] Sources
        {
            get { return sources; }
        }

        /// <summary>
        /// Gets a reference to the collection of messages for this binding.
        /// </summary>
        public string[] Messages
        {
            get { return messages; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Binding"/> class from a
        /// given XML node.
        /// </summary>
        /// <param name="bindingXmlNode"></param>
        public Binding(XmlNode bindingXmlNode)
        {
            if(bindingXmlNode.Attributes[PersisterNamePropertyName] == null)
                throw new ConfigurationException(
                    string.Format(Strings.Binding_Binding_RequiredPropertyMissing, 
                    PersisterNamePropertyName));
            
            persisterName = bindingXmlNode.Attributes[PersisterNamePropertyName].Value;

            severities = GetStringArray(bindingXmlNode.SelectNodes("./severity"));
            sources = GetStringArray(bindingXmlNode.SelectNodes("./source"));
            messages = GetStringArray(bindingXmlNode.SelectNodes("./message"));
        }

        private string[] GetStringArray(XmlNodeList xmlNodeList)
        {
            List<string> strings = new List<string>();
            
            foreach(XmlNode xmlNode in xmlNodeList)
            {
                string item = xmlNode.InnerText;
                string[] items = item.Split(',');
                
                foreach(string splitItem in items)
                    strings.Add(splitItem.Trim());
            } // foreach

            return strings.ToArray();
        }
    }
}
