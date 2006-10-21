namespace octalforty.Brushie.Instrumentation.Core.Persisters
{
    /// <summary>
    /// Persister which simply discards all messages.
    /// </summary>
    public class NullPersister : PersisterBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NullPersister"/> class.
        /// </summary>
        public NullPersister()
        {
        }

        #region PersisterBase Members
        /// <summary>
        /// Persists message <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Message to be persisted.</param>
        public override void Persist(IMessage message)
        {
            
        }
        #endregion
    }
}