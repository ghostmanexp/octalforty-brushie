namespace octalforty.Brushie.Xml
{
    /// <summary>
    /// Represents XML attribute for use in <see cref="XmlBuilder"/>.
    /// </summary>
    internal class XmlAttribute
    {
        #region Private Member Variables
        private string name;
        private string value;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets attribute name.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets attribute value.
        /// </summary>
        public string Value
        {
            get { return value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlAttribute"/> class with given values.
        /// </summary>
        /// <param name="name">Attribute name. Cannot be null or <see cref="string.Empty"/>.</param>
        /// <param name="value">Attribute name. Cannot be null.</param>
        public XmlAttribute(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
