using System.Collections.Generic;
using System.Configuration;

namespace octalforty.Brushie.Configuration
{
    /// <summary>
    /// Provides a base class for configuration elements.
    /// </summary>
    /// <remarks>
    /// All unrecognized attributes are available via <see cref="Attributes"/> property.
    /// </remarks>
    public abstract class ConfigurationElementBase : ConfigurationElement
    {
        #region Private Member Variables
        private IDictionary<string, string> attributes = new Dictionary<string, string>();
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a reference to the <see cref="IDictionary{TKey,TValue}"/> which
        /// contains unrecognized attributes.
        /// </summary>
        public IDictionary<string, string> Attributes
        {
            get { return attributes; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationElementBase"/> class.
        /// </summary>
        protected ConfigurationElementBase()
        {
        }

        #region ConfigurationElement Members
        /// <summary>
        /// Gets a value indicating whether an unknown attribute is encountered 
        /// during deserialization.
        /// </summary>
        /// <returns>
        /// true when an unknown attribute is encountered while deserializing.
        /// </returns>
        /// <param name="name">The name of the unrecognized attribute.</param>
        /// <param name="value">The value of the unrecognized attribute.</param>
        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            Attributes.Add(name, value);
            return false;
        }
        #endregion
    }
}
