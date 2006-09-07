using System;

using NUnit.Framework;

using octalforty.Brushie.Xml;

namespace octalforty.Brushie.UnitTests.Xml
{
    /// <summary>
    /// <see cref="XmlBuilder"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class XmlBuilderTestFixture
    {
        [Test()]
        public void NoArgumentConstructor()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            Assert.IsEmpty(xmlBuilder.ToString());
        }
        
        [Test()]
        public void AppendXmlDeclaration()
        {
            AssertXmlDeclaration("1.1", "windows-1251", "1.1", "windows-1251");
        }
        
        [Test()]
        public void AppendXmlDeclarationNullVersion()
        {
            AssertXmlDeclaration(null, "windows-1251", XmlBuilder.DefaultVersion, "windows-1251");
        }
        
        [Test()]
        public void AppendXmlDeclarationEmptyVersion()
        {
            AssertXmlDeclaration(string.Empty, "windows-1251", XmlBuilder.DefaultVersion, "windows-1251");
        }
        
        [Test()]
        public void AppendXmlDeclarationNullEncoding()
        {
            AssertXmlDeclaration("1.1", null, "1.1", XmlBuilder.DefaultEncoding);
        }
        
        [Test()]
        public void AppendXmlDeclarationEmptyEncoding()
        {
            AssertXmlDeclaration("1.1", string.Empty, "1.1", XmlBuilder.DefaultEncoding);
        }

        private static void AssertXmlDeclaration(string version, string encoding,
            string requiredVersion, string requiredEncoding)
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendXmlDeclaration(version, encoding);
            
