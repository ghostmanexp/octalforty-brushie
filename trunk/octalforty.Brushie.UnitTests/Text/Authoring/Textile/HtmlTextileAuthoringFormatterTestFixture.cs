using NUnit.Framework;

using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    /// <summary>
    /// <see cref="HtmlTextileAuthoringFormatter"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class HtmlTextileAuthoringFormatterTestFixture
    {
        [Test()]
        public void FormatHeading()
        {
            ITextileAuthoringFormatter authoringFormatter =
                new HtmlTextileAuthoringFormatter();

            Assert.AreEqual("<h1>Heading</h1>",
                authoringFormatter.FormatHeading(1, "Heading",
                    new BlockElementAttributes(string.Empty, string.Empty, string.Empty, string.Empty,
                        BlockElementAlignment.Unknown, 0, 0)));

            Assert.AreEqual("<h1 class=\"heading\" style=\"font-weight: bold;text-align: center;padding-left: 2em;\">Heading</h1>",
                authoringFormatter.FormatHeading(1, "Heading",
                    new BlockElementAttributes("heading", string.Empty, "font-weight: bold", string.Empty,
                        BlockElementAlignment.Center, 2, 0)));

            Assert.AreEqual("<h1 id=\"id\" lang=\"en-US\" style=\"font-weight: bold;text-align: center;padding-left: 2em;padding-right: 3em;\">Heading</h1>",
                authoringFormatter.FormatHeading(1, "Heading",
                    new BlockElementAttributes(null, "id", "font-weight: bold;", "en-US",
                        BlockElementAlignment.Center, 2, 3)));
        }
    }
}
