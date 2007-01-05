namespace octalforty.Brushie.Instrumentation.Core.Configuration
{
    /// <summary>
    /// Represents a configuration element for a message.
    /// </summary>
    public sealed class MessageElement : ConfigurationElementWithNameAndType
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MessageElement"/> class.
        /// </summary>
        public MessageElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MessageElement"/> class
        /// with a given name and type.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public MessageElement(string name, string type) : 
            base(name, type)
        {
        }
    }
}
