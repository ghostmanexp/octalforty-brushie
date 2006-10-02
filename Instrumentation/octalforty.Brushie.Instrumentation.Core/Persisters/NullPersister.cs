namespace octalforty.Brushie.Instrumentation.Core.Persisters
{
    /// <summary>
    /// Persister which simply discards all messages.
    /// </summary>
    public class NullPersister : PersisterBase
    {
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
