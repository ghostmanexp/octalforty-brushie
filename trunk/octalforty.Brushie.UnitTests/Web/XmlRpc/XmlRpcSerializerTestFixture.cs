﻿using System;
using System.IO;
using System.Text;

using NUnit.Framework;

using octalforty.Brushie.Web.XmlRpc;

namespace octalforty.Brushie.UnitTests.Web.XmlRpc
{
    /// <summary>
    /// <see cref="XmlRpcSerializer"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class XmlRpcSerializerTestFixture
    {
        [Test()]
        public void DefaultEncodingIsUtf8()
        {
            XmlRpcSerializer xmlRpcSerializer = new XmlRpcSerializer();
            Assert.AreEqual(Encoding.UTF8, xmlRpcSerializer.Encoding);
        }

        [Test()]
        public void SerializeRequest()
        {
            XmlRpcSerializer xmlRpcSerializer = new XmlRpcSerializer();
            AssertSerializedRequest(xmlRpcSerializer, 
                new XmlRpcRequest("namespace/sampleMethod", null),
                xmlRpcSerializer.Encoding.GetString(xmlRpcSerializer.Encoding.GetPreamble()) + 
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<methodCall><methodName>namespace/sampleMethod</methodName></methodCall>");

            DateTime dateTime = new DateTime(2007, 1, 2, 15, 20, 54);

            AssertSerializedRequest(xmlRpcSerializer,
                new XmlRpcRequest("namespace/sampleMethod", 1, 2.5, "three", true, dateTime),
                xmlRpcSerializer.Encoding.GetString(xmlRpcSerializer.Encoding.GetPreamble()) +
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<methodCall><methodName>namespace/sampleMethod</methodName>" +
                "<params>"+
                "<param><i4>1</i4></param>" +
                "<param><double>2.5</double></param>" +
                "<param>three</param>" +
                "<param><boolean>true</boolean></param>" +
                "<param><dateTime.iso8601>20070102T15:20:54</dateTime.iso8601></param></params>" +
                "</methodCall>");

            AssertSerializedRequest(xmlRpcSerializer,
                new XmlRpcRequest("namespace/sampleMethod", -1, -52.5, "yep", 
                false, new byte[] { 0, 1, 2, 54, 66 }),
                xmlRpcSerializer.Encoding.GetString(xmlRpcSerializer.Encoding.GetPreamble()) +
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<methodCall><methodName>namespace/sampleMethod</methodName>" +
                "<params>" +
                "<param><i4>-1</i4></param>" +
                "<param><double>-52.5</double></param>" +
                "<param>yep</param>" +
                "<param><boolean>false</boolean></param>" +
                "<param><base64>AAECNkI=</base64></param></params>" +
                "</methodCall>");

            AssertSerializedRequest(xmlRpcSerializer,
                new XmlRpcRequest("namespace/sampleMethod", -1, -52.5, "yep",
                false, new byte[] { 0, 1, 2, 54, 66 },
                new object[] { 12, "Egypt", false },
                new Range(12, "hi!")),
                xmlRpcSerializer.Encoding.GetString(xmlRpcSerializer.Encoding.GetPreamble()) +
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<methodCall><methodName>namespace/sampleMethod</methodName>" +
                "<params>" +
                "<param><i4>-1</i4></param>" +
                "<param><double>-52.5</double></param>" +
                "<param>yep</param>" +
                "<param><boolean>false</boolean></param>" +
                "<param><base64>AAECNkI=</base64></param>" +
                "<param><array><data>" +
                "<value><i4>12</i4></value>" +
                "<value>Egypt</value>" +
                "<value><boolean>false</boolean></value>" + 
                "</data></array></param>" +
                "<param><struct>" +
                "<member><name>LowerBound</name><value><i4>12</i4></value></member>" +
                "<member><name>UpperBound</name><value>hi!</value></member>" +
                "</struct></param>" +
                "</params>" +
                "</methodCall>");

            AssertSerializedRequest(xmlRpcSerializer, 
                new XmlRpcRequest("namespace/sampleMethod",
                new DoubleRange(new Range(4, "from"), new Range(54, "to"))), 
                xmlRpcSerializer.Encoding.GetString(xmlRpcSerializer.Encoding.GetPreamble()) + 
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<methodCall><methodName>namespace/sampleMethod</methodName>" +
                "<params>" +
                    "<param>" +
                        "<struct>" +
                            "<member>" +
                                "<name>From</name>" +
                                "<value>" +
                                    "<struct>" +
                                        "<member>" +
                                            "<name>LowerBound</name><value><i4>4</i4></value>" +
                                        "</member>" +
                                        "<member>" +
                                            "<name>UpperBound</name><value>from</value>" +
                                        "</member>" +
                                    "</struct>" +
                                "</value>" +
                            "</member>" +
                            "<member>" +
                                "<name>To</name>" +
                                "<value>" +
                                    "<struct>" +
                                        "<member>" +
                                            "<name>LowerBound</name><value><i4>54</i4></value>" +
                                        "</member>" +
                                        "<member>" +
                                            "<name>UpperBound</name><value>to</value>" +
                                        "</member>" +
                                    "</struct>" +
                                "</value>" +
                            "</member>" +
                        "</struct>" +
                    "</param>" +
                "</params>" +
                "</methodCall>");
        }

        [Test()]
        public void DeserializeRequest()
        {
            XmlRpcSerializer xmlRpcSerializer = new XmlRpcSerializer();
            XmlRpcRequest xmlRpcRequest = DeserializeRequest(xmlRpcSerializer,
                xmlRpcSerializer.Encoding.GetString(xmlRpcSerializer.Encoding.GetPreamble()) +
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<methodCall><methodName>namespace/sampleMethod</methodName></methodCall>");

            Assert.AreEqual("namespace/sampleMethod", xmlRpcRequest.MethodName);
            Assert.IsEmpty(xmlRpcRequest.Parameters);

            xmlRpcRequest = DeserializeRequest(xmlRpcSerializer,
                xmlRpcSerializer.Encoding.GetString(xmlRpcSerializer.Encoding.GetPreamble()) +
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<methodCall><methodName>namespace/sampleMethod</methodName>" +
                "<params>" +
                "<param><i4>1</i4></param>" +
                "<param><double>2.5</double></param>" +
                "<param>three</param>" +
                "<param><boolean>true</boolean></param>" +
                "<param><dateTime.iso8601>20070102T15:20:54</dateTime.iso8601></param></params>" +
                "</methodCall>",
                typeof(int), typeof(double), typeof(string), typeof(bool), typeof(DateTime));

            Assert.AreEqual("namespace/sampleMethod", xmlRpcRequest.MethodName);
            Assert.AreEqual(5, xmlRpcRequest.Parameters.GetLength(0));

            Assert.AreEqual(1, xmlRpcRequest.Parameters[0]);
            Assert.AreEqual(2.5, xmlRpcRequest.Parameters[1]);
            Assert.AreEqual("three", xmlRpcRequest.Parameters[2]);
            Assert.AreEqual(true, xmlRpcRequest.Parameters[3]);
            Assert.AreEqual(new DateTime(2007, 1, 2, 15, 20, 54), 
                xmlRpcRequest.Parameters[4]);

            xmlRpcRequest = DeserializeRequest(xmlRpcSerializer,
                xmlRpcSerializer.Encoding.GetString(xmlRpcSerializer.Encoding.GetPreamble()) +
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<methodCall><methodName>namespace/sampleMethod</methodName>" +
                "<params>" +
                    "<param>" +
                        "<struct>" +
                            "<member>" +
                                "<name>From</name>" +
                                "<value>" +
                                    "<struct>" +
                                        "<member>" +
                                            "<name>LowerBound</name><value><i4>4</i4></value>" +
                                        "</member>" +
                                        "<member>" +
                                            "<name>UpperBound</name><value>from</value>" +
                                        "</member>" +
                                    "</struct>" +
                                "</value>" +
                            "</member>" +
                            "<member>" +
                                "<name>To</name>" +
                                "<value>" +
                                    "<struct>" +
                                        "<member>" +
                                            "<name>LowerBound</name><value><i4>54</i4></value>" +
                                        "</member>" +
                                        "<member>" +
                                            "<name>UpperBound</name><value>to</value>" +
                                        "</member>" +
                                    "</struct>" +
                                "</value>" +
                            "</member>" +
                        "</struct>" +
                    "</param>" +
                "</params>" +
                "</methodCall>",
                typeof(DoubleRange));

            Assert.AreEqual("namespace/sampleMethod", xmlRpcRequest.MethodName);
            Assert.AreEqual(1, xmlRpcRequest.Parameters.GetLength(0));
        }

        private static XmlRpcRequest DeserializeRequest(XmlRpcSerializer xmlRpcSerializer,
            string serializedRequest, params Type[] parameterTypes)
        {
            using(MemoryStream memoryStream = 
                new MemoryStream(xmlRpcSerializer.Encoding.GetBytes(serializedRequest)))
            {
                return xmlRpcSerializer.DeserializeRequest(memoryStream, parameterTypes);
            } // using
        }

        private static void AssertSerializedRequest(XmlRpcSerializer xmlRpcSerializer, 
            XmlRpcRequest request, string serializedRequest)
        {
            using(MemoryStream memoryStream = new MemoryStream())
            {
                xmlRpcSerializer.SerializeRequest(request, memoryStream);

                string actualSerializedRequest = 
                    xmlRpcSerializer.Encoding.GetString(memoryStream.ToArray());

                Assert.AreEqual(serializedRequest, actualSerializedRequest);
            } // using
        }
    }
}
