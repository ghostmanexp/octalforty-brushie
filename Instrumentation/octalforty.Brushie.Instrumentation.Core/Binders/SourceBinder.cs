namespace octalforty.Brushie.Instrumentation.Core.Binders
{
    /// <summary>
    /// Binds messages according to source values.
    /// </summary>
    public class SourceBinder : BinderBase
    {
        #region Private Member Variables
        private string[] sources;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a reference to the array of strings with the sources
        /// of the messages.
        /// </summary>
        public string[] Sources
        {
            get { return sources; }
            set { sources = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="SourceBinder"/> class with
        /// a given array of sources.
        /// </summary>
        /// <param name="sources"></param>
        public SourceBinder(string[] sources)
        {
            this.sources = sources;
        }

        #region BinderBase Members
        /// <summary>
        /// Internal method, invoked from implementation of <see cref="BinderBase.CanBind"/>.<para />
        /// Determines whether the <paramref name="message"/> can be bound to the
        /// persister in question or not.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected override bool InternalCanBind(IMessage message)
        {
            //
            // If no sources specified, move on to the next binder.
            if(Sources == null || Sources.GetLength(0) == 0)
                return true;
            
            //
            // Now check each source individually.
            foreach(string source in Sources)
            {
                if(source == message.Source)
                    return true;
            } // foreach
            
            return false;
        }
        #endregion
    }
}
