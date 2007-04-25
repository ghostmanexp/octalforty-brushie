using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    /// <summary>
    /// <see cref="BlockquoteParser"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class BlockquoteParserTestFixture
    {
        [Test()]
        public void Parse()
        {
            DomDocument document = new DomDocument();
            IBlockElementParser blockElementParser = new BlockquoteParser();

            blockElementParser.Parse(null, document, 
                "bq(class#id){style}=(())). Blockquote\r\n\r\nbq.One more\r\n\r\nbq(class2). And one more\r\n\r\n");

            Assert.AreEqual(2, document.ChildElements.Count);

            Blockquote blockquote = document.ChildElements[0] as Blockquote;

            Assert.IsNotNull(blockquote);
            Assert.AreEqual("class", blockquote.Attributes.CssClass);
            Assert.AreEqual("id", blockquote.Attributes.ID);
            Assert.AreEqual("style", blockquote.Attributes.Style);
            Assert.AreEqual(BlockElementAlignment.Center, blockquote.Attributes.Alignment);
            Assert.AreEqual(2, blockquote.Attributes.LeftIndent);
            Assert.AreEqual(3, blockquote.Attributes.RightIndent);

            blockquote = document.ChildElements[1] as Blockquote;

            Assert.IsNotNull(blockquote);
            Assert.AreEqual("class2", blockquote.Attributes.CssClass);
        }
    }
}
