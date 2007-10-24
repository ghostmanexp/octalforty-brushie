namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// An XML-RPC structure which represents a &lt;fault> element.
    /// </summary>
    [XmlRpcStructure()]
    public sealed class XmlRpcFault
    {
        #region Private Member Variables
        private int code;
        private string message;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value which contains fault code.
        /// </summary>
        [XmlRpcMember("faultCode")]
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>
        /// Gets or sets a string which contains fault message.
        /// </summary>
        [XmlRpcMember("faultString")]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcFault"/> class.
        /// </summary>
        public XmlRpcFault()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcFault"/> class.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public XmlRpcFault(int code, string message)
        {
            this.code = code;
            this.message = message;
        }
    }
}
