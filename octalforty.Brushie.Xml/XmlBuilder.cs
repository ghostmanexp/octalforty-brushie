using System;
using System.Collections.Generic;
using System.Text;

using octalforty.Brushie.Xml.Resources;

namespace octalforty.Brushie.Xml
{
    /// <summary>
    /// Assists in creating XML documents. 
    /// </summary>
    public class XmlBuilder
    {
        #region Public Static Constants
        /// <summary>
        /// Represents default encoding used when building XML documents.
        /// </summary>
        public static readonly string DefaultEncoding = "utf-8";

        /// <summary>
        /// Represents default XML version used when building XML documents.
        /// </summary>
        public static readonly string DefaultVersion = "1.0";
        #endregion

        #region Private Member Variables
        private StringBuilder xmlBuilder = new StringBuilder();
        private Stack<XmlAttribute> attributes = new Stack<XmlAttribute>();
        private Stack<string> tagNames = new Stack<string>();
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="XmlBuilder"/> class.
        /// </summary>
        public XmlBuilder()
        {
        }
        
        /// <summary>
        /// Appends "xml" declaration with XML version information and encoding.
        /// <para />
        /// When <paramref name="encoding"/> is a null reference or an empty string,
        /// <see cref="DefaultEncoding"/> is used. <para />
        /// When <paramref name="version"/> is a null reference or an empty string,
        /// <see cref="DefaultVersion"/> is used.
        /// </summary>
        /// <param name="version">XML version.</param>
        /// <param name="encoding">Encoding.</param>
        public void AppendXmlDeclaration(string version, string encoding)
        {
            string content = string.Format("version=\"{0}\" encoding=\"{1}\"",
                version == null || version == string.Empty ? DefaultVersion : version,
                encoding == null || encoding == string.Empty ? DefaultEncoding : encoding);
            AppendProcessingInstruction("xml", content);
        }
        
        /// <summary>
        /// Appends processing instruction with target <paramref name="target"/> and
        /// content <paramref name="content"/>.
        /// </summary>
        /// <param name="target">Processing instruction target.</param>
        /// <param name="content">Processing instruction content.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="target"/> is a null reference.
        /// </exception> 
        /// <exception cref="ArgumentException">
        /// When <paramref name="target"/> is an empty string.
        /// </exception>
        public void AppendProcessingInstruction(string target, string content)
        {
            if(target == null)
                throw new ArgumentNullException("target",
                    Strings.XmlBuilder_AppendProcessingInstruction_TargetCannotBeNull);

            if(target == string.Empty)
                throw new ArgumentException(Strings.XmlBuilder_AppendProcessingInstruction_TargetCannotBeEmpty,
                    "target");

            xmlBuilder.AppendFormat("<?{0}", target);

            if(content != null && content != string.Empty)
                xmlBuilder.AppendFormat(" {0}", content);

            xmlBuilder.Append("?>");
        }
        
        /// <summary>
        /// Appends XML comment.
        /// </summary>
        /// <param name="text">Comment text.</param>
        public void AppendComment(string text)
        {
            xmlBuilder.AppendFormat("<!-- {0} -->", text == null ? string.Empty : text);
        }
        
        /// <summary>
        /// Adds attribute to internal attributes stack. Attributes are added to the
        /// next element appended with <see cref="AppendElement"/> or <see cref="AppendStartTag(string)"/>.
        /// </summary>
        /// <param name="name">Attribute name.</param>
        /// <param name="value">Attribute value.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="name"/> is a null reference.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// When <paramref name="name"/> is an empty string.</exception>
        public void AddAttribute(string name, string value)
        {
            AddAttribute(string.Empty, name, value);
        }
        
        /// <summary>
        /// Adds attribute to internal attributes stack. Attributes are added to the
        /// next element appended with <see cref="AppendElement"/> or <see cref="AppendStartTag(string)"/>.
        /// </summary>
        /// <param name="_namespace">Attribute namespace.</param>
        /// <param name="name">Attribute name.</param>
        /// <param name="value">Attribute value.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="name"/> is a null reference.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// When <paramref name="name"/> is an empty string.</exception>
        public void AddAttribute(string _namespace, string name, string value)
        {
            if(name == null)
                throw new ArgumentNullException("name",
                    Strings.XmlBuilder_AddAttribute_NameCannotBeNull);

            if(name == string.Empty)
                throw new ArgumentException(Strings.XmlBuilder_AddAttribute_NameCannotBeEmpty,
                    "name");

            string effectiveName = GetEffectiveName(_namespace, name);
            
            attributes.Push(new XmlAttribute(effectiveName, value == null ? string.Empty : value));
        }

