using System;
using System.IO;

namespace octalforty.Brushie.Web.XmlRpc
{
    public interface IXmlRpcWebResponse : IDisposable
    {
        Stream ResponseStream
        { get; }
    }
}
