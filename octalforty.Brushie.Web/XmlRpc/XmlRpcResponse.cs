namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Represents an XML-RPC response.
    /// </summary>
    public class XmlRpcResponse
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
        /// Initializes a new instance of <see cref="XmlRpcResponse"/> class.
        /// </summary>
        /// <param name="returnValue"></param>
        public XmlRpcResponse(object returnValue)
        {
            this.returnValue = returnValue;
        }
    }
}
