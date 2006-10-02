using System;

using octalforty.Brushie.Instrumentation.Core;
using octalforty.Brushie.Instrumentation.Core.Configuration;

namespace octalforty.Brushie.Instrumentation.Core.Internal
{
    /// <summary>
    /// Creates instances of various objects.
    /// </summary>
    internal static class ObjectFactory
    {
        /// <summary>
        /// Creates an instance of class which implements <see cref="IFormatter"/>.
        /// </summary>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public static IFormatter CreateFormatter(Formatter formatter)
        {
            Type formatterType = Type.GetType(formatter.Type);
            IFormatter formatterInstance = Activator.CreateInstance(formatterType)
                as IFormatter;
            
            return formatterInstance;
        }
        
        /// <summary>
        /// Creates an instance of class which implements <see cref="IPersister"/>.
        /// </summary>
        /// <param name="persister"></param>
        /// <returns></returns>
        public static IPersister CreatePersister(Persister persister)
        {
            Type persisterType = Type.GetType(persister.Type);
            IPersister persisterInstance = Activator.CreateInstance(persisterType)
                as IPersister;
            
            return persisterInstance;
        }
    }
}
