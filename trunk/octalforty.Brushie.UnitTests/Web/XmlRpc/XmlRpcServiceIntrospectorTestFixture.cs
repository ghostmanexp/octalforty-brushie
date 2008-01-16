using System.IO;
using System.Text;

using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc
{
    [TestFixture()]
    public class XmlRpcServiceIntrospectorTestFixture
    {
        [Test()]
        public void Introspect()
        {
            XmlRpcServiceIntrospector xmlRpcServiceIntrospector = 
                new XmlRpcServiceIntrospector(typeof(MathXmlRpcService));

            using(MemoryStream memoryStream = new MemoryStream())
            {
                xmlRpcServiceIntrospector.Introspect(memoryStream);
                string html = Encoding.UTF8.GetString(memoryStream.ToArray());
            } // using
        }
    }
}
