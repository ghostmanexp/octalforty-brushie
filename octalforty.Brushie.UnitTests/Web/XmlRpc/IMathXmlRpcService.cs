using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc
{
    public interface IMathXmlRpcService : IXmlRpcService, IDummyMarkerInterface
    {
        [XmlRpcServiceMethod("math:add", "Adds up two values")]
        int Add(int x, int y);

        [XmlRpcServiceMethod("math:subtract", "Subtracts second value from the first one")]
        int Subtract(int x, int y);
    }
}