using System;

namespace octalforty.Brushie.Web.Rest
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class HttpRequestHandlerAttribute : Attribute
    {
        private HttpMethod httpMethod;

        public HttpMethod HttpMethod
        {
            get { return httpMethod; }
        }

        public HttpRequestHandlerAttribute(HttpMethod httpMethod)
        {
            this.httpMethod = httpMethod;
        }
    }
}
