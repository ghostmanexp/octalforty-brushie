using System.IO;
using System.Text;

using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc;

using Rhino.Mocks;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc
{
    /// <summary>
    /// <see cref="XmlRpcServiceDispatcher"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class XmlRpcServiceDispatcherTestFixture
    {
        [Test()]
        public void Dispatch()
        {
            MockRepository mockRepository = new MockRepository();

            IMathXmlRpcService mathXmlRpcService = mockRepository.CreateMock<IMathXmlRpcService>();
            
            Expect.Call(mathXmlRpcService.Add(1, 2)).Return(1 + 2);
            mockRepository.ReplayAll();

            IXmlRpcServiceDispatcher xmlRpcServiceDispatcher = new XmlRpcServiceDispatcher(mathXmlRpcService.GetType());
            
            using(MemoryStream memoryStream = new MemoryStream())
            {
                xmlRpcServiceDispatcher.Dispatch(new XmlRpcServiceContext(mathXmlRpcService, 
                    ToStream(
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                    "<methodCall><methodName>math:add</methodName>" +
                        "<params>"+
                            "<param><value><i4>1</i4></value></param>" +
                            "<param><value><i4>2</i4></value></param>" +
                        "</params>" +
                    "</methodCall>"), memoryStream));

                string response = Encoding.UTF8.GetString(memoryStream.ToArray());
                Assert.AreEqual(Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble()) +
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                    "<methodResponse>" +
                    "<params>" +
                        "<param>" +
                            "<value>" +
                                "<i4>3</i4>" +
                            "</value>" +
                        "</param>" +
                    "</params>" +
                    "</methodResponse>", response);
            } // using
        }

        private static Stream ToStream(string serializedString)
        {
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(serializedString));
            return memoryStream;
        }
    }
}
