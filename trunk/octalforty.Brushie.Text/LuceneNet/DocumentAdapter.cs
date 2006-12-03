using System.Collections.Generic;
using System.Collections.ObjectModel;

using Lucene.Net.Documents;

namespace octalforty.Brushie.Text.LuceneNet
{
    /// <summary>
    /// <see cref="Document"/> adapter.
    /// </summary>
    public class DocumentAdapter
    {
        #region Private Member Variables
        private Document document;
        #endregion
        
        #region Public Properties
        /// <summary>
        /// Gets a reference to a Lucene.Net document.
        /// </summary>
        public Document Document
        {
            get { return document; }
        }

        /// <summary>
        /// Gets or sets documents' boost value.
        /// </summary>
        public float Boost
        {
            get { return document.GetBoost(); }
            set { document.SetBoost(value); }
        }
        
        /// <summary>
        /// Returns the string value of the field with the given name <paramref name="fieldName"/>,
        /// if any exist in this document, or <see langword="null"/>.  
        /// If multiple fields exist with this name, this method returns the first value added.  
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public string this[string fieldName]
        {
            get { return document.Get(fieldName); }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="DocumentAdapter"/> class with
        /// a given Lucene.Net <paramref name="document"/>.
        /// </summary>
        /// <param name="document">Document.</param>
        public DocumentAdapter(Document document)
        {
            this.document = document;
        }
        
        /// <summary>
        /// Adds a field <paramref name="field"/> to a document.
        /// </summary>
        /// <param name="field"></param>
        /// <remarks>
        /// Several fields may be added with the same name. In this case, if the fields are indexed, 
        /// their text is treated as though appended for the purposes of search. <para/>
        /// Note that <see cref="AddField"/> like the <see cref="RemoveField"/> and 
        /// <see cref="RemoveFields"/> methods only makes sense prior 
        /// to adding a document to an index. These methods cannot be used to change the content 
        /// of an existing index. 
        /// In order to achieve this, a document has to be deleted from an index and a new changed 
        /// version of that document has to be added.  
        /// </remarks>
        public void AddField(FieldAdapter field)
        {
            document.Add(field.Field);
        }
        
        /// <summary>
        /// Removes field with the specified name <paramref name="name"/> from the document.
        /// </summary>
        /// <param name="name"></param>
        /// <remarks>
        /// If multiple fields exist with this name, this method removes the first field that has 
        /// been added. If there is no field with the specified name, the document remains unchanged.<para />
        /// Note that the <see cref="RemoveField"/> and <see cref="RemoveFields"/> methods 
        /// like the <see cref="AddField"/> method only make sense prior to adding a document to an index. 
        /// These methods cannot be used to change the content of an existing index. In order to achieve 
        /// this, a document has to be deleted from an index and a new changed version of that document 
        /// has to be added.  
        /// </remarks>
        public void RemoveField(string name)
        {
            document.RemoveField(name);
        }
        
        /// <summary>
        /// Removes all fields with the given name <paramref name="name"/> from the document.
        /// </summary>
        /// <param name="name"></param>
        /// <remarks>
        /// If there is no field with the specified name, the document remains unchanged.<para />
        /// Note that the <see cref="RemoveField"/> and <see cref="RemoveFields"/> methods 
        /// like the <see cref="AddField"/> method only make sense prior to adding a document to an index. 
        /// These methods cannot be used to change the content of an existing index. In order to achieve 
        /// this, a document has to be deleted from an index and a new changed version of that document 
        /// </remarks>
        public void RemoveFields(string name)
        {
            document.RemoveFields(name);
        }
        
        /// <summary>
        /// Returns a field with the given name <paramref name="name"/> if any exist in this document, 
        /// or <see langword="null"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FieldAdapter GetField(string name)
        {
            Field field = document.GetField(name);
            return field == null ? null : new FieldAdapter(field);
        }
        
        /// <summary>
        /// Returns a read-only collection of fields with the given name <paramref name="name"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FieldAdapterReadOnlyCollection GetFields(string name)
        {
            Field[] fields = document.GetFields(name);
            List<FieldAdapter> list = new List<FieldAdapter>();
            
            if(fields != null)
                foreach(Field field in fields)
                    list.Add(new FieldAdapter(field));
            
            return new FieldAdapterReadOnlyCollection(list);
        }
        
        /// <summary>
        /// Gets a read-only collection of values of the field with the given name <paramref name="name"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ReadOnlyCollection<string> GetValues(string name)
        {
            string[] values = document.GetValues(name);
            List<string> list = new List<string>();
            
            if(values != null)
                foreach(string value in values)
                    list.Add(value);
            
            return new ReadOnlyCollection<string>(list);
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
            return document.ToString();
        }
        #endregion
    }
}