using System.Configuration;

using octalforty.Brushie.Configuration;

namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Represents a configuration element for a binging.
    /// </summary>
    public sealed class BindingElement : ConfigurationElementBase
    {
        #region Private Constants
        private const string PersisterNamePropertyName = "persisterName";
        private const string SeverityPropertyName = "severity";
        private const string SourcePropertyName = "source";
        private const string MessagePropertyName = "message";
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a string with the name of the persister this binding is associated with.
        /// </summary>
        [ConfigurationProperty(PersisterNamePropertyName, IsRequired = true)]
        public string PersisterName
        {
            get { return this[PersisterNamePropertyName] as string; }
            set { this[PersisterNamePropertyName] = value; }
        }
        
        /// <summary>
        /// Gets or sets a string with the comma-separated list of severities for this binding.
        /// </summary>
        [ConfigurationProperty(SeverityPropertyName, IsRequired = false)]
        public string Severity
        {
            get { return this[SeverityPropertyName] as string; }
            set { this[SeverityPropertyName] = value; }
        }

        /// <summary>
        /// Gets or sets a string with the comma-separated list of sources for this binding.
        /// </summary>
        [ConfigurationProperty(SourcePropertyName, IsRequired = false)]
        public string Source
        {
            get { return this[SourcePropertyName] as string; }
            set { this[SourcePropertyName] = value; }
        }

        /// <summary>
        /// Gets or sets a string with the comma-separated list of messages for this binding.
        /// </summary>
        [ConfigurationProperty(MessagePropertyName, IsRequired = false)]
        public string Message
        {
            get { return this[MessagePropertyName] as string; }
            set { this[MessagePropertyName] = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="BindingElement"/> class.
        /// </summary>
        public BindingElement()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="BindingElement"/> class with a
        /// given persister name, severity, source and message.
        /// </summary>
        /// <param name="persisterName"></param>
        /// <param name="severity"></param>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public BindingElement(string persisterName, string severity, string source, string message)
        {
            PersisterName = persisterName;
            Severity = severity;
            Source = source;
            Message = message;
        }
    }
}
