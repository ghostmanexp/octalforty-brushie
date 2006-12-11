using System;
using System.IO;

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;

using NUnit.Framework;

using octalforty.Brushie.Text.LuceneNet;

namespace octalforty.Brushie.UnitTests.Text.LuceneNet
{
    /// <summary>
    /// <see cref="IndexWriterAdapter"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class IndexWriterAdapterTestFixture
    {
        [Test()]
        public void StringAnalyzerBoolConstructor()
        {
            string tempEnvironmentVariable = Environment.GetEnvironmentVariable("Temp");
            
            using(IndexWriterAdapter indexWriterAdapter = 
                new IndexWriterAdapter(Path.Combine(tempEnvironmentVariable, "Lucene"),
                    new StandardAnalyzer(), true))
            {
                DirectoryInfo directory = new DirectoryInfo(tempEnvironmentVariable);
                FileInfo[] files = directory.GetFiles("*.txt");
                
                foreach(FileInfo file in files)
                {
                    using(TextReader textReader = new StreamReader(file.FullName))
                    {
                        Document document = new Document();
                        document.Add(Field.Text("text", textReader));
                        
                        indexWriterAdapter.AddDocument(new DocumentAdapter(document));
                    } // using
                } // foreach
                
                indexWriterAdapter.Optimize();
            } // using
        }
        
        [Test()]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ObjectDisposed()
        {
            string tempEnvironmentVariable = Environment.GetEnvironmentVariable("Temp");
            IndexWriterAdapter indexWriterAdapter;
            using(indexWriterAdapter = new IndexWriterAdapter(
                Path.Combine(tempEnvironmentVariable, "Lucene"), new StandardAnalyzer(), true))
            {
            } // using

            Analyzer analyzer = indexWriterAdapter.Analyzer;
        }
    }
}
