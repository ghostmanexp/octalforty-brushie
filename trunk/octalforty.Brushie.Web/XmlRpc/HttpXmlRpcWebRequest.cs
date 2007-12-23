using System;
using System.IO;
using System.Net;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// A <see cref="IXmlRpcWebRequest"/> which uses <see cref="HttpWebRequest"/>.
    /// </summary>
    public class HttpXmlRpcWebRequest : IXmlRpcWebRequest
    {
        #region Private Member Variables
        private Stream requestStream;
        private HttpWebRequest httpWebRequest;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="HttpXmlRpcWebRequest"/>.
        /// </summary>
        /// <param name="serviceEndpointUri"></param>
        public HttpXmlRpcWebRequest(Uri serviceEndpointUri)
        {
            httpWebRequest = (HttpWebRequest)WebRequest.Create(serviceEndpointUri);

            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "text/xml";

            requestStream = httpWebRequest.GetRequestStream();
        }

        #region IXmlRpcWebRequest Members
        /// <summary>
        /// Gets a reference to the <see cref="Stream"/> which is used to write
        /// data to the XML-RPC service.
        /// </summary>
        public Stream RequestStream
        {
            get { return requestStream; }
        }

        public IXmlRpcWebResponse Invoke()
        {
            requestStream.Close();
            return new HttpXmlRpcWebResponse(httpWebRequest.GetResponse());
        }
        #endregion
    }
}
