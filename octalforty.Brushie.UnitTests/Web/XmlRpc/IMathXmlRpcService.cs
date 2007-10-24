using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc
{
    public interface IMathXmlRpcService : IXmlRpcService
    {
        [XmlRpcServiceMethod("math:add")]
        int Add(int x, int y);
    }
}