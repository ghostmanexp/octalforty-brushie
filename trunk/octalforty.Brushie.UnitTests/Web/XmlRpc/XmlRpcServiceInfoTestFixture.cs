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

            Assert.AreEqual("Math XML-RPC Service", serviceInfo.Name);
            Assert.AreEqual("Provides an XML-RPC endpoint for trigonometric, logarithmic, and other common mathematical functions. ",
                serviceInfo.Description);

            Assert.AreEqual("math:add", serviceInfo.Methods[0].Name);
            Assert.AreEqual("Adds up two values", serviceInfo.Methods[0].Description);
            Assert.AreEqual(typeof(int), serviceInfo.Methods[0].ParameterTypes[0]);
            Assert.AreEqual(typeof(int), serviceInfo.Methods[0].ParameterTypes[1]);
        }

        [Test()]
        public void CreateXmlRpcServiceInfoForInterface()
        {
            XmlRpcServiceInfo serviceInfo =
                XmlRpcServiceInfo.CreateXmlRpcServiceInfo(typeof(IMathXmlRpcService));

            Assert.IsNotEmpty((ICollection)serviceInfo.Methods);

            Assert.AreEqual("math:add", serviceInfo.Methods[0].Name);
            Assert.AreEqual("Adds up two values", serviceInfo.Methods[0].Description);
            Assert.AreEqual(typeof(int), serviceInfo.Methods[0].ParameterTypes[0]);
            Assert.AreEqual(typeof(int), serviceInfo.Methods[0].ParameterTypes[1]);
        }
    }
}
