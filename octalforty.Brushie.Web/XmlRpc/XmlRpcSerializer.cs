using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

using octalforty.Brushie.Web.XmlRpc.Conversion;

namespace octalforty.Brushie.Web.XmlRpc
{
    /// <summary>
    /// Serializes and deserializes XML-RPC structures.
    /// </summary>
    public sealed class XmlRpcSerializer
    {
        #region Private Member Variables
        private Encoding encoding = System.Text.Encoding.UTF8;
        private SerializationContext serializationContext = new SerializationContext();
        private DeserializationContext deserializationContext = new DeserializationContext();
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the encoding used when serializing or deserializing structures.
        /// </summary>
        /// <remarks>
        /// The default is UTF-8.
        /// </remarks>
        public Encoding Encoding
        {
            get { return encoding; }
            set { encoding = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlRpcSerializer"/> class.
        /// </summary>
        public XmlRpcSerializer()
        {
        }

        /// <summary>
        /// Serializes <paramref name="request"/> into a <paramref name="stream"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="stream"></param>
        public void SerializeRequest(XmlRpcRequest request, Stream stream)
        {
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding);
            
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("methodCall");

            xmlTextWriter.WriteElementString(String.Empty, "methodName", 
                String.Empty, request.MethodName);

            SerializeParameters(request.Parameters, xmlTextWriter);

            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();

            xmlTextWriter.Flush();
        }

        /// <summary>
        /// Deserializes <see cref="XmlRpcRequest"/> from the <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="parameterTypes"></param>
        /// <returns></returns>
        public XmlRpcRequest DeserializeRequest(Stream stream, params Type[] parameterTypes)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(stream);
            
            //
            // Method name
            string methodName = xmlDocument.SelectSingleNode("methodCall/methodName").InnerText;

            //
            // Deserializing parameters
            List<object> parameters = DeserializeParameters(parameterTypes, xmlDocument);
            return new XmlRpcRequest(methodName, parameters.ToArray());
        }

        /// <summary>
        /// Deserializes <see cref="XmlRpcRequest"/> from the <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="parameterTypesProvider"></param>
        /// <returns></returns>
        public XmlRpcRequest DeserializeRequest(Stream stream, IXmlRpcSerializerParameterTypesProvider parameterTypesProvider)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(stream);

            //
            // Method name
            string methodName = xmlDocument.SelectSingleNode("methodCall/methodName").InnerText;

            Type[] parameterTypes = parameterTypesProvider.GetRequestParameterTypes(methodName);
            List<object> parameters = DeserializeParameters(parameterTypes, xmlDocument);

            return new XmlRpcRequest(methodName, parameters.ToArray());
        }

        /// <summary>
        /// Deserializes parameter from <paramref name="parameterXmlNode"/>.
        /// </summary>
        /// <param name="parameterXmlNode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private object DeserializeParameter(XmlNode parameterXmlNode, Type type)
        {
            return DeserializeObject(parameterXmlNode.FirstChild, type);
        }

        /// <summary>
        /// Deserializes object of type <paramref name="type"/> from <paramref name="objectXmlNode"/>.
        /// </summary>
        /// <param name="objectXmlNode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object DeserializeObject(XmlNode objectXmlNode, Type type)
        {
            return deserializationContext.Deserialize(objectXmlNode, type);
        }

        /// <summary>
        /// Serializes <paramref name="parameters"/> into <paramref name="xmlTextWriter"/>.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="xmlTextWriter"></param>
        private void SerializeParameters(object[] parameters, XmlTextWriter xmlTextWriter)
        {
            if(parameters == null || parameters.GetLength(0) == 0)
                return;

            xmlTextWriter.WriteStartElement("params");

            foreach(object parameter in parameters)
                SerializeParameter(parameter, xmlTextWriter);

            xmlTextWriter.WriteEndElement();
        }

        /// <summary>
        /// Serializes <paramref name="value"/> into <paramref name="xmlTextWriter"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="xmlTextWriter"></param>
        private void SerializeParameter(object value, XmlTextWriter xmlTextWriter)
        {
            xmlTextWriter.WriteStartElement("param");
            SerializeObject(value, xmlTextWriter);
            xmlTextWriter.WriteEndElement();
        }

        /// <summary>
        /// Serializes <paramref name="value"/> into <paramref name="xmlTextWriter"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="xmlTextWriter"></param>
        private void SerializeObject(object value, XmlTextWriter xmlTextWriter)
        {
            serializationContext.Serialize(value, xmlTextWriter);
        }

        /// <summary>
        /// Deserializes parameters from <paramref name="xmlDocument"/>.
        /// </summary>
        /// <param name="parameterTypes"></param>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        private List<object> DeserializeParameters(Type[] parameterTypes, XmlDocument xmlDocument)
        {
            //
            // Deserializing parameters
            List<object> parameters = new List<object>();
            int parameterIndex = 0;

            foreach(XmlNode parameterXmlNode in xmlDocument.SelectNodes("methodCall/params/param"))
            {
                parameters.Add(DeserializeParameter(parameterXmlNode,
                    parameterTypes[parameterIndex++]));
            } // foreach
            return parameters;
        }

        /// <summary>
        /// Serializes <paramref name="response"/> into <paramref name="stream"/>.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="stream"></param>
        public void SerializeResponse(XmlRpcSuccessResponse response, Stream stream)
        {
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding);

            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("methodResponse");
            
            SerializeParameters(new object[] { response.ReturnValue }, xmlTextWriter);

            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();

            xmlTextWriter.Flush();
        }
    }
}
