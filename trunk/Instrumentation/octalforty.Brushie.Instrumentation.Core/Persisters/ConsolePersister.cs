using System;

using octalforty.Brushie.Instrumentation.Core.Util;

namespace octalforty.Brushie.Instrumentation.Core.Persisters
{
    /// <summary>
    /// Persister which writes messages to console using supplied format string.
    /// </summary>
    public class ConsolePersister : FormattingPersister
    {
        #region FormattingPersister Members
        /// <summary>
        /// Persists message <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Message to be persisted.</param>
        public override void Persist(IMessage message)
        {
            Console.WriteLine(MessageFormatter.FormatMessage(message, FormatString));
        }
        #endregion
    }
}