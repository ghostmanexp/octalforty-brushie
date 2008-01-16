using System.IO;
using System.Net;
using System.Web;
using System.Web.SessionState;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Provides a base class for creating XML-RPC services.
    /// </summary>
    public abstract class XmlRpcService : IHttpHandler, IRequiresSessionState, IXmlRpcService
    {
        #region Private Member Variables
        private HttpContext context;
        private XmlRpcServiceDispatcher xmlRpcServiceDispatcher;
        private XmlRpcServiceIntrospector xmlRpcServiceIntrospector;
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
            xmlRpcServiceIntrospector = new XmlRpcServiceIntrospector(GetType());
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
            {
                using(MemoryStream memoryStream = new MemoryStream())
                {
                    xmlRpcServiceDispatcher.Dispatch(new XmlRpcServiceContext(this, 
                        httpContext.Request.InputStream, memoryStream));

                    httpContext.Response.Clear();
                    httpContext.Response.ClearContent();
                    httpContext.Response.ClearHeaders();

                    httpContext.Response.ContentType = "text/xml";
                    httpContext.Response.StatusCode = (int)HttpStatusCode.OK;

                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.WriteTo(httpContext.Response.OutputStream);

                    httpContext.Response.End();
                } // using
            } // if

            if(Context.Request.HttpMethod.ToUpper() == "GET")
            {
                httpContext.Response.Clear();
                httpContext.Response.ClearContent();
                httpContext.Response.ClearHeaders();

                httpContext.Response.ContentType = "text/html";
                httpContext.Response.StatusCode = (int)HttpStatusCode.OK;

                xmlRpcServiceIntrospector.Introspect(httpContext.Response.OutputStream);
            } // if
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
    }
}
