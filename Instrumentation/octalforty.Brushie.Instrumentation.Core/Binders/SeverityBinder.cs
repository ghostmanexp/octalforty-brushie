using octalforty.Brushie.Instrumentation.Core;

namespace octalforty.Brushie.Instrumentation.Core.Binders
{
    /// <summary>
    /// Binds messages according to severity values.
    /// </summary>
    public class SeverityBinder : BinderBase
    {
        #region Private Member Variables
        private MessageSeverity[] severities;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a reference to the array of <see cref="MessageSeverity"/>.
        /// </summary>
        public MessageSeverity[] Severities
        {
            get { return severities; }
            set { severities = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="SeverityBinder"/> class with
        /// the given array of severities.
        /// </summary>
        /// <param name="severities"></param>
        public SeverityBinder(MessageSeverity[] severities)
        {
            this.severities = severities;
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
            // If no severities specified, continue with the next binder. 
            if(Severities == null || Severities.GetLength(0) == 0)
                return true;
            
            //
            // Now checking each severity in turn, paying particular attention
            // to the Sink value.
            foreach(MessageSeverity severity in Severities)
            {
                if(severity == MessageSeverity.Sink)
                    return true;
                
                if(severity == message.Severity)
                    return true;
            } // if
            
            return false;
        }
        #endregion
    }
}
