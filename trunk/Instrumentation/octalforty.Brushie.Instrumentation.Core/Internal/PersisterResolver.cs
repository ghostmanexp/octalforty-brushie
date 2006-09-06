using System.Collections.Generic;

namespace octalforty.Brushie.Instrumentation.Core.Internal
{
    /// <summary>
    /// Resolves persistes depending upon message parameters.
    /// </summary>
    internal sealed class PersisterResolver
    {
        /// <summary>
        /// Resolves persister names based upon <paramref name="bindings"/> and <paramref name="message"/>
        /// parameters.
        /// </summary>
        /// <param name="bindings">Bindings.</param>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public static string[] ResolvePersisterNames(IList<Binding> bindings, IMessage message)
        {
            return null;
        }
    }
}
