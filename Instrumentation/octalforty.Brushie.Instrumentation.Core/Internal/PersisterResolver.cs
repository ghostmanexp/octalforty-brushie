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
            // ������ �������� -- �� ���� ���������.
            if(!MessageTypeQualifies(binding, message))
                return string.Empty;

            foreach(MessageSeverity severity in binding.Severities)
            {
                foreach(string source in binding.Sources)
                {
                    //
                    // ������� ��������, �� ����� �� severity MessageSeverity.Sink,
                    // � source - '*'.
                    if(severity == MessageSeverity.Sink && source == "*")
                        return binding.PersisterName;

                    //
                    // ���� ������ ����, �� ������� ��������, �� ����� �� ��������
                    // severity MessageSeverity.Sink.
                    if(severity == MessageSeverity.Sink)
                    {
                        //
                        // �������� ��������.
                        if(message.Source == source)
                            return binding.PersisterName;
                    } // if

                    //
                    // ������ �������� source �� ��������� '*'.
                    if(source == "*")
                    {
                        //
                        // �������� �����������.
                        if(message.Severity == severity)
                            return binding.PersisterName;
                    } // if

                    //
                    // � �������������� ���� - �������� ��� ��������.
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
