using System.IO;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Provides support for invoking XML-RPC service methods.
    /// </summary>
    public interface IXmlRpcWebRequest
    {
        /// <summary>
        /// Gets a reference to the <see cref="Stream"/> which is used to write
        /// data to the XML-RPC service.
        /// </summary>
        Stream RequestStream
        { get; }

        IXmlRpcWebResponse Invoke();
    }
}
