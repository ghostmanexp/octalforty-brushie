using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    [TestFixture()]
    public class NoTextileParserTestFixture
    {
        [Test()]
        public void Parse()
        {
            DomDocument document = new DomDocument();
            IBlockElementParser blockElementParser = new NoTextileParser();

            blockElementParser.Parse(null, document,
                "==This text== is left intact, whereas \\==this and == this - are just fine\\==, ==again \\== that== is notextile too.");

            Assert.AreEqual(2, document.ChildElements.Count);
            
            TextBlock textBlock = document.ChildElements[0] as TextBlock;

            Assert.IsNotNull(textBlock);
            Assert.AreEqual("This text", textBlock.InnerText);

            textBlock = document.ChildElements[1] as TextBlock;

            Assert.IsNotNull(textBlock);
            Assert.AreEqual(" this - are just fine\\==, ", textBlock.InnerText);
        }
    }
}
