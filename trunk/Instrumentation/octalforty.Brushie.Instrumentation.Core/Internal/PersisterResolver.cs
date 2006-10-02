using System;
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
            List<string> persisterNames = new List<string>();
            foreach(Binding binding in bindings)
            {
                string persisterName = InternalResolvePersisterName(binding, message);
                if(persisterName != string.Empty)
                    persisterNames.Add(persisterName);
            } // foreach

            return persisterNames.ToArray();
        }

        private static string InternalResolvePersisterName(Binding binding, IMessage message)
        {
            //
            // Первая проверка -- по типу сообщения.
            if(!MessageTypeQualifies(binding, message))
                return string.Empty;

            foreach(MessageSeverity severity in binding.Severities)
            {
                foreach(string source in binding.Sources)
                {
                    //
                    // Сначала проверим, не равна ли severity MessageSeverity.Sink,
                    // а source - '*'.
                    if(severity == MessageSeverity.Sink && source == "*")
                        return binding.PersisterName;

                    //
                    // Если попали сюда, то сначала проверим, не равно ли отдельно
                    // severity MessageSeverity.Sink.
                    if(severity == MessageSeverity.Sink)
                    {
                        //
                        // Проверим источник.
                        if(message.Source == source)
                            return binding.PersisterName;
                    } // if

                    //
                    // Теперь проверим source на равенство '*'.
                    if(source == "*")
                    {
                        //
                        // Проверим Серьезность.
                        if(message.Severity == severity)
                            return binding.PersisterName;
                    } // if

                    //
                    // И заключительный этап - проверим все отдельно.
                    if(severity == message.Severity && source == message.Source)
                        return binding.PersisterName;
                } // foreach
            } // foreach

            return string.Empty;
        }

        private static bool MessageTypeQualifies(Binding binding, IMessage message)
        {
            if(binding.Messages.GetLength(0) == 0)
                return true;

            Type requiredMessageType = message.GetType();
            foreach(Type messageType in binding.Messages)
                if(messageType == requiredMessageType)
                    return true;

            return false;
        }

    }
}
