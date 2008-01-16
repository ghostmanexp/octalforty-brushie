using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Provides introspection capabilities to XML-RPC services.
    /// </summary>
    public class XmlRpcServiceIntrospector
    {
        #region Private Member Variables
        private XmlRpcServiceInfo xmlRpcServiceInfo;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcServiceIntrospector"/> class.
        /// </summary>
        /// <param name="serviceType"></param>
        public XmlRpcServiceIntrospector(Type serviceType)
        {
            xmlRpcServiceInfo = XmlRpcServiceInfo.CreateXmlRpcServiceInfo(serviceType);
        }

        public void Introspect(Stream outputStream)
        {
            XmlDocument xmlRpcServiceDefinition = BuildXmlRpcServiceDefinition(xmlRpcServiceInfo);
            XslCompiledTransform xslCompiledTransform = GetXmlRpcServiceDefinitionTransformation();

            xslCompiledTransform.Transform(xmlRpcServiceDefinition, new XmlTextWriter(outputStream, Encoding.UTF8));
        }

        private static XslCompiledTransform GetXmlRpcServiceDefinitionTransformation()
        {
            using(Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                "octalforty.Brushie.Web.XmlRpc.Resources.Introspection.xslt"))
            {
                using(XmlReader xmlReader = new XmlTextReader(stream))
                {
                    XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
                    xslCompiledTransform.Load(xmlReader);

                    return xslCompiledTransform;
                } // using
            } // using
        }

        private static XmlDocument BuildXmlRpcServiceDefinition(XmlRpcServiceInfo xmlRpcServiceInfo)
        {
            using(MemoryStream memoryStream = new MemoryStream())
            {
                using(XmlWriter xmlWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
                {
                    xmlWriter.WriteStartDocument();

                    xmlWriter.WriteStartElement("xmlrpc-service");
                    xmlWriter.WriteAttributeString("name", xmlRpcServiceInfo.Name);
                    xmlWriter.WriteAttributeString("description", xmlRpcServiceInfo.Description);

                    xmlWriter.WriteStartElement("methods");

                    foreach(XmlRpcServiceMethodInfo methodInfo in xmlRpcServiceInfo.Methods)
                    {
                        xmlWriter.WriteStartElement("method");
                        xmlWriter.WriteAttributeString("name", methodInfo.Name);
                        xmlWriter.WriteAttributeString("description", methodInfo.Description);

                        xmlWriter.WriteEndElement();
                    } // foreach

                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();

                    xmlWriter.Flush();

                    memoryStream.Position = 0;

                    XmlDocument xmlRpcServiceDefinitionDocument = new XmlDocument();
                    xmlRpcServiceDefinitionDocument.Load(memoryStream);

                    return xmlRpcServiceDefinitionDocument;
                } // using
            } // using
        }
    }
}
