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

        private static void FormatTextFormatting(TextFormatting textFormatting, string tag)
        {
            ITextileAuthoringFormatter authoringFormatter = new
                HtmlTextileAuthoringFormatter();

            Assert.AreEqual(string.Format("<{0}>Inner Text</{0}>", tag),
                authoringFormatter.FormatTextFormatting(textFormatting, " Inner Text ",
                    new PhraseElementAttributes(string.Empty, string.Empty, string.Empty, string.Empty)));

            Assert.AreEqual(string.Format("<{0} class=\"a\" style=\"font-weight: bold;\">Inner Text</{0}>", tag),
                authoringFormatter.FormatTextFormatting(textFormatting, " Inner Text ",
                    new PhraseElementAttributes("a", string.Empty, "font-weight: bold", string.Empty)));

            Assert.AreEqual(string.Format("<{0} id=\"id\" lang=\"en-US\" style=\"font-weight: bold;\">Inner Text</{0}>", tag),
                authoringFormatter.FormatTextFormatting(textFormatting, " Inner Text ",
                    new PhraseElementAttributes(null, "id", "font-weight: bold;", "en-US")));
        }

        [Test()]
        public void FormatTextFormattingBold()
        {
            FormatTextFormatting(TextFormatting.Bold, "b");
        }

        [Test()]
        public void FormatTextFormattingStrongEmphasis()
        {
            FormatTextFormatting(TextFormatting.StrongEmphasis, "strong");
        }

        [Test()]
        public void FormatTextFormattingItalics()
        {
            FormatTextFormatting(TextFormatting.Italics, "i");
        }

        [Test()]
        public void FormatTextFormattingEmphasis()
        {
            FormatTextFormatting(TextFormatting.Emphasis, "em");
        }

        [Test()]
        public void FormatTextFormattingCitation()
        {
            FormatTextFormatting(TextFormatting.Citation, "cite");
        }

        [Test()]
        public void FormatTextFormattingInserted()
        {
            FormatTextFormatting(TextFormatting.Inserted, "ins");
        }

        [Test()]
        public void FormatTextFormattingDeleted()
        {
            FormatTextFormatting(TextFormatting.Deleted, "del");
        }

        [Test()]
        public void FormatTextFormattingSuperscript()
        {
            FormatTextFormatting(TextFormatting.Superscript, "sup");
        }

        [Test()]
        public void FormatTextFormattingSubscript()
        {
            FormatTextFormatting(TextFormatting.Subscript, "sub");
        }

        [Test()]
        public void FormatTextFormattingSpan()
        {
            FormatTextFormatting(TextFormatting.Span, "span");
        }
    }
}
