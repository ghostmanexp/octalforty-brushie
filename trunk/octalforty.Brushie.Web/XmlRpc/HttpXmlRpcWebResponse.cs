using System.IO;
using System.Net;

namespace octalforty.Brushie.Web.XmlRpc
{
    public class HttpXmlRpcWebResponse : IXmlRpcWebResponse
    {
        #region Private Member Variables
        private WebResponse webResponse;
        private Stream responseStream;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="HttpXmlRpcWebResponse"/> class.
        /// </summary>
        /// <param name="webResponse"></param>
        public HttpXmlRpcWebResponse(WebResponse webResponse)
        {
            this.webResponse = webResponse;

            responseStream = this.webResponse.GetResponseStream();
        }

        #region IXmlRpcWebResponse Members
        public Stream ResponseStream
        {
            get { return responseStream; }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            responseStream.Close();
            webResponse.Close();
        }
        #endregion
    }
}
