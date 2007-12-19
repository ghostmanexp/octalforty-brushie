using System;

using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc
{
    /// <summary>
    /// <see cref="XmlRpcServiceProxyGenerator"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class XmlRpcServiceProxyGeneratorTestFixture
    {
        [Test()]
        public void CreateProxy()
        {
            XmlRpcServiceProxyGenerator xmlRpcServiceProxyGenerator = 
                new XmlRpcServiceProxyGenerator();
            IMathXmlRpcService mathXmlRpcService = 
                xmlRpcServiceProxyGenerator.CreateProxy<IMathXmlRpcService>();
            
            Assert.IsTrue(mathXmlRpcService is IXmlRpcServiceProxy);
            Assert.IsNull(((IXmlRpcServiceProxy)mathXmlRpcService).ServiceEndpointUri);

            ((IXmlRpcServiceProxy)mathXmlRpcService).ServiceEndpointUri = new Uri("http://www.math.com/math");

            int result = mathXmlRpcService.Add(1, 2);
        }
    }
}
