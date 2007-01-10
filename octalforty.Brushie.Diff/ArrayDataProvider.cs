namespace octalforty.Brushie.Diff
{
    /// <summary>
    /// Represents a data provider which wraps standard array for consumption by
    /// the <see cref="DiffEngine{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ArrayDataProvider<T> : IDataProvider<T>
    {
        #region Private Member Variables
        private T[] wrappedArray;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ArrayDataProvider{T}"/>
        /// with a reference to the given array to wrap.
        /// </summary>
        /// <param name="wrappedArray"></param>
        public ArrayDataProvider(T[] wrappedArray)
        {
            this.wrappedArray = wrappedArray;
        }

        #region IDataProvider<T> Members
        /// <summary>
        /// Gets a value with the given <paramref name="index"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return wrappedArray[index]; }
        }

        /// <summary>
        /// Gets the number of items this <see cref="IDataProvider{T}"/> can supply.
        /// </summary>
        public int Count
        {
            get { return wrappedArray.GetLength(0); }
        }
        #endregion
    }
}
