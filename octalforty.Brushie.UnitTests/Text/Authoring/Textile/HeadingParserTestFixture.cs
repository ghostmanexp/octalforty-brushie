using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    /// <summary>
    /// <see cref="HeadingParser"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class HeadingParserTestFixture
    {
        [Test()]
        public void Parse()
        {
            DomDocument document = new DomDocument();
            IBlockElementParser blockElementParser = new HeadingParser();

            blockElementParser.Parse(null, document,
                "h1(class#id){style}=(())). Heading 1\r\n\r\nh2.One more\r\n\r\nh3(class2). And one more\r\n\r\n");

            Assert.AreEqual(2, document.ChildElements.Count);

            Heading heading = document.ChildElements[0] as Heading;

            Assert.IsNotNull(heading);
            Assert.AreEqual(1, heading.Level);
            Assert.AreEqual("class", heading.Attributes.CssClass);
            Assert.AreEqual("id", heading.Attributes.ID);
            Assert.AreEqual("style", heading.Attributes.Style);
            Assert.AreEqual(BlockElementAlignment.Center, heading.Attributes.Alignment);
            Assert.AreEqual(2, heading.Attributes.LeftIndent);
            Assert.AreEqual(3, heading.Attributes.RightIndent);

            heading = document.ChildElements[1] as Heading;

            Assert.IsNotNull(heading);
            Assert.AreEqual(3, heading.Level);
            Assert.AreEqual("class2", heading.Attributes.CssClass);
        }
    }
}
