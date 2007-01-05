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
    /// Core class of octalforty Brushie Instrumentation framework.
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
                    
                    persisters.Add(persister.Name, persisterInstance);
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
                    binders.Add(binding.PersisterName, CreateBinder(binding));
                
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
            IBinder rootBinder = new MessageTypeBinder(GetMessageTypes(binding));
            
            IBinder sourceBinder = new SourceBinder(GetSources(binding));
            rootBinder.NextBinder = sourceBinder;

            IBinder severityBinder = new SeverityBinder(GetSeverities(binding));
            sourceBinder.NextBinder = severityBinder;
            
            return rootBinder;
        }

        /// <summary>
        /// Gets an array of message types.
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        private Type[] GetMessageTypes(BindingElement binding)
        {
            List<Type> effectiveTypes = new List<Type>();
            string[] types = binding.Message.Split(',');

            foreach(string type in types)
            {
                string typeToAdd = type.Trim();
                if(typeToAdd == string.Empty)
                    continue;

                effectiveTypes.Add(Type.GetType(typeToAdd));
            } // foreach
            
            return effectiveTypes.ToArray();
        }

        /// <summary>
        /// Gets an array of strings with sources.
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        private string[] GetSources(BindingElement binding)
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
        private MessageSeverity[] GetSeverities(BindingElement binding)
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
                if(!isInstrumentationEnabled)
                    return;
                
                //
                // Resolving persister names.
                string[] persisterNames;
                persisterNames = ResolverPersisterNames(message);

                if(persisterNames == null || persisterNames.GetLength(0) == 0)
                    throw new InstrumentationException(string.Format(
                        Strings.InstrumentationManager_InternalInstrument_UnableToResolvePersistersForMessage,
                        message.GetType().FullName, message.Source, message.Severity));

                //
                // Persisting message.
                foreach(string persisterName in persisterNames)
                {
                    if(!persisters.ContainsKey(persisterName))
                        throw new InstrumentationException(string.Format(
                            Strings.InstrumentationManager_InternalInstrument_NoPersistersWithGivenNameRegistered,
                            persisterName));

                    IPersister persister = persisters[persisterName];
                    persister.Persist(message);
                } // foreach              
            } // lock
        }

        /// <summary>
        /// Resolves names of persisters used to persist <paramref name="message"/>.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string[] ResolverPersisterNames(IMessage message)
        {
            List<string> resolvedPersisters = new List<string>();
            foreach(KeyValuePair<string, IBinder> binder in binders)
            {
                if(binder.Value.CanBind(message))
                    resolvedPersisters.Add(binder.Key);
            } // foreach

            return resolvedPersisters.ToArray();
        }
    }
}