using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    /// <summary>
    /// <see cref="AcronymParser"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class AcronymParserTestFixture
    {
        [Test()]
        public void Parse()
        {
            DomDocument document = new DomDocument();
            IInlineElementParser inlineElementParser = new AcronymParser();

            inlineElementParser.Parse(null, document, 
                "HTML(Hypertext Markup Language), КГБ(Комитет Государственной безопасности) XML - eXtensible(yep) markup language");

            Assert.AreEqual(2, document.ChildElements.Count);

            Acronym acronym = document.ChildElements[0] as Acronym;
            
            Assert.IsNotNull(acronym);
            Assert.AreEqual("HTML", acronym.Title);
            Assert.AreEqual("Hypertext Markup Language", acronym.InnerText);

            acronym = document.ChildElements[1] as Acronym;

            Assert.IsNotNull(acronym);
            Assert.AreEqual("КГБ", acronym.Title);
            Assert.AreEqual("Комитет Государственной безопасности", acronym.InnerText);
        }
    }
}
