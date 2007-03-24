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
        }
    }
}
