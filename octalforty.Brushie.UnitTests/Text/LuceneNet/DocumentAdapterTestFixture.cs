using Lucene.Net.Documents;

using NUnit.Framework;

using octalforty.Brushie.Text.LuceneNet;

namespace octalforty.Brushie.UnitTests.Text.LuceneNet
{
    /// <summary>
    /// <see cref="DocumentAdapter"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class DocumentAdapterTestFixture
    {
        [Test()]
        public void Boost()
        {
            Document document = new Document();
            DocumentAdapter documentAdapter = new DocumentAdapter(document);
            
            Assert.AreEqual(document.GetBoost(), documentAdapter.Boost);

            documentAdapter.Boost = 0.7f;

            Assert.AreEqual(document.GetBoost(), documentAdapter.Boost);
        }
        
        [Test()]
        public void Indexer()
        {
            Document document = new Document();
            DocumentAdapter documentAdapter = new DocumentAdapter(document);
            
            Field field = Field.Text("Name", "Value");
            document.Add(field);
            
            Assert.AreEqual("Value", documentAdapter["Name"]);
        }
    }
}