using System;
using System.IO;

using Lucene.Net.Documents;

using NUnit.Framework;

using octalforty.Brushie.Text.LuceneNet;

namespace octalforty.Brushie.UnitTests.Text.LuceneNet
{
    /// <summary>
    /// <see cref="FieldAdapter"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class FieldAdapterTestFixture
    {
        [Test()]
        public void Boost()
        {
            Field field = Field.Keyword("Name", "Value");
            FieldAdapter fieldAdapter = new FieldAdapter(field);
            
            Assert.AreEqual(field.GetBoost(), fieldAdapter.Boost);

            fieldAdapter.Boost = 0.7f;
            Assert.AreEqual(0.7f, fieldAdapter.Boost);
            Assert.AreEqual(field.GetBoost(), fieldAdapter.Boost);
        }
        
        [Test()]
        public void CreateKeywordField()
        {
            FieldAdapter keyword = FieldAdapter.CreateKeywordField("Name", "Value");
            
            Assert.AreEqual("Name", keyword.Name);
            Assert.AreEqual("Value", keyword.StringValue);
            
            Assert.IsFalse(keyword.IsTokenized);
            Assert.IsTrue(keyword.IsIndexed);
            Assert.IsTrue(keyword.IsStored);
        }

        [Test()]
        public void CreateDateTimeKeywordField()
        {
            FieldAdapter keyword = FieldAdapter.CreateKeywordField("Name", DateTime.Now);

            Assert.AreEqual("Name", keyword.Name);

            Assert.IsFalse(keyword.IsTokenized);
            Assert.IsTrue(keyword.IsIndexed);
            Assert.IsTrue(keyword.IsStored);
        }
        
        [Test()]
        public void CreateReaderTextField()
        {
            TextReader textReader = new StringReader(string.Empty);

            FieldAdapter text = FieldAdapter.CreateTextField("Name", textReader);
            
            Assert.AreEqual("Name", text.Name);
            Assert.AreSame(textReader, text.ReaderValue);
            
            Assert.IsTrue(text.IsTokenized);
            Assert.IsTrue(text.IsIndexed);
            Assert.IsFalse(text.IsStored);
        }

        [Test()]
        public void CreateTextField()
        {
            FieldAdapter text = FieldAdapter.CreateTextField("Name", "Value");

            Assert.AreEqual("Name", text.Name);
            Assert.AreEqual("Value", text.StringValue);

            Assert.IsTrue(text.IsTokenized);
            Assert.IsTrue(text.IsIndexed);
            Assert.IsTrue(text.IsStored);
        }
        
        [Test()]
        public void CreateUnindexedField()
        {
            FieldAdapter unindexed = FieldAdapter.CreateUnindexedField("Name", "Value");

            Assert.AreEqual("Name", unindexed.Name);
            Assert.AreEqual("Value", unindexed.StringValue);

            Assert.IsFalse(unindexed.IsTokenized);
            Assert.IsFalse(unindexed.IsIndexed);
            Assert.IsTrue(unindexed.IsStored);
        }
        
        [Test()]
        public void CreateUnstoredField()
        {
            FieldAdapter unstored = FieldAdapter.CreateUnstoredField("Name", "Value");

            Assert.AreEqual("Name", unstored.Name);
            Assert.AreEqual("Value", unstored.StringValue);

            Assert.IsTrue(unstored.IsTokenized);
            Assert.IsTrue(unstored.IsIndexed);
            Assert.IsFalse(unstored.IsStored);
        }
    }
}