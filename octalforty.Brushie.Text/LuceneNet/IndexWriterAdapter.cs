using System;

using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace octalforty.Brushie.Text.LuceneNet
{
    /// <summary>
    /// <see cref="Lucene.Net.Index.IndexWriter"/> adapter.
    /// </summary>
    public sealed class IndexWriterAdapter : IDisposable
    {
        #region Private Member Variables
        private IndexWriter indexWriter;
        private bool isDisposed = false;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a reference to the analyzer instance, used by the index writer.
        /// </summary>
        public Analyzer Analyzer
        {
            get
            {
                CheckIsDisposed();
                return indexWriter.GetAnalyzer();
            }
        }
        
        /// <summary>
        /// Gets or sets a reference to the similarity.
        /// </summary>
        public Similarity Similarity
        {
            get
            {
                CheckIsDisposed();
                return indexWriter.GetSimilarity();
            }
            set
            {
                CheckIsDisposed();
                indexWriter.SetSimilarity(value);
            }
        }
        
        /// <summary>
        /// Gets or sets a boolean value which indicates whether to use compound file or not.
        /// </summary>
        public bool UseCompoundFile
        {
            get
            {
                CheckIsDisposed();
                return indexWriter.GetUseCompoundFile();
            }
            set
            {
                CheckIsDisposed();
                indexWriter.SetUseCompoundFile(value);
            }
        }
        #endregion
        
        /// <summary>
        /// Initializes a new instance of <see cref="IndexWriterAdapter"/> class 
        /// from a given path and a supplied analyzer.
        /// </summary>
        /// <param name="path">Location on a disk where index will be stored.</param>
        /// <param name="analyzer">Analyzer.</param>
        /// <param name="createIndex">
        /// Indicated whether to create index or use existing.
        /// </param>
        public IndexWriterAdapter(string path, Analyzer analyzer, bool createIndex)
        {
            indexWriter = new IndexWriter(path, analyzer, createIndex);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="IndexWriterAdapter"/> class 
        /// from a given directory and a supplied analyzer.
        /// </summary>
        /// <param name="directory">Directory, where an index will be stored.</param>
        /// <param name="analyzer">Analyzer.</param>
        /// <param name="createIndex">
        /// Indicated whether to create index or use existing.
        /// </param>
        public IndexWriterAdapter(Directory directory, Analyzer analyzer, bool createIndex)
        {
            indexWriter = new IndexWriter(directory, analyzer, createIndex);
        }
        
        /// <summary>
        /// Adds <paramref name="document"/> with a given <paramref name="analyzer"/>.
        /// </summary>
        /// <param name="document">Document to be added.</param>
        /// <param name="analyzer"></param>
        public void AddDocument(Document document, Analyzer analyzer)
        {
            CheckIsDisposed();
            indexWriter.AddDocument(document, analyzer);
        }

        /// <summary>
        /// Adds <paramref name="document"/> to the index.
        /// </summary>
        /// <param name="document">Document to be added.</param>
        public void AddDocument(Document document)
        {
            CheckIsDisposed();
            indexWriter.AddDocument(document);
        }
        
        /// <summary>
        /// Optimizes index.
        /// </summary>
        public void Optimize()
        {
            CheckIsDisposed();
            indexWriter.Optimize();
        }

        #region IDisposable Members
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting 
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        
        /// <summary>
        /// <see cref="Dispose(bool)"/> executes in two distinct scenarios. If 
        /// <paramref name="disposing"/> equals <see langword="true" />, the method has been 
        /// called directly or indirectly by a user's code. Managed and unmanaged resources 
        /// can be disposed.<para />
        /// If <paramref name="disposing"/> equals <see langword="false" />, the method has 
        /// been called by the runtime from inside the finalizer and you should not reference 
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if(!isDisposed)
            {
                if(disposing)
                {
                    indexWriter.Close();
                } // if
                
                isDisposed = true;
            } // if
        }

        /// <summary>
        /// Checks whether the object was disposed and throws <see cref="ObjectDisposedException"/> if
        /// it was disposed of.
        /// </summary>
        private void CheckIsDisposed()
        {
            if(isDisposed)
                throw new ObjectDisposedException("IndexWriterAdapter");
        }
    }
}