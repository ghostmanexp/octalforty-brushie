namespace octalforty.Brushie.Instrumentation.Core
{
    /// <summary>
    /// Interface which must be implemented by binders, which route messages
    /// to various persisters.
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// Gets or sets a reference to the next <see cref="IBinder"/> in the binding chain.
        /// </summary>
        IBinder NextBinder
        { get; set; }

        /// <summary>
        /// Determines whether the <paramref name="message"/> can be bound to the
        /// persister in question or not.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool CanBind(IMessage message);
    }
}