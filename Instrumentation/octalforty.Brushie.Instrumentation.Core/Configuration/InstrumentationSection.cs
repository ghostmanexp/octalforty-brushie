using System.Configuration;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// <c>octalforty.brushie.instrumentation</c> configuration section.
    /// </summary>
    public sealed class InstrumentationSection : ConfigurationSection
    {
        #region Public Properties
        /// <summary>
        /// Gets a collection of persister configuration objects.
        /// </summary>
        [ConfigurationProperty("persisters", IsRequired = false)]
        [ConfigurationCollection(typeof(PersisterElementCollection))]
        public PersisterElementCollection Persisters
        {
            get { return this["persisters"] as PersisterElementCollection; }
        }
        
        /// <summary>
        /// Gets a collection of message configuration objects.
        /// </summary>
        [ConfigurationProperty("messages", IsRequired = false)]
        [ConfigurationCollection(typeof(MessageElementCollection))]
        public MessageElementCollection Messages
        {
            get { return this["messages"] as MessageElementCollection; }
        }

        /// <summary>
        /// Gets a collection of formatter configuration objects.
        /// </summary>
        [ConfigurationProperty("formatters", IsRequired = false)]
        [ConfigurationCollection(typeof(FormatterElementCollection))]
        public FormatterElementCollection Formatters
        {
            get { return this["formatters"] as FormatterElementCollection; }
        }

        /// <summary>
        /// Gets a collection of binding configuration objects.
        /// </summary>
        [ConfigurationProperty("bindings", IsRequired = true)]
        [ConfigurationCollection(typeof(BindingElementCollection))]
        public BindingElementCollection Bindings
        {
            get { return this["bindings"] as BindingElementCollection; }
        }

        /// <summary>
        /// Gets a string with the XML namespace of the configuration section.
        /// </summary>
        [ConfigurationProperty("xmlns", IsRequired = true)]
        public string XmlNamespace
        {
            get { return this["xmlns"] as string; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="InstrumentationSection"/> class.
        /// </summary>
        public InstrumentationSection()
        {
        }
    }
}
