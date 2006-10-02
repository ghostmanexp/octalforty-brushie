using System;
using System.Collections.Generic;

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
        /// from an instance of <see cref="Configuration.Binding"/> class.
        /// </summary>
        /// <param name="binding"></param>
        public Binding(Configuration.Binding binding)
        {
            persisterName = binding.PersisterName;
            sources = binding.Sources;
            
            InitializeSeverities(binding);
            InitializeMessages(binding);
        }

        private void InitializeMessages(Configuration.Binding binding)
        {
            List<Type> effectiveMessages = new List<Type>();
            foreach(string message in binding.Messages)
                effectiveMessages.Add(Type.GetType(message));

            messages = effectiveMessages.ToArray();
        }

        private void InitializeSeverities(Configuration.Binding binding)
        {
            List<MessageSeverity> effectiveSeverities = new List<MessageSeverity>();
            foreach(string severity in binding.Severities)
                effectiveSeverities.Add((MessageSeverity)Enum.Parse(typeof(MessageSeverity), 
                    severity, true));
            
            severities = effectiveSeverities.ToArray();
        }
    }
}
