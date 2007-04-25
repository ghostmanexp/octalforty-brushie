using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    /// <summary>
    /// <see cref="ParagraphParser"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class ParagraphParserTestFixture
    {
        [Test()]
        public void Parse()
        {
            DomDocument document = new DomDocument();
            IBlockElementParser blockElementParser = new ParagraphParser();

            blockElementParser.Parse(null, document, 
                "p. Paragraph one\r\n\r\nImplicit paragraph\r\n\r\nNot a paragraph");

            Assert.AreEqual(2, document.ChildElements.Count);
        }
    }
}
