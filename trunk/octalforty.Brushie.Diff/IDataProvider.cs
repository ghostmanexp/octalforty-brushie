namespace octalforty.Brushie.Diff
{
    /// <summary>
    /// Defines the contract used by <see cref="DiffEngine{T}"/> to retrieve data
    /// to be compared.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataProvider<T>
    {
        /// <summary>
        /// Gets a value with the given <paramref name="index"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        T this[int index]
        { get; }

        /// <summary>
        /// Gets the number of items this <see cref="IDataProvider{T}"/> can supply.
        /// </summary>
        int Count
        { get; }
    }
}