        /// <summary>
        /// Appends start tag with all attributes previously added with 
        /// <see cref="AddAttribute(string,string)"/> or <see cref="AddAttribute(string,string,string)"/>.
        /// </summary>
        /// <param name="name">Tag name.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="name"/> is a null reference.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// When <paramref name="name"/> is an empty string.
        /// </exception>
        public void AppendStartTag(string name)
        {
            AppendStartTag(string.Empty, name);
        }
        
        /// <summary>
        /// Appends start tag with all attributes previously added with 
        /// <see cref="AddAttribute(string,string)"/> or <see cref="AddAttribute(string,string,string)"/>.
        /// </summary>
        /// <param name="name">Tag name.</param>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="name"/> is a null reference.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// When <paramref name="name"/> is an empty string.
        /// </exception>
        public void AppendStartTag(string _namespace, string name)
        {
            if(name == null)
                throw new ArgumentNullException("name",
                    Strings.XmlBuilder_AppendStartTag_NameCannotBeNull);

            if(name == string.Empty)
                throw new ArgumentException(Strings.XmlBuilder_AppendStartTag_NameCannotBeEmpty,
                    "name");
            
            InternalAddStartTag(_namespace, name);
            xmlBuilder.Append(">");
            
            tagNames.Push(GetEffectiveName(_namespace, name));
        }

        /// <summary>
        /// Appends end tag.
        /// </summary>
        public void AppendEndTag()
        {
            if(tagNames.Count == 0)
                throw new InvalidOperationException(
                    Strings.XmlBuilder_AppendEndTag_NoStartTagsWereAppended);

            xmlBuilder.AppendFormat("</{0}>", tagNames.Pop());
        }
        
        /// <summary>
        /// Appends element with all attributes and possibly inner text.
        /// </summary>
        /// <param name="name">Element name.</param>
        /// <param name="innerText">Inner text.</param>
        public void AppendElement(string name, string innerText)
        {
            AppendElement(string.Empty, name, innerText);
        }

        /// <summary>
        /// Appends element with all attributes and possibly inner text.
        /// </summary>
        /// <param name="_namespace">Namespace.</param>
        /// <param name="name">Element name.</param>
        /// <param name="innerText">Inner text.</param>
        public void AppendElement(string _namespace, string name, string innerText)
        {
            if(innerText != null)
            {
                AppendStartTag(_namespace, name);
                AppendText(innerText);
                AppendEndTag();
            } // if
            else
            {
                InternalAddStartTag(_namespace, name);
                xmlBuilder.Append(" />");
            } // if
        }
        
        /// <summary>
        /// Appends simple textual content.
        /// </summary>
        /// <param name="text">Text to be appended.</param>
        public void AppendText(string text)
        {
            xmlBuilder.Append(text);
        }

        /// <summary>
        /// Gets effective name.
        /// </summary>
        /// <param name="_namespace"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetEffectiveName(string _namespace, string name)
        {
            return _namespace == null || _namespace == string.Empty ?
                name : string.Format("{0}:{1}", _namespace, name);
        }

        /// <summary>
        /// Internal method, which adds start portion of an element with all
        /// attributes.
        /// </summary>
        /// <param name="_namespace"></param>
        /// <param name="name"></param>
        private void InternalAddStartTag(string _namespace, string name)
        {
            xmlBuilder.AppendFormat("<{0}", GetEffectiveName(_namespace, name));

            //
            // Adding attributes.
            while(attributes.Count != 0)
            {
                XmlAttribute xmlAttribute = attributes.Pop();
                xmlBuilder.AppendFormat(" {0}=\"{1}\"", xmlAttribute.Name, xmlAttribute.Value);
            } // if
        }

        #region Object Methods
        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return xmlBuilder.ToString();
        }
        #endregion
    }
}
