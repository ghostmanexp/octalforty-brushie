using System;
using System.Collections.Generic;

using octalforty.Brushie.Instrumentation.Core.Configuration;

namespace octalforty.Brushie.Instrumentation.Core.Internal
{
    internal class Binding
    {
        #region Private Member Variables
        private MessageSeverity[] severities;
        private string[] sources;
        private Type[] messages;
        private string persisterName;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a reference to the array of severities.
        /// </summary>
        public MessageSeverity[] Severities
        {
            get { return severities; }
        }

        /// <summary>
        /// Gets a reference to the array of sources.
        /// </summary>
        public string[] Sources
        {
            get { return sources; }
        }

        /// <summary>
        /// Gets a reference to the array of message types.
        /// </summary>
        public Type[] Messages
        {
            get { return messages; }
        }

        /// <summary>
        /// Gets a string which contains a name of the persister.
        /// </summary>
        public string PersisterName
        {
            get { return persisterName; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Binding"/> class
        /// from an instance of <see cref="Configuration.BindingElement"/> class.
        /// </summary>
        /// <param name="binding"></param>
        public Binding(BindingElement binding)
        {
            persisterName = binding.PersisterName;
            sources = GetSources(binding.Source);
            
            InitializeSeverities(binding);
            InitializeMessages(binding);
        }

        private void InitializeMessages(BindingElement binding)
        {
            List<Type> effectiveMessages = new List<Type>();
            string[] _messages = GetMessages(binding.Message);
            
            foreach(string _message in _messages)
                effectiveMessages.Add(Type.GetType(_message));

            messages = effectiveMessages.ToArray();
        }

        private void InitializeSeverities(BindingElement binding)
        {
            List<MessageSeverity> effectiveSeverities = new List<MessageSeverity>();
            string[] _severities = GetSeverities(binding.Severity);

            foreach(string _severity in _severities)
            {
                if(_severity == "*")
                    effectiveSeverities.Add(MessageSeverity.Sink);
                else
                    effectiveSeverities.Add((MessageSeverity)Enum.Parse(
                        typeof(MessageSeverity), _severity, true));
            } // else
            
            severities = effectiveSeverities.ToArray();
        }

        private string[] GetMessages(string message)
        {
            return SplitString(message);
        }

        private string[] GetSources(string source)
        {
            return SplitString(source);
        }

        private string[] GetSeverities(string severity)
        {
            return SplitString(severity);
        }

        private static string[] SplitString(string sourceString)
        {
            List<string> effectiveStrings = new List<string>();
            string[] _strings = sourceString.Split(',');

            foreach(string _string in _strings)
                effectiveStrings.Add(_string);

            return effectiveStrings.ToArray();
        }
    }
}
