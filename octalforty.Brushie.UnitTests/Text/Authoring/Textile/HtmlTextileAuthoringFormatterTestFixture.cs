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
                authoringFormatter.FormatHeading(1, " Heading ",
                    new BlockElementAttributes(string.Empty, string.Empty, string.Empty, string.Empty,
                        BlockElementAlignment.Unknown, 0, 0)));

            Assert.AreEqual("<h1 class=\"heading\" style=\"font-weight: bold;text-align: center;padding-left: 2em;\">Heading</h1>",
                authoringFormatter.FormatHeading(1, " Heading ",
                    new BlockElementAttributes("heading", string.Empty, "font-weight: bold", string.Empty,
                        BlockElementAlignment.Center, 2, 0)));

            Assert.AreEqual("<h1 id=\"id\" lang=\"en-US\" style=\"font-weight: bold;text-align: center;padding-left: 2em;padding-right: 3em;\">Heading</h1>",
                authoringFormatter.FormatHeading(1, " Heading ",
                    new BlockElementAttributes(null, "id", "font-weight: bold;", "en-US",
                        BlockElementAlignment.Center, 2, 3)));
        }

        [Test()]
        public void FormatBlockquote()
        {
            ITextileAuthoringFormatter authoringFormatter =
                new HtmlTextileAuthoringFormatter();

            Assert.AreEqual("<blockquote>Blockquote</blockquote>",
                authoringFormatter.FormatBlockquote(" Blockquote ",
                    new BlockElementAttributes(string.Empty, string.Empty, string.Empty, string.Empty,
                        BlockElementAlignment.Unknown, 0, 0)));

            Assert.AreEqual("<blockquote class=\"blockquote\" style=\"font-weight: bold;text-align: center;padding-left: 2em;\">Blockquote</blockquote>",
                authoringFormatter.FormatBlockquote(" Blockquote ",
                    new BlockElementAttributes("blockquote", string.Empty, "font-weight: bold", string.Empty,
                        BlockElementAlignment.Center, 2, 0)));

            Assert.AreEqual("<blockquote id=\"id\" lang=\"en-US\" style=\"font-weight: bold;text-align: center;padding-left: 2em;padding-right: 3em;\">Blockquote</blockquote>",
                authoringFormatter.FormatBlockquote(" Blockquote ",
                    new BlockElementAttributes(null, "id", "font-weight: bold;", "en-US",
                        BlockElementAlignment.Center, 2, 3)));
        }

        [Test()]
        public void FormatHyperlink()
        {
            ITextileAuthoringFormatter authoringFormatter =
                new HtmlTextileAuthoringFormatter();

            Assert.AreEqual("<a title=\"Title\" href=\"http://www.google.com\">Link</a>",
                authoringFormatter.FormatHyperlink(" Link ", " Title ", " http://www.google.com ",
                    new PhraseElementAttributes(string.Empty, string.Empty, string.Empty, string.Empty)));

            Assert.AreEqual("<a class=\"a\" style=\"font-weight: bold;\" title=\"Title\" href=\"http://www.google.com\">Link</a>",
                authoringFormatter.FormatHyperlink(" Link ", " Title ", " http://www.google.com ",
                    new PhraseElementAttributes("a", string.Empty, "font-weight: bold", string.Empty)));

            Assert.AreEqual("<a id=\"id\" lang=\"en-US\" style=\"font-weight: bold;\" title=\"Title\" href=\"http://www.google.com\">Link</a>",
                authoringFormatter.FormatHyperlink(" Link ", " Title ", " http://www.google.com ",
                    new PhraseElementAttributes(null, "id", "font-weight: bold;", "en-US")));
        }

        [Test()]
        public void FormatTextFormattingBold()
        {
            ITextileAuthoringFormatter authoringFormatter =
                new HtmlTextileAuthoringFormatter();

            Assert.AreEqual("<b>Link</b>",
                authoringFormatter.FormatTextFormatting(TextFormatting.Bold, " Link ",
                    new PhraseElementAttributes(string.Empty, string.Empty, string.Empty, string.Empty)));

            Assert.AreEqual("<b class=\"a\" style=\"font-weight: bold;\">Link</b>",
                authoringFormatter.FormatTextFormatting(TextFormatting.Bold, " Link ",
                    new PhraseElementAttributes("a", string.Empty, "font-weight: bold", string.Empty)));

            Assert.AreEqual("<b id=\"id\" lang=\"en-US\" style=\"font-weight: bold;\">Link</b>",
                authoringFormatter.FormatTextFormatting(TextFormatting.Bold, " Link ",
                    new PhraseElementAttributes(null, "id", "font-weight: bold;", "en-US")));
        }

        [Test()]
        public void FormatTextFormattingStrongEmphasis()
        {
            ITextileAuthoringFormatter authoringFormatter =
                new HtmlTextileAuthoringFormatter();

            Assert.AreEqual("<strong>Link</strong>",
                authoringFormatter.FormatTextFormatting(TextFormatting.StrongEmphasis, " Link ",
                    new PhraseElementAttributes(string.Empty, string.Empty, string.Empty, string.Empty)));

            Assert.AreEqual("<strong class=\"a\" style=\"font-weight: bold;\">Link</strong>",
                authoringFormatter.FormatTextFormatting(TextFormatting.StrongEmphasis, " Link ",
                    new PhraseElementAttributes("a", string.Empty, "font-weight: bold", string.Empty)));

            Assert.AreEqual("<strong id=\"id\" lang=\"en-US\" style=\"font-weight: bold;\">Link</strong>",
                authoringFormatter.FormatTextFormatting(TextFormatting.StrongEmphasis, " Link ",
                    new PhraseElementAttributes(null, "id", "font-weight: bold;", "en-US")));
        }

        [Test()]
        public void FormatTextFormattingItalics()
        {
            ITextileAuthoringFormatter authoringFormatter =
                new HtmlTextileAuthoringFormatter();

            Assert.AreEqual("<i>Link</i>",
                authoringFormatter.FormatTextFormatting(TextFormatting.Italics, " Link ",
                    new PhraseElementAttributes(string.Empty, string.Empty, string.Empty, string.Empty)));

            Assert.AreEqual("<i class=\"a\" style=\"font-weight: bold;\">Link</i>",
                authoringFormatter.FormatTextFormatting(TextFormatting.Italics, " Link ",
                    new PhraseElementAttributes("a", string.Empty, "font-weight: bold", string.Empty)));

            Assert.AreEqual("<i id=\"id\" lang=\"en-US\" style=\"font-weight: bold;\">Link</i>",
                authoringFormatter.FormatTextFormatting(TextFormatting.Italics, " Link ",
                    new PhraseElementAttributes(null, "id", "font-weight: bold;", "en-US")));
        }

        [Test()]
        public void FormatTextFormattingEmphasis()
        {
            ITextileAuthoringFormatter authoringFormatter =
                new HtmlTextileAuthoringFormatter();

            Assert.AreEqual("<em>Link</em>",
                authoringFormatter.FormatTextFormatting(TextFormatting.Emphasis, " Link ",
                    new PhraseElementAttributes(string.Empty, string.Empty, string.Empty, string.Empty)));

            Assert.AreEqual("<em class=\"a\" style=\"font-weight: bold;\">Link</em>",
                authoringFormatter.FormatTextFormatting(TextFormatting.Emphasis, " Link ",
                    new PhraseElementAttributes("a", string.Empty, "font-weight: bold", string.Empty)));

            Assert.AreEqual("<em id=\"id\" lang=\"en-US\" style=\"font-weight: bold;\">Link</em>",
                authoringFormatter.FormatTextFormatting(TextFormatting.Emphasis, " Link ",
                    new PhraseElementAttributes(null, "id", "font-weight: bold;", "en-US")));
        }
    }
}
