using System.Collections.Specialized;

namespace octalforty.Brushie.Instrumentation.Core
{
    /// <summary>
    /// Interface, which must be implemented by all persister classes, used
    /// to persist instances of classes implementing <see cref="IMessage"/> interface.
    /// </summary>
    public interface IPersister
    {
        /// <summary>
        /// Configures persister with information from <paramref name="properties"/> dictionary.
        /// </summary>
        /// <param name="properties">Properties of the persister.</param>
        void Configure(StringDictionary properties);

        /// <summary>
        /// Persists message <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Message to be persisted.</param>
        void Persist(IMessage message);
    }
}