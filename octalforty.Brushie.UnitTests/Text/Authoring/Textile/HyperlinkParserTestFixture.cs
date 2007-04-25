using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    /// <summary>
    /// <see cref="HyperlinkParser"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class HyperlinkParserTestFixture
    {
        [Test()]
        public void Parse()
        {
            DomDocument document = new DomDocument();
            IInlineElementParser inlineElementParser = new HyperlinkParser();

            inlineElementParser.Parse(null, document, 
                "\"This is a hyperlink(and text)\":url , whereas \"this\": is not ");

            Assert.AreEqual(1, document.ChildElements.Count);

            Hyperlink hyperlink = document.ChildElements[0] as Hyperlink;

            Assert.IsNotNull(hyperlink);
            Assert.AreEqual("This is a hyperlink", hyperlink.InnerText);
            Assert.AreEqual("and text", hyperlink.Title);
            Assert.AreEqual("url", hyperlink.Url);
        }
    }
}
