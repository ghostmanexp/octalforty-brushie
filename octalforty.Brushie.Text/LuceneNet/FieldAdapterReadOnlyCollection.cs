using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace octalforty.Brushie.Text.LuceneNet
{
    /// <summary>
    /// A read-only collection of <see cref="FieldAdapter"/>.
    /// </summary>
    public class FieldAdapterReadOnlyCollection : ReadOnlyCollection<FieldAdapter>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FieldAdapterReadOnlyCollection"/> class.
        /// </summary>
        /// <param name="list"></param>
        public FieldAdapterReadOnlyCollection(IList<FieldAdapter> list) : 
            base(list)
        {
        }
    }
}