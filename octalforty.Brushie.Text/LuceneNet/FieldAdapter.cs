using System;
using System.IO;

using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace octalforty.Brushie.Text.LuceneNet
{
    /// <summary>
    /// <see cref="Field"/> adapter.
    /// </summary>
    public class FieldAdapter
    {
        #region Private Member Variables
        private Field field;
        #endregion
        
        #region Public Properties
        /// <summary>
        /// Gets a reference to the Lucene.Net field.
        /// </summary>
        public Field Field
        {
            get { return field; }
        }

        /// <summary>
        /// Gets or sets fields' boost value.
        /// </summary>
        public float Boost
        {
            get { return field.GetBoost(); }
            set { field.SetBoost(value); }
        }
        
        /// <summary>
        /// Gets a value which indicates whether value of the field is to be indexed, 
        /// so that it may be searched on.
        /// </summary>
        public bool IsIndexed
        {
            get { return field.IsIndexed(); }
        }
        
        /// <summary>
        /// Gets a value which indicates whether the value of the field is to be stored in the 
        /// index for return with search hits. <para/>
        /// It is an error for this to be true if a field is reader-valued.
        /// </summary>
        public bool IsStored
        {
            get { return field.IsStored();  }
        }
        
        /// <summary>
        /// Gets a value which indicates whether the term or terms used to index this field are stored 
        /// as a term vector, available from <see cref="IndexReader.GetTermFreqVector"/>.
        /// </summary>
        public bool IsTermVectorStored
        {
            get { return field.IsTermVectorStored();  }
        }
        
        /// <summary>
        /// Gets a value which indicates whether the value of the field should be tokenized as 
        /// text prior to indexing.
        /// </summary>
        /// <remarks>
        /// Untokenized fields are indexed as a single word and may not be reader-valued.  
        /// </remarks>
        public bool IsTokenized
        {
            get { return field.IsTokenized(); }
        }
        
        /// <summary>
        /// Gets a string with the name of the field.
        /// </summary>
        public string Name
        {
            get { return field.Name(); }
        }
        
        /// <summary>
        /// Gets an instance of <see cref="TextReader"/> class with the value of 
        /// the field as a reader, or <see langword="null"/> if not applicable.
        /// </summary>
        public TextReader ReaderValue
        {
            get { return field.ReaderValue(); }
        }
        
        /// <summary>
        /// Gets a string with the value of the field, or <see langword="null"/> if not applicable.
        /// </summary>
        public string StringValue
        {
            get { return field.StringValue(); }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="FieldAdapter"/> class.
        /// </summary>
        public FieldAdapter()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="FieldAdapter"/> class with 
        /// a given Lucene.Net field.
        /// </summary>
        /// <param name="field"></param>
        public FieldAdapter(Field field)
        {
            this.field = field;
        }
        
        /// <summary>
        /// Creates an instance of a date-valued field that is not tokenized and is indexed, 
        /// and stored in the index, for return with hits.  
        /// </summary>
        /// <param name="name">Name of the field.</param>
        /// <param name="value">Value of the field.</param>
        /// <returns></returns>
        public static FieldAdapter CreateKeywordField(string name, DateTime value)
        {
            return new FieldAdapter(new Field(name, DateTools.DateToString(value, DateTools.Resolution.MINUTE),
                Field.Store.YES, Field.Index.UN_TOKENIZED));
        }
        
        /// <summary>
        /// Creates an instance of a string-valued field that is not tokenized, but is indexed 
        /// and stored. 
        /// </summary> 
        /// <param name="name">Field name.</param>
        /// <param name="value">Field value.</param>
        /// <remarks>
        /// Useful for non-text fields, e.g. url. 
        /// </remarks>
        /// <returns></returns>
        public static FieldAdapter CreateKeywordField(string name, string value)
        {
            return new FieldAdapter(new Field(name, value, Field.Store.YES, Field.Index.UN_TOKENIZED));
        }
        
        /// <summary>
        /// Creates an instance of a reader-valued field that is tokenized and indexed, but is not 
        /// stored in the index verbatim.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="value">Field value.</param>
        /// <param name="storeTermVector">
        /// Flag which indicates whether to store data in the term vector.
        /// </param>
        /// <remarks>
        /// Useful for longer text fields, like "body". 
        /// </remarks>
        /// <returns></returns>
        public static FieldAdapter CreateTextField(string name, TextReader value, bool storeTermVector)
        {
            return new FieldAdapter(new Field(name, value));
        }

        /// <summary>
        /// Creates an instance of a reader-valued field that is tokenized and indexed, but is not 
        /// stored in the index verbatim without the term vector.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="value">Field value.</param>
        /// <remarks>
        /// Useful for longer text fields, like "body". 
        /// </remarks>
        /// <returns></returns>
        public static FieldAdapter CreateTextField(string name, TextReader value)
        {
            return new FieldAdapter(Field.Text(name, value));
        }
        
        /// <summary>
        /// Creates an instance of a string-valued field that is tokenized and indexed, and is 
        /// stored in the index, for return with hits.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="value">Field value.</param>
        /// <param name="storeTermVector">
        /// Flag which indicates whether to store data in the term vector.
        /// </param>
        /// <remarks>
        /// Useful for short text fields, like "title" or "subject".
        /// </remarks>
        /// <returns></returns>
        public static FieldAdapter CreateTextField(string name, string value, bool storeTermVector)
        {
            return new FieldAdapter(Field.Text(name, value, storeTermVector));
        }

        /// <summary>
        /// Creates an instance of a string-valued field that is tokenized and indexed, and is 
        /// stored in the index, for return with hits.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="value">Field value.</param>
        /// <remarks>
        /// Useful for short text fields, like "title" or "subject".
        /// </remarks>
        /// <returns></returns>
        public static FieldAdapter CreateTextField(string name, string value)
        {
            return new FieldAdapter(Field.Text(name, value));
        }
        
        /// <summary>
        /// Creates a new instance of a string-valued field that is not tokenized nor indexed, 
        /// but is stored in the index, for return with hits.  
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="value">Field value.</param>
        /// <returns></returns>
        public static FieldAdapter CreateUnindexedField(string name, string value)
        {
            return new FieldAdapter(Field.UnIndexed(name, value));
        }

        /// <summary>
        /// Creates a new instance of a string-valued field that is tokenized and indexed, 
        /// but that is not stored in the index. 
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="value">Field value.</param>
        /// <param name="storeTermVector">
        /// Flag which indicates whether to store data in the term vector.
        /// </param>
        /// <returns></returns>
        public static FieldAdapter CreateUnstoredField(string name, string value, bool storeTermVector)
        {
            return new FieldAdapter(Field.UnStored(name, value, storeTermVector));
        }

        /// <summary>
        /// Creates a new instance of a string-valued field that is tokenized and indexed, 
        /// but that is not stored in the index. 
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="value">Field value.</param>
        /// <returns></returns>
        public static FieldAdapter CreateUnstoredField(string name, string value)
        {
            return new FieldAdapter(Field.UnStored(name, value));
        }

        #region Object Members
        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return field.ToString();
        }
        #endregion
    }
}