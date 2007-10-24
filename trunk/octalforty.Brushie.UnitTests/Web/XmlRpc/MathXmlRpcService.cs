using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc
{
    public class MathXmlRpcService : IMathXmlRpcService
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
