using System;
using System.Net;
using System.Text;
using System.Web;

namespace octalforty.Brushie.Web
{
    /// <summary>
    /// Provides a base class for HTTP Handlers which require Basic HTTP Authentication.
    /// </summary>
    public abstract class BasicHttpAuthenticatedHttpHandlerBase : IHttpHandler
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BasicHttpAuthenticatedHttpHandlerBase"/> class.
        /// </summary>
        protected BasicHttpAuthenticatedHttpHandlerBase()
        {
        }

        #region IHttpHandler Members
        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the 
        /// <see cref="System.Web.IHttpHandler"></see> interface.
        /// </summary>
        /// <param name="context">An <see cref="System.Web.HttpContext"></see> object that provides 
        /// references to the intrinsic server objects (for example, Request, Response, Session, and Server) 
        /// used to service HTTP requests. </param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ClearHeaders();

            string authorization = context.Request.Headers["Authorization"] == null ? 
				string.Empty : context.Request.Headers["Authorization"];
            string login = string.Empty;
            string password = string.Empty;

            if(authorization != string.Empty)
            {
                int userIndex = authorization.IndexOf(' ');
                string userInfo = Encoding.UTF8.GetString(Convert.FromBase64String(authorization.Substring(userIndex)));

                userIndex = userInfo.IndexOf(':');

                login = userInfo.Substring(0, userIndex) == null ? 
					string.Empty : userInfo.Substring(0, userIndex);
                password = userInfo.Substring(userIndex + 1) == null ? 
					string.Empty : userInfo.Substring(userIndex + 1);
            } // if

            int pos = login.IndexOf(@"\");
            if(pos > 0)
            {
                login = login.Substring(pos);
            } // if

            if(!IsValid(login, password))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.AppendHeader("WWW-Authenticate", "Basic realm=\"" + context.Request.Url.Host + "\"");

                FormsAuthenticationMuter.RequireMuteFormsAuthentication(context);

                return;
            } // if

            ProcessRequestAuthenticated(context, login, password);
        }

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="System.Web.IHttpHandler"></see> instance.
        /// </summary>
        /// <returns>
        /// true if the <see cref="System.Web.IHttpHandler"></see> instance is reusable; otherwise, false.
        /// </returns>
        public bool IsReusable
        {
            get { return false; }
        }
        #endregion

        #region Overridables
        /// <summary>
        /// Processes HTTP Authenticated request.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        protected abstract void ProcessRequestAuthenticated(HttpContext context, string login, string password);

        /// <summary>
        /// Verifies whether a given credentials are valid.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        protected abstract bool IsValid(string login, string password);
        #endregion
    }
}