            Assert.AreEqual(
                string.Format("<?xml version=\"{0}\" encoding=\"{1}\"?>",
                requiredVersion, requiredEncoding), 
                xmlBuilder.ToString());
        }

        [Test()]
        public void AppendProcessingInstruction()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendProcessingInstruction("xml", "version=\"1.0\"");
            
            Assert.AreEqual("<?xml version=\"1.0\"?>", xmlBuilder.ToString());
        }
        
        [Test()]
        public void AppendProcessingInstructionNullContent()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendProcessingInstruction("xml", null);
            
            Assert.AreEqual("<?xml?>", xmlBuilder.ToString());
        }

        [Test()]
        public void AppendProcessingInstructionEmptyContent()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendProcessingInstruction("xml", string.Empty);

            Assert.AreEqual("<?xml?>", xmlBuilder.ToString());
        }
        
        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AppendProcessingInstructionNullTarget()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendProcessingInstruction(null, string.Empty);
        }
        
        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void AppendProcessingInstructionEmptyTarget()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendProcessingInstruction(string.Empty, string.Empty);
        }
        
        [Test()]
        public void AppendComment()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendComment("Comment");
            
            Assert.AreEqual("<!-- Comment -->", xmlBuilder.ToString());
        }

        [Test()]
        public void AppendCommentNullText()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendComment(null);

            Assert.AreEqual("<!--  -->", xmlBuilder.ToString());
        }

        [Test()]
        public void AppendCommentEmptyText()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendComment(null);

            Assert.AreEqual("<!--  -->", xmlBuilder.ToString());
        }

        [Test()]
        public void AddAttribute()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AddAttribute("1", "2");
            xmlBuilder.AddAttribute("1", "1", "2");
            xmlBuilder.AddAttribute(null, "1", "2");
            
            xmlBuilder.AddAttribute("1", null);
            xmlBuilder.AddAttribute("1", "1", null);
            xmlBuilder.AddAttribute(null, "1", null);

            xmlBuilder.AddAttribute("1", string.Empty);
            xmlBuilder.AddAttribute("1", "1", string.Empty);
            xmlBuilder.AddAttribute(null, "1", string.Empty);
        }
        
        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddAttributeNullName()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AddAttribute(null, string.Empty);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddAttributeNullName2()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AddAttribute(null, null, string.Empty);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddAttributeEmptyName()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AddAttribute(string.Empty, string.Empty);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddAttributeEmptyName2()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AddAttribute(null, string.Empty, string.Empty);
        }
        
        [Test()]
        public void AppendStartTag()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendStartTag("tag");
            
            Assert.AreEqual("<tag>", xmlBuilder.ToString());
        }
        
        [Test()]
        public void AppendStartTag2()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendStartTag("ns", "tag");
            
            Assert.AreEqual("<ns:tag>", xmlBuilder.ToString());
        }
        
        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AppendStartTagNullNullName()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendStartTag(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AppendStartTagNullNullName2()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendStartTag(null, null);
        }
        
        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void AppendStartTagEmptyName()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendStartTag(string.Empty);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void AppendStartTagEmptyName2()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendStartTag(null, string.Empty);
        }

        [Test()]
        public void AppendStartTagWithAttributes()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            
            xmlBuilder.AddAttribute("a", "1");
            xmlBuilder.AddAttribute("b", "2");
            xmlBuilder.AddAttribute("ns", "c", "3");
            xmlBuilder.AppendStartTag("tag");

            Assert.AreEqual("<tag ns:c=\"3\" b=\"2\" a=\"1\">", xmlBuilder.ToString());

            xmlBuilder.AppendStartTag("tag");

            Assert.AreEqual("<tag ns:c=\"3\" b=\"2\" a=\"1\"><tag>", xmlBuilder.ToString());
        }

        [Test()]
        public void AppendStartTagWithAttributes2()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();

            xmlBuilder.AddAttribute("a", "1");
            xmlBuilder.AddAttribute("b", "2");
            xmlBuilder.AddAttribute("ns", "c", "3");
            xmlBuilder.AppendStartTag("ns", "tag");

            Assert.AreEqual("<ns:tag ns:c=\"3\" b=\"2\" a=\"1\">", xmlBuilder.ToString());

            xmlBuilder.AppendStartTag("ns", "tag");

            Assert.AreEqual("<ns:tag ns:c=\"3\" b=\"2\" a=\"1\"><ns:tag>", xmlBuilder.ToString());
        }
        
        [Test()]
        public void AppendEndTag()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            
            xmlBuilder.AppendStartTag("tag");
            xmlBuilder.AppendEndTag();
            
            Assert.AreEqual("<tag></tag>", xmlBuilder.ToString());
        }

        [Test()]
        public void AppendEndTag2()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();

            xmlBuilder.AppendStartTag("ns", "tag");
            xmlBuilder.AppendEndTag();

            Assert.AreEqual("<ns:tag></ns:tag>", xmlBuilder.ToString());
        }
        
        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AppendEndTagWithoutStartTag()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendEndTag();
        }
        
        [Test()]
        public void AppendElement()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendElement("tag", "inner text");
            
            Assert.AreEqual("<tag>inner text</tag>", xmlBuilder.ToString());
        }

        [Test()]
        public void AppendElementNullText()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendElement("tag", null);

            Assert.AreEqual("<tag />", xmlBuilder.ToString());
        }

        [Test()]
        public void AppendElementEmptyText()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendElement("tag", string.Empty);

            Assert.AreEqual("<tag></tag>", xmlBuilder.ToString());
        }
        
        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AppendElementNullName()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendElement(null, string.Empty);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void AppendElementEmptyName()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            xmlBuilder.AppendElement(string.Empty, string.Empty);
        }
        
        [Test()]
        public void ComplexXmlDocument()
        {
            XmlBuilder xmlBuilder = new XmlBuilder();
            
            xmlBuilder.AppendXmlDeclaration(null, null);
            
            xmlBuilder.AddAttribute("version", "2.01");
            xmlBuilder.AppendStartTag("rss");
            xmlBuilder.AppendStartTag("channel");
            xmlBuilder.AppendElement("name", "Rss 2.01 Channel");
            xmlBuilder.AppendEndTag();
            xmlBuilder.AppendEndTag();
            
            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"utf-8\"?><rss version=\"2.01\"><channel><name>Rss 2.01 Channel</name></channel></rss>",
                xmlBuilder.ToString());
        }
    }
}
