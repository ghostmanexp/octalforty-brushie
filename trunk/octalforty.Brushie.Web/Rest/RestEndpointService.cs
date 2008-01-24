using System.Web;

namespace octalforty.Brushie.Web.Rest
{
    public class RestEndpointService : IHttpHandler
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RestEndpointService"/> class.
        /// </summary>
        public RestEndpointService()
        {
        }

        #region „лены IHttpHandler
        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the 
        /// <see cref="System.Web.IHttpHandler"></see> interface.
        /// </summary>
        /// <param name="context">An <see cref="System.Web.HttpContext"></see> object that provides 
        /// references to the intrinsic server objects (for example, Request, Response, Session, and Server) 
        /// used to service HTTP requests.
        /// </param>
        void IHttpHandler.ProcessRequest(HttpContext context)
        {
        }

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="System.Web.IHttpHandler"></see> instance.
        /// </summary>
        /// <returns>
        /// true if the <see cref="System.Web.IHttpHandler"></see> instance is reusable; otherwise, false.
        /// </returns> 
        bool IHttpHandler.IsReusable
        {
            get { return false; }
        }
        #endregion
    }
}
