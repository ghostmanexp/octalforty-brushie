using System.Runtime.Serialization;

using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc
{
    public interface IMathXmlRpcService : IXmlRpcService, IDummyMarkerInterface
    {
        [XmlRpcServiceMethod("math:add")]
        int Add(int x, int y);
    }
}