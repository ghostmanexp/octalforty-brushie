namespace octalforty.Brushie.Instrumentation.Core.Binders
{
    /// <summary>
    /// Provides a base class for binders.
    /// </summary>
    public abstract class BinderBase : IBinder
    {
        #region Private Member Variables
        private IBinder nextBinder;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="BinderBase"/> class.
        /// </summary>
        protected BinderBase()
        {
        }

        #region IBinder Members
        /// <summary>
        /// Gets or sets a reference to the next <see cref="IBinder"/> in the binding chain.
        /// </summary>
        public IBinder NextBinder
        {
            get { return nextBinder; }
            set { nextBinder = value; }
        }

        /// <summary>
        /// Determines whether the <paramref name="message"/> can be bound to the
        /// persister in question or not.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CanBind(IMessage message)
        {
            if(!InternalCanBind(message))
                return false;
            else if(NextBinder != null)
                return NextBinder.CanBind(message);
            
            return true;
        }
        #endregion

        #region Overridables
        /// <summary>
        /// Internal method, invoked from implementation of <see cref="CanBind"/>.<para />
        /// Determines whether the <paramref name="message"/> can be bound to the
        /// persister in question or not.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected abstract bool InternalCanBind(IMessage message);
        #endregion
    }
}
