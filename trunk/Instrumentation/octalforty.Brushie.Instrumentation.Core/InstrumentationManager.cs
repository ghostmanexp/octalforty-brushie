using System;
using System.Collections.Generic;

using octalforty.Brushie.Instrumentation.Core.Binders;
using octalforty.Brushie.Instrumentation.Core.Configuration;
using octalforty.Brushie.Instrumentation.Core.Exceptions;
using octalforty.Brushie.Instrumentation.Core.Internal;
using octalforty.Brushie.Instrumentation.Core.Resources;

namespace octalforty.Brushie.Instrumentation.Core
{
    /// <summary>
    /// Core class of octalforty Brushie Instrumentation Framework.
    /// </summary>
    public sealed class InstrumentationManager
    {
        #region Private Static Member Variables
        private static InstrumentationManager instance = new InstrumentationManager();
        #endregion

        #region Private Member Variables
        private IDictionary<string, IPersister> persisters = new Dictionary<string, IPersister>();
        private IDictionary<string, IBinder> binders = new Dictionary<string, IBinder>();
        private object syncRoot = new object();
        private bool isInstrumentationEnabled = false;
        #endregion

        #region Public Static Properties
        /// <summary>
        /// Returns one and the only instance of Instrumentation manager.
        /// </summary>
        public static InstrumentationManager Instance
        {
            get { return instance; }
        }
        #endregion
        
        #region Private Properties
        private IDictionary<string, IPersister> Persisters
        {
            get { return persisters; }
        }

        private IDictionary<string, IBinder> Binders
        {
            get { return binders; }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value which indicates whether instrumentation is enabled or not.
        /// </summary>
        public bool IsInstrumentationEnabled
        {
            get { return isInstrumentationEnabled; }
        }
        #endregion

        /// <summary>
        /// Explicit static constructor to tell C# compiler not to mark the type as 
        /// <c>beforefieldinit</c>.
        /// </summary>
        static InstrumentationManager()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InstrumentationManager"/> class.
        /// </summary>
        private InstrumentationManager()
        {
            //
            // First off, check whether the configuration section is present. If
            // it is, proceed with configuring InstrumentationManager.
            if(ConfigurationManager.ConfigurationSection != null)
            {
                //
                // Adding persisters.
                foreach(PersisterElement persister in ConfigurationManager.ConfigurationSection.Persisters)
                {
                    IPersister persisterInstance =
                        ObjectFactory.CreatePersister(persister);
                    persisterInstance.Configure(persister.CustomProperties.ToStringDictionary());
                    
                    Persisters.Add(persister.Name, persisterInstance);
                } // foreach
                
                //
                // Adding formatters.
                foreach(FormatterElement formatter in ConfigurationManager.ConfigurationSection.Formatters)
                {
                    IFormatter formatterInstance = ObjectFactory.CreateFormatter(formatter);
                    FormattingManager.AddFormatter(formatterInstance);
                }
                
                //
                // Creating binders.
                foreach(BindingElement binding in ConfigurationManager.ConfigurationSection.Bindings)
                    Binders.Add(binding.PersisterName, CreateBinder(binding));
                
                isInstrumentationEnabled = true;
            } // if
        }

        /// <summary>
        /// Creates a binder for the given binding.
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        private IBinder CreateBinder(BindingElement binding)
        {
            IBinder rootBinder = new MessageTypeBinder(GetMessageTypeNames(binding));
            
            IBinder sourceBinder = new SourceBinder(GetSources(binding));
            rootBinder.NextBinder = sourceBinder;

            IBinder severityBinder = new SeverityBinder(GetSeverities(binding));
            sourceBinder.NextBinder = severityBinder;
            
            return rootBinder;
        }

        /// <summary>
        /// Gets an array of message type names.
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        private static string[] GetMessageTypeNames(BindingElement binding)
        {
            List<string> effectiveTypeNames = new List<string>();
            string[] typeNames = binding.Message.Split(',');

            foreach(string typeName in typeNames)
            {
                string typeNameToAdd = typeName.Trim();
                if(typeNameToAdd == string.Empty)
                    continue;
                
                //
                // We either add an asterisk or the assembly-qualified name of the 
                // message type.
                if(typeNameToAdd == "*")
                    effectiveTypeNames.Add(typeNameToAdd);
                else
                    effectiveTypeNames.Add(ConfigurationManager.ConfigurationSection.Messages[typeNameToAdd].Type);

                effectiveTypeNames.Add(typeNameToAdd);
            } // foreach
            
            return effectiveTypeNames.ToArray();
        }

        /// <summary>
        /// Gets an array of strings with sources.
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        private static string[] GetSources(BindingElement binding)
        {
            List<string> effectiveSources = new List<string>();
            string[] sources = binding.Source.Split(',');

            foreach(string source in sources)
            {
                string sourceToAdd = source.Trim();
                if(sourceToAdd == string.Empty)
                    continue;
                
                effectiveSources.Add(sourceToAdd);
            } // foreach

            return effectiveSources.ToArray();
        }

        /// <summary>
        /// Gets an array of <see cref="MessageSeverity"/>.
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        private static MessageSeverity[] GetSeverities(BindingElement binding)
        {
            List<MessageSeverity> effectiveSeverities = new List<MessageSeverity>();
            string[] severities = binding.Severity.Split(',');
            
            foreach(string severity in severities)
            {
                string severityToParse = severity.Trim();
                if(severityToParse == string.Empty)
                    continue;
                
                if(severityToParse == "*")
                    effectiveSeverities.Add(MessageSeverity.Sink);
                else 
                    effectiveSeverities.Add((MessageSeverity)Enum.Parse(
                        typeof(MessageSeverity), severityToParse));
            } // foreach

            return effectiveSeverities.ToArray();
        }

        /// <summary>
        /// Instruments message <paramref name="message"/>.
        /// </summary>
        /// <param name="message"></param>
        public static void Instrument(IMessage message)
        {
            Instance.InternalInstrument(message);
        }

        /// <summary>
        /// Instruments message <paramref name="message"/>.
        /// </summary>
        /// <param name="message"></param>
        private void InternalInstrument(IMessage message)
        {
            lock(syncRoot)
            {
                if(!IsInstrumentationEnabled)
                    return;
                
                //
                // Resolving persister names.
                string[] persisterNames;
                persisterNames = ResolvePersisterNames(message);

                if(persisterNames == null || persisterNames.GetLength(0) == 0)
                    throw new InstrumentationException(string.Format(
                        Strings.InstrumentationManager_InternalInstrument_UnableToResolvePersistersForMessage,
                        message.GetType().FullName, message.Source, message.Severity));

                //
                // Persisting message.
                foreach(string persisterName in persisterNames)
                {
                    if(!Persisters.ContainsKey(persisterName))
                        throw new InstrumentationException(string.Format(
                            Strings.InstrumentationManager_InternalInstrument_NoPersistersWithGivenNameRegistered,
                            persisterName));

                    IPersister persister = Persisters[persisterName];
                    persister.Persist(message);
                } // foreach              
            } // lock
        }

        /// <summary>
        /// Resolves names of persisters used to persist <paramref name="message"/>.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string[] ResolvePersisterNames(IMessage message)
        {
            List<string> resolvedPersisters = new List<string>();
            foreach(KeyValuePair<string, IBinder> binder in Binders)
            {
                if(binder.Value.CanBind(message))
                    resolvedPersisters.Add(binder.Key);
            } // foreach

            return resolvedPersisters.ToArray();
        }
    }
}