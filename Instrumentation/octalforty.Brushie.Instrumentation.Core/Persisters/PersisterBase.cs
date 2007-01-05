using System.Collections.Specialized;

namespace octalforty.Brushie.Instrumentation.Core.Persisters
{
    /// <summary>
    /// Abstract base class for persisters.
    /// </summary>
    public abstract class PersisterBase : IPersister
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PersisterBase"/> class.
        /// </summary>
        protected PersisterBase()
        {
        }

        #region IPersister Members
        /// <summary>
        /// Configures persister with information from <paramref name="properties"/> dictionary.
        /// </summary>
        /// <param name="properties">Properties of the persister.</param>
        public virtual void Configure(StringDictionary properties)
        {
        }

        /// <summary>
        /// Persists message <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Message to be persisted.</param>
        public abstract void Persist(IMessage message);
        #endregion
    }
}