using NUnit.Framework;

using octalforty.Brushie.Text.Authoring.Textile;
using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    /// <summary>
    /// <see cref="HtmlAuthoringDomElementVisitor"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class HtmlAuthoringDomElementVisitorTestFixture
    {
        [Test()]
        public void VisitHeading()
        {
            HtmlAuthoringDomElementVisitor htmlAuthoringDomElementVisitor = new HtmlAuthoringDomElementVisitor();
            
            htmlAuthoringDomElementVisitor.Visit(new Heading(null, new BlockElementAttributes("heading", "__h1", "color: red",
                "en-US", BlockElementAlignment.Justify, 2, 3), 2, "Level 2 Heading"));
            
            Assert.AreEqual("<h2 class=\"heading\" id=\"__h1\" lang=\"en-US\" style=\"color: red;text-align: justify;padding-left: 2em;padding-right: 3em;\">Level 2 Heading</h2>",
                htmlAuthoringDomElementVisitor.Html.ToString());
        }

        [Test()]
        public void VisitBlockquote()
        {
            HtmlAuthoringDomElementVisitor htmlAuthoringDomElementVisitor = new HtmlAuthoringDomElementVisitor();

            Blockquote blockquote = new Blockquote(null, new BlockElementAttributes("class", "__id", "color: red",
                "en-US", BlockElementAlignment.Justify, 2, 3));
            blockquote.AppendChild(new TextBlock(blockquote, InlineElementAttributes.Empty, "Text", 
                TextBlockModifier.Unknown));
            htmlAuthoringDomElementVisitor.Visit(blockquote);

            Assert.AreEqual("<blockquote class=\"class\" id=\"__id\" lang=\"en-US\" style=\"color: red;text-align: justify;padding-left: 2em;padding-right: 3em;\"><span>Text</span></blockquote>",
                htmlAuthoringDomElementVisitor.Html.ToString());
        }

        [Test()]
        public void VisitParagraph()
        {
            HtmlAuthoringDomElementVisitor htmlAuthoringDomElementVisitor = new HtmlAuthoringDomElementVisitor();

            Paragraph paragraph = new Paragraph(null, new BlockElementAttributes("class", "__id", "color: red",
                "en-US", BlockElementAlignment.Justify, 2, 3));
            paragraph.AppendChild(new TextBlock(paragraph, InlineElementAttributes.Empty, "Text",
                TextBlockModifier.Unknown));
            htmlAuthoringDomElementVisitor.Visit(paragraph);

            Assert.AreEqual("<p class=\"class\" id=\"__id\" lang=\"en-US\" style=\"color: red;text-align: justify;padding-left: 2em;padding-right: 3em;\"><span>Text</span></p>",
                htmlAuthoringDomElementVisitor.Html.ToString());
        }

        [Test()]
        public void VisitHyperlink()
        {
            HtmlAuthoringDomElementVisitor htmlAuthoringDomElementVisitor = new HtmlAuthoringDomElementVisitor();

            Hyperlink hyperlink = new Hyperlink(null, "Hyperlink", "title", "url");

            htmlAuthoringDomElementVisitor.Visit(hyperlink);

            Assert.AreEqual("<a title=\"title\" href=\"url\">Hyperlink</a>", htmlAuthoringDomElementVisitor.Html.ToString());
        }
    }
}
