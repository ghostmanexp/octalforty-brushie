using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace octalforty.Brushie.Web
{
    /// <summary>
    /// A <see cref="HttpRequest"/>-based implementation of the <see cref="IHttpRequest"/>.
    /// </summary>
    public class HttpRequestHttpRequest : IHttpRequest
    {
        #region Private Member Variables
        private HttpRequest httpRequest;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="HttpRequestHttpRequest"/> class.
        /// </summary>
        /// <param name="httpRequest"></param>
        public HttpRequestHttpRequest(HttpRequest httpRequest)
        {
            this.httpRequest = httpRequest;
        }

        #region IHttpRequest Members
        /// <summary>
        /// Performs a binary read of a specified number of bytes from the current input stream.
        /// </summary>
        /// 
        /// <returns>
        /// A byte array.
        /// </returns>
        /// 
        /// <param name="count">The number of bytes to read. </param>
        /// <exception cref="System.ArgumentException">count is 0.- or -count is greater than the number of bytes available. </exception>
        public byte[] BinaryRead(int count)
        {
            return httpRequest.BinaryRead(count);
        }

        /// <summary>
        /// Validates data submitted by a client browser and raises an exception if potentially dangerous data is present.
        /// </summary>
        /// 
        /// <exception cref="System.Web.HttpRequestValidationException">Potentially dangerous data was received from the client. </exception>
        public void ValidateInput()
        {
            httpRequest.ValidateInput();
        }

        /// <summary>
        /// Maps an incoming image-field form parameter to appropriate x-coordinate and y-coordinate values.
        /// </summary>
        /// 
        /// <returns>
        /// A two-dimensional array of integers.
        /// </returns>
        /// 
        /// <param name="imageFieldName">The name of the form image map. </param>
        public int[] MapImageCoordinates(string imageFieldName)
        {
            return httpRequest.MapImageCoordinates(imageFieldName);
        }

        /// <summary>
        /// Saves an HTTP request to disk.
        /// </summary>
        /// 
        /// <param name="includeHeaders">A Boolean value specifying whether an HTTP header should be saved to disk. </param>
        /// <param name="filename">The physical drive path. </param>
        public void SaveAs(string filename, bool includeHeaders)
        {
            httpRequest.SaveAs(filename, includeHeaders);
        }

        /// <summary>
        /// Maps the specified virtual path to a physical path.
        /// </summary>
        /// 
        /// <returns>
        /// The physical path on the server specified by virtualPath.
        /// </returns>
        /// 
        /// <param name="virtualPath">The virtual path (absolute or relative) for the current request. </param>
        /// <exception cref="System.Web.HttpException">No <see cref="System.Web.HttpContext"></see> object is defined for the request. </exception>
        public string MapPath(string virtualPath)
        {
            return httpRequest.MapPath(virtualPath);
        }

        /// <summary>
        /// Maps the specified virtual path to a physical path.
        /// </summary>
        /// 
        /// <returns>
        /// The physical path on the server.
        /// </returns>
        /// 
        /// <param name="baseVirtualDir">The virtual base directory path used for relative resolution. </param>
        /// <param name="virtualPath">The virtual path (absolute or relative) for the current request. </param>
        /// <param name="allowCrossAppMapping">true to indicate that virtualPath may belong to another application; otherwise, false. </param>
        /// <exception cref="System.Web.HttpException">allowCrossMapping is false and virtualPath belongs to another application. </exception>
        /// <exception cref="System.Web.HttpException">No <see cref="System.Web.HttpContext"></see> object is defined for the request. </exception>
        public string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
        {
            return httpRequest.MapPath(virtualPath, baseVirtualDir, allowCrossAppMapping);
        }

        /// <summary>
        /// Gets a value indicating whether the request is from the local computer.
        /// </summary>
        /// 
        /// <returns>
        /// true if the request is from the local computer; otherwise, false.
        /// </returns>
        /// 
        public bool IsLocal
        {
            get { return httpRequest.IsLocal; }
        }

        /// <summary>
        /// Gets the HTTP data transfer method (such as GET, POST, or HEAD) used by the client.
        /// </summary>
        /// 
        /// <returns>
        /// The HTTP data transfer method used by the client.
        /// </returns>
        /// 
        public string HttpMethod
        {
            get { return httpRequest.HttpMethod; }
        }

        /// <summary>
        /// Gets or sets the HTTP data transfer method (GET or POST) used by the client.
        /// </summary>
        /// 
        /// <returns>
        /// A string representing the HTTP invocation type sent by the client.
        /// </returns>
        /// 
        public string RequestType
        {
            get { return httpRequest.RequestType; }
            set { httpRequest.RequestType = value; }
        }

        /// <summary>
        /// Gets or sets the MIME content type of the incoming request.
        /// </summary>
        /// 
        /// <returns>
        /// A string representing the MIME content type of the incoming request, for example, "text/html".
        /// </returns>
        /// 
        public string ContentType
        {
            get { return httpRequest.ContentType; }
            set { httpRequest.ContentType = value; }
        }

        /// <summary>
        /// Specifies the length, in bytes, of content sent by the client.
        /// </summary>
        /// 
        /// <returns>
        /// The length, in bytes, of content sent by the client.
        /// </returns>
        /// 
        public int ContentLength
        {
            get { return httpRequest.ContentLength; }
        }

        /// <summary>
        /// Gets or sets the character set of the entity-body.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="System.Text.Encoding"></see> object representing the client's character set.
        /// </returns>
        /// 
        public Encoding ContentEncoding
        {
            get { return httpRequest.ContentEncoding; }
            set { httpRequest.ContentEncoding = value; }
        }

        /// <summary>
        /// Gets a string array of client-supported MIME accept types.
        /// </summary>
        /// 
        /// <returns>
        /// A string array of client-supported MIME accept types.
        /// </returns>
        /// 
        public string[] AcceptTypes
        {
            get { return httpRequest.AcceptTypes; }
        }

        /// <summary>
        /// Gets a value indicating whether the request has been authenticated.
        /// </summary>
        /// 
        /// <returns>
        /// true if the request is authenticated; otherwise, false.
        /// </returns>
        /// 
        public bool IsAuthenticated
        {
            get { return httpRequest.IsAuthenticated; }
        }

        /// <summary>
        /// Gets a value indicting whether the HTTP connection uses secure sockets (that is, HTTPS).
        /// </summary>
        /// 
        /// <returns>
        /// true if the connection is an SSL connection; otherwise, false.
        /// </returns>
        /// 
        public bool IsSecureConnection
        {
            get { return httpRequest.IsSecureConnection; }
        }

        /// <summary>
        /// Gets the virtual path of the current request.
        /// </summary>
        /// 
        /// <returns>
        /// The virtual path of the current request.
        /// </returns>
        /// 
        public string Path
        {
            get { return httpRequest.Path; }
        }

        /// <summary>
        /// Gets the anonymous identifier for the user, if present.
        /// </summary>
        /// 
        /// <returns>
        /// A string representing the current anonymous user identifier.
        /// </returns>
        /// 
        public string AnonymousID
        {
            get { return httpRequest.AnonymousID; }
        }

        /// <summary>
        /// Gets the virtual path of the current request.
        /// </summary>
        /// 
        /// <returns>
        /// The virtual path of the current request.
        /// </returns>
        /// 
        public string FilePath
        {
            get { return httpRequest.FilePath; }
        }

        /// <summary>
        /// Gets the virtual path of the current request.
        /// </summary>
        /// 
        /// <returns>
        /// The virtual path of the current request.
        /// </returns>
        /// 
        public string CurrentExecutionFilePath
        {
            get { return httpRequest.CurrentExecutionFilePath; }
        }

        /// <summary>
        /// Gets the virtual path of the application root and makes it relative by using the tilde (~) notation for the application root (as in "~/page.aspx").
        /// </summary>
        /// 
        /// <returns>
        /// The virtual path of the application root for the current request.
        /// </returns>
        /// 
        public string AppRelativeCurrentExecutionFilePath
        {
            get { return httpRequest.AppRelativeCurrentExecutionFilePath; }
        }

        /// <summary>
        /// Gets additional path information for a resource with a URL extension.
        /// </summary>
        /// 
        /// <returns>
        /// Additional path information for a resource.
        /// </returns>
        /// 
        public string PathInfo
        {
            get { return httpRequest.PathInfo; }
        }

        /// <summary>
        /// Gets the physical file system path corresponding to the requested URL.
        /// </summary>
        /// 
        /// <returns>
        /// The file system path of the current request.
        /// </returns>
        /// 
        public string PhysicalPath
        {
            get { return httpRequest.PhysicalPath; }
        }

        /// <summary>
        /// Gets the ASP.NET application's virtual application root path on the server.
        /// </summary>
        /// 
        /// <returns>
        /// The virtual path of the current application.
        /// </returns>
        /// 
        public string ApplicationPath
        {
            get { return httpRequest.ApplicationPath; }
        }

        /// <summary>
        /// Gets the physical file system path of the currently executing server application's root directory.
        /// </summary>
        /// 
        /// <returns>
        /// The file system path of the current application's root directory.
        /// </returns>
        /// 
        public string PhysicalApplicationPath
        {
            get { return httpRequest.PhysicalApplicationPath; }
        }

        /// <summary>
        /// Gets the raw user agent string of the client browser.
        /// </summary>
        /// 
        /// <returns>
        /// The raw user agent string of the client browser.
        /// </returns>
        /// 
        public string UserAgent
        {
            get { return httpRequest.UserAgent; }
        }

        /// <summary>
        /// Gets a sorted string array of client language preferences.
        /// </summary>
        /// 
        /// <returns>
        /// A sorted string array of client language preferences, or null if empty.
        /// </returns>
        /// 
        public string[] UserLanguages
        {
            get { return httpRequest.UserLanguages; }
        }

        /// <summary>
        /// Gets or sets information about the requesting client's browser capabilities.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="System.Web.HttpBrowserCapabilities"></see> object listing the capabilities of the client's browser.
        /// </returns>
        /// 
        public HttpBrowserCapabilities Browser
        {
            get { return httpRequest.Browser; }
            set { httpRequest.Browser = value; }
        }

        /// <summary>
        /// Gets the DNS name of the remote client.
        /// </summary>
        /// 
        /// <returns>
        /// The DNS name of the remote client.
        /// </returns>
        /// 
        public string UserHostName
        {
            get { return httpRequest.UserHostName; }
        }

        /// <summary>
        /// Gets the IP host address of the remote client.
        /// </summary>
        /// 
        /// <returns>
        /// The IP address of the remote client.
        /// </returns>
        /// 
        public string UserHostAddress
        {
            get { return httpRequest.UserHostAddress; }
        }

        /// <summary>
        /// Gets the raw URL of the current request.
        /// </summary>
        /// 
        /// <returns>
        /// The raw URL of the current request.
        /// </returns>
        /// 
        public string RawUrl
        {
            get { return httpRequest.RawUrl; }
        }

        /// <summary>
        /// Gets information about the URL of the current request.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="System.Uri"></see> object containing information regarding the URL of the current request.
        /// </returns>
        /// 
        public Uri Url
        {
            get { return httpRequest.Url; }
        }

        /// <summary>
        /// Gets information about the URL of the client's previous request that linked to the current URL.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="System.Uri"></see> object.
        /// </returns>
        /// 
        public Uri UrlReferrer
        {
            get { return httpRequest.UrlReferrer; }
        }

        /// <summary>
        /// Gets a combined collection of <see cref="P:System.Web.HttpRequest.QueryString"></see>, <see cref="P:System.Web.HttpRequest.Form"></see>, <see cref="P:System.Web.HttpRequest.ServerVariables"></see>, and <see cref="P:System.Web.HttpRequest.Cookies"></see> items.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="System.Collections.Specialized.NameValueCollection"></see> object.
        /// </returns>
        /// 
        public NameValueCollection Params
        {
            get { return httpRequest.Params; }
        }

        /// <summary>
        /// Gets the specified object from the <see cref="P:System.Web.HttpRequest.Cookies"></see>, <see cref="P:System.Web.HttpRequest.Form"></see>, <see cref="P:System.Web.HttpRequest.QueryString"></see> or <see cref="P:System.Web.HttpRequest.ServerVariables"></see> collections.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="P:System.Web.HttpRequest.QueryString"></see>, <see cref="P:System.Web.HttpRequest.Form"></see>, <see cref="P:System.Web.HttpRequest.Cookies"></see>, or <see cref="P:System.Web.HttpRequest.ServerVariables"></see> collection member specified in the key parameter. If the specified key is not found, then null is returned.
        /// </returns>
        /// 
        /// <param name="key">The name of the collection member to get. </param>
        public string this[string key]
        {
            get { return httpRequest[key]; }
        }

        /// <summary>
        /// Gets the collection of HTTP query string variables.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="System.Collections.Specialized.NameValueCollection"></see> containing the collection of query string variables sent by the client.
        /// </returns>
        /// 
        public NameValueCollection QueryString
        {
            get { return httpRequest.QueryString; }
        }

        /// <summary>
        /// Gets a collection of form variables.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="System.Collections.Specialized.NameValueCollection"></see> representing a collection of form variables.
        /// </returns>
        /// 
        public NameValueCollection Form
        {
            get { return httpRequest.Form; }
        }

        /// <summary>
        /// Gets a collection of HTTP headers.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="System.Collections.Specialized.NameValueCollection"></see> of headers.
        /// </returns>
        /// 
        public NameValueCollection Headers
        {
            get { return httpRequest.Headers; }
        }

        /// <summary>
        /// Gets a collection of Web server variables.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="System.Collections.Specialized.NameValueCollection"></see> of server variables.
        /// </returns>
        /// 
        public NameValueCollection ServerVariables
        {
            get { return httpRequest.ServerVariables; }
        }

        /// <summary>
        /// Gets a collection of cookies sent by the client.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="System.Web.HttpCookieCollection"></see> object representing the client's cookie variables.
        /// </returns>
        /// 
        public HttpCookieCollection Cookies
        {
            get { return httpRequest.Cookies; }
        }

        /// <summary>
        /// Gets the collection of files uploaded by the client, in multipart MIME format.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="System.Web.HttpFileCollection"></see> object representing a collection of files uploaded by the client.
        /// </returns>
        /// 
        public HttpFileCollection Files
        {
            get { return httpRequest.Files; }
        }

        /// <summary>
        /// Gets the contents of the incoming HTTP entity body.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="System.IO.Stream"></see> object representing the contents of the incoming HTTP content body.
        /// </returns>
        /// 
        public Stream InputStream
        {
            get { return httpRequest.InputStream; }
        }

        /// <summary>
        /// Gets the number of bytes in the current input stream.
        /// </summary>
        /// 
        /// <returns>
        /// The number of bytes in the input stream.
        /// </returns>
        /// 
        public int TotalBytes
        {
            get { return httpRequest.TotalBytes; }
        }

        /// <summary>
        /// Gets or sets the filter to use when reading the current input stream.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="System.IO.Stream"></see> object to be used as the filter.
        /// </returns>
        /// 
        /// <exception cref="System.Web.HttpException">The specified <see cref="System.IO.Stream"></see> is invalid.</exception>
        public Stream Filter
        {
            get { return httpRequest.Filter; }
            set { httpRequest.Filter = value; }
        }

        /// <summary>
        /// Gets the current request's client security certificate.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="System.Web.HttpClientCertificate"></see> object containing information about the client's security certificate settings.
        /// </returns>
        /// 
        public HttpClientCertificate ClientCertificate
        {
            get { return httpRequest.ClientCertificate; }
        }

        /// <summary>
        /// Gets the <see cref="System.Security.Principal.WindowsIdentity"></see> type for the current user.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="System.Security.Principal.WindowsIdentity"></see> for the current Microsoft Internet Information Services (IIS) authentication settings.
        /// </returns>
        /// 
        public WindowsIdentity LogonUserIdentity
        {
            get { return httpRequest.LogonUserIdentity; }
        }
        #endregion
    }
}
