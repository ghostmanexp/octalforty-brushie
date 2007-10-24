using System.Collections;

using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc
{
    /// <summary>
    /// <see cref="XmlRpcServiceInfo"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class XmlRpcServiceInfoTestFixture
    {
        [Test()]
        public void CreateXmlRpcServiceInfo()
        {
            XmlRpcServiceInfo serviceInfo = 
                XmlRpcServiceInfo.CreateXmlRpcServiceInfo(typeof(MathXmlRpcService));

            Assert.IsNotEmpty((ICollection)serviceInfo.Methods);

            Assert.AreEqual("math:add", serviceInfo.Methods[0].Name);
            Assert.AreEqual(typeof(int), serviceInfo.Methods[0].ParameterTypes[0]);
            Assert.AreEqual(typeof(int), serviceInfo.Methods[0].ParameterTypes[1]);
        }
    }
}
