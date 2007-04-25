using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    /// <summary>
    /// <see cref="ImageParser"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class ImageParserTestFixture
    {
        [Test()]
        public void Parse()
        {
            DomDocument document = new DomDocument();
            IBlockElementParser blockElementParser = new ImageParser();

            blockElementParser.Parse(null, document, 
                "!Image Url!, \\!Not an url\\!, !(class#id)One more url (with optional text)!");

            Assert.AreEqual(2, document.ChildElements.Count);

            Image image = document.ChildElements[0] as Image;

            Assert.IsNotNull(image);
            Assert.AreEqual("Image Url", image.Url);

            image = document.ChildElements[1] as Image;

            Assert.IsNotNull(image);
            Assert.AreEqual("class", image.Attributes.CssClass);
            Assert.AreEqual("id", image.Attributes.ID);
            Assert.AreEqual("One more url ", image.Url);
            Assert.AreEqual("with optional text", image.AlternateText);
        }
    }
}
