namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Represents an XML-RPC request to the XML-RPC service.
    /// </summary>
    public sealed class XmlRpcRequest
    {
        #region Private Member Variables
        private string methodName;
        private object[] parameters;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a string which contains the name of the method to be invoked.
        /// </summary>
        public string MethodName
        {
            get { return methodName; }
        }

        /// <summary>
        /// Gets an array which contains parameters for the method.
        /// </summary>
        public object[] Parameters
        {
            get { return parameters; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcRequest"/> class.
        /// </summary>
        public XmlRpcRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcRequest"/> class.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        public XmlRpcRequest(string methodName, params object[] parameters)
        {
            this.methodName = methodName;
            this.parameters = parameters;
        }
    }
}
