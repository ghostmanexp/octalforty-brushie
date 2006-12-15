namespace octalforty.Brushie.Instrumentation.Core
{
    /// <summary>
    /// Interface which must be implemented by binders, which route messages
    /// to various persisters.
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="persister"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        bool CanBindTo(IPersister persister, IMessage message);
    }
}