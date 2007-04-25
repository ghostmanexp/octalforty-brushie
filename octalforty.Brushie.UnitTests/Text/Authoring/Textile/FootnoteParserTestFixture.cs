using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    /// <summary>
    /// <see cref="FootnoteParser"/> unit tests;
    /// </summary>
    [TestFixture()]
    public class FootnoteParserTestFixture
    {
        [Test()]
        public void Parse()
        {
            DomDocument document = new DomDocument();
            IBlockElementParser blockElementParser = new FootnoteParser();

            blockElementParser.Parse(null, document,
                "fn243. Footnote\r\n\r\nfn323.Not a footnote\r\nfn3444. One more footnote\r\n\r\n");

            Assert.AreEqual(2, document.ChildElements.Count);

            Footnote footnote = document.ChildElements[0] as Footnote;

            Assert.IsNotNull(footnote);
            Assert.AreEqual(243, footnote.Number);

            footnote = document.ChildElements[1] as Footnote;

            Assert.IsNotNull(footnote);
            Assert.AreEqual(3444, footnote.Number);
        }
    }
}
