using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    /// <summary>
    /// <see cref="FormattedTextBlockParser"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class TextBlockParserTestFixture
    {
        [Test()]
        public void Parse()
        {
            DomDocument document = new DomDocument();
            IInlineElementParser inlineElementParser = new FormattedTextBlockParser();
            inlineElementParser.NextElementParser = new UnformattedTextBlockParser();

            inlineElementParser.Parse(null, document, 
                "This *text* is ??cited??, whereas _that ^one~^ is_ %spanned%.");

            Assert.AreEqual(9, document.ChildElements.Count);

            TextBlock textBlock = document.ChildElements[0] as TextBlock;

            Assert.IsNotNull(textBlock);
            Assert.AreEqual("This ", textBlock.InnerText);

            textBlock = document.ChildElements[1] as TextBlock;

            Assert.IsNotNull(textBlock);
            Assert.AreEqual(TextBlockFormatting.StrongEmphasis, textBlock.Formatting);

            textBlock = document.ChildElements[3] as TextBlock;

            Assert.IsNotNull(textBlock);
            Assert.AreEqual(TextBlockFormatting.Citation, textBlock.Formatting);

            textBlock = document.ChildElements[5] as TextBlock;

            Assert.IsNotNull(textBlock);
            Assert.AreEqual(TextBlockFormatting.Emphasis, textBlock.Formatting);

            textBlock = document.ChildElements[5].ChildElements[0] as TextBlock;

            Assert.IsNotNull(textBlock);
            Assert.AreEqual("that ", textBlock.InnerText);

            textBlock = document.ChildElements[5].ChildElements[1] as TextBlock;

            Assert.IsNotNull(textBlock);
            Assert.AreEqual(TextBlockFormatting.Superscript, textBlock.Formatting);

            textBlock = document.ChildElements[7] as TextBlock;

            Assert.IsNotNull(textBlock);
            Assert.AreEqual(TextBlockFormatting.Span, textBlock.Formatting);
        }
    }
}
