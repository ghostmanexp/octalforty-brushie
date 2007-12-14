using System.Web;

namespace octalforty.Brushie.Web
{
    /// <summary>
    /// Provides functionality for muting Forms Authentication stuff.
    /// </summary>
#if FW2
    public static class FormsAuthenticationMuter
#else
    public sealed class FormsAuthenticationMuter
#endif
    {
        #region Private Constants
        private const string FormsAuthenticationMuterKey = "FormsAuthenticationMuter";
        #endregion

        /// <summary>
        /// Mutes Forms Authentication.
        /// </summary>
        /// <param name="httpContext"></param>
        public static void MuteFormsAuthentication(HttpContext httpContext)
        {
            if(ShouldMuteFormsAuthentication(httpContext))
            {
                httpContext.Response.StatusCode = 401;
                httpContext.Response.StatusDescription = "Unauthorized";
            } // if
        }

        /// <summary>
        /// Requires that Forms Authentication should be muted.
        /// </summary>
        /// <param name="httpContext"></param>
        public static void RequireMuteFormsAuthentication(HttpContext httpContext)
        {
            httpContext.Items[FormsAuthenticationMuterKey] = true;
        }

        /// <summary>
        /// Returns a value which indicates whther Forms Authentication should be muted.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static bool ShouldMuteFormsAuthentication(HttpContext httpContext)
        {
            return httpContext.Items[FormsAuthenticationMuterKey] != null &&
                (bool)httpContext.Items[FormsAuthenticationMuterKey];
        }
    }
}
