using System.Web;
using System.Web.SessionState;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Provides a base class for creating XML-RPC services.
    /// </summary>
    public abstract class XmlRpcService : IHttpHandler, IRequiresSessionState
    {
        #region Private Member Variables
        private HttpContext context;
        private XmlRpcServiceDispatcher xmlRpcServiceDispatcher;
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets a <see cref="HttpContext"/> for the current request.
        /// </summary>
        protected HttpContext Context
        {
            get { return context; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcService"/> class.
        /// </summary>
        protected XmlRpcService()
        {
            xmlRpcServiceDispatcher = new XmlRpcServiceDispatcher(GetType());
        }

        #region IHttpHandler Members
        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that 
        /// implements the <see cref="System.Web.IHttpHandler"></see> interface.
        /// </summary>
        /// <param name="httpContext">
        /// An <see cref="System.Web.HttpContext"></see> object that provides references 
        /// to the intrinsic server objects (for example, Request, Response, Session, and Server) 
        /// used to service HTTP requests.
        /// </param>
        public virtual void ProcessRequest(HttpContext httpContext)
        {
            context = httpContext;

            if(Context.Request.HttpMethod.ToUpper() == "POST")
                xmlRpcServiceDispatcher.Dispatch(Context.Request.InputStream, 
                    Context.Response.OutputStream);
        }
        
        /// <summary>
        /// Gets a value indicating whether another request can use the 
        /// <see cref="System.Web.IHttpHandler"></see> instance.
        /// </summary>
        /// <returns>
        /// true if the <see cref="System.Web.IHttpHandler"></see> instance is reusable; 
        /// otherwise, false.
        /// </returns>
        public bool IsReusable
        {
            get { return false; }
        }
        #endregion

        /// <summary>
        /// Handles HTTP POST request.
        /// </summary>
        protected virtual void HandleHttpPost()
        {
            XmlRpcSerializer xmlRpcSerializer = new XmlRpcSerializer();
            XmlRpcRequest xmlRpcRequest = 
                xmlRpcSerializer.DeserializeRequest(Context.Request.InputStream,);
        }
    }
}
