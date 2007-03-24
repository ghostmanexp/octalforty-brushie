using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;

namespace octalforty.Brushie.UnitTests.Text.Authoring
{
    /// <summary>
    /// <see cref="HtmlTagEnclosure"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class HtmlTagEnclosureTestFixture
    {
        [Test()]
        public void StringConstructor()
        {
            HtmlTagEnclosure enclosure = new HtmlTagEnclosure("em");

            Assert.AreEqual("<em>", enclosure.StartBlock);
            Assert.AreEqual("</em>", enclosure.EndBlock);
        }
    }
}
