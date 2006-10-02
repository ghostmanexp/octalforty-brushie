using System.Diagnostics;

using octalforty.Brushie.Instrumentation.Core.Util;

namespace octalforty.Brushie.Instrumentation.Core.Persisters
{
    /// <summary>
    /// Persister which writes messages to the trace using supplied format string.
    /// </summary>
    public class TracePersister : FormattingPersister
    {
        #region FormattingPersister Members
        /// <summary>
        /// Persists message <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Message to be persisted.</param>
        public override void Persist(IMessage message)
        {
            Trace.WriteLine(MessageFormatter.FormatMessage(message, FormatString));
        }
        #endregion
    }
}
