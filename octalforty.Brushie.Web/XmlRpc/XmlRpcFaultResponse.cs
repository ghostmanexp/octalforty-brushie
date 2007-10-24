namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Represents XML-RPC Fault response.
    /// </summary>
    public sealed class XmlRpcFaultResponse : XmlRpcResponse
    {
        #region Private Member Variables
        private XmlRpcFault fault = new XmlRpcFault();
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a reference to the <see cref="XmlRpcFault"/>, which
        /// describes the error.
        /// </summary>
        public XmlRpcFault Fault
        {
            get { return fault; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpc"/> class.
        /// </summary>
        public XmlRpcFaultResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpc"/> class.
        /// </summary>
        /// <param name="fault"></param>
        public XmlRpcFaultResponse(XmlRpcFault fault)
        {
            this.fault = fault;
        }
    }
}
