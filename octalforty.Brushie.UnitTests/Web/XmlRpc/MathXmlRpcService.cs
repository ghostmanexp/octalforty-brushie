using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc
{
    [XmlRpcService("Math XML-RPC Service", 
        "Provides an XML-RPC endpoint for trigonometric, logarithmic, and other common mathematical functions. ")]
    public class MathXmlRpcService : IMathXmlRpcService
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Subtract(int x, int y)
        {
            return x - y;
        }
    }
}
