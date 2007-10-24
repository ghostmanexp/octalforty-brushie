namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// XML-RPC Service Dispatcher.
    /// </summary>
    public interface IXmlRpcServiceDispatcher
    {
        /// <summary>
        /// Dispatches a request to the XML-RPC service.
        /// </summary>
        /// <param name="xmlRpcServiceContext"></param>
        void Dispatch(IXmlRpcServiceContext xmlRpcServiceContext);
    }
}