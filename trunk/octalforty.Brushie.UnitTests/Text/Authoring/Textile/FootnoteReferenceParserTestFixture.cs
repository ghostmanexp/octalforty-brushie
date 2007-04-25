using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    /// <summary>
    /// <see cref="FootnoteReferenceParser"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class FootnoteReferenceParserTestFixture
    {
        [Test()]
        public void Parse()
        {
            DomDocument document = new DomDocument();
            IInlineElementParser inlineElementParser = new FootnoteReferenceParser();

            inlineElementParser.Parse(null, document, 
                "This[1] is a footnote reference, whereas [2] this and\\[3] and[this] are not.");

            Assert.AreEqual(1, document.ChildElements.Count);

            FootnoteReference footnoteReference = document.ChildElements[0] as FootnoteReference;

            Assert.IsNotNull(footnoteReference);
            Assert.AreEqual(1, footnoteReference.Number);
        }
    }
}
