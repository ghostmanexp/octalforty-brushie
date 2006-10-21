using System.IO;

using octalforty.Brushie.Instrumentation.Core.Util;

namespace octalforty.Brushie.Instrumentation.Core.Persisters
{
    /// <summary>
    /// Persister which writes messages to the supplied instance of <see cref="TextWriter"/>.
    /// </summary>
    public class TextWriterPersister : FormattingPersister
    {
        #region Private Member Variables
        private TextWriter textWriter;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="TextWriterPersister"/> with a given
        /// instance of <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="textWriter"></param>
        public TextWriterPersister(TextWriter textWriter)
        {
            this.textWriter = textWriter;
        }

        #region FormattingPersister Members
        /// <summary>
        /// Persists message <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Message to be persisted.</param>
        public override void Persist(IMessage message)
        {
            textWriter.WriteLine(MessageFormatter.FormatMessage(message, FormatString));
        }
        #endregion
    }
}