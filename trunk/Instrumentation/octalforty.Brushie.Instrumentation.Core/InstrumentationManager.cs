using System.Collections.Generic;

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
        private IList<Internal.Binding> bindings = new List<Internal.Binding>();
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
            if(ConfigurationSettings.Instance != null)
            {
                //
                // Adding persisters.
                foreach(Persister persister in ConfigurationSettings.Instance.Persisters)
                {
                    IPersister persisterInstance = ObjectFactory.CreatePersister(persister);
                    persisterInstance.Configure(persister.Properties);

                    persisters.Add(persister.Name, persisterInstance);
                } // foreach

                //
                // Adding bindings.
                foreach(Configuration.Binding binding in ConfigurationSettings.Instance.Bindings)
                    bindings.Add(new Internal.Binding(binding));
                
                isInstrumentationEnabled = true;
            } // if
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
                persisterNames = PersisterResolver.ResolvePersisterNames(bindings, message);

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
    }
}