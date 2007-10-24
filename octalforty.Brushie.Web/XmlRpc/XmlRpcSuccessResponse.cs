namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Represents an XML-RPC response.
    /// </summary>
    public class XmlRpcSuccessResponse : XmlRpcResponse
    {
        #region Private Member Variables
        private object returnValue;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a return value of the method previously invoked with <see cref="XmlRpcRequest"/>.
        /// </summary>
        public object ReturnValue
        {
            get { return returnValue; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcSuccessResponse"/> class.
        /// </summary>
        /// <param name="returnValue"></param>
        public XmlRpcSuccessResponse(object returnValue)
        {
            this.returnValue = returnValue;
        }
    }
}
